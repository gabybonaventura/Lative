using Lative.Domain;
using Microsoft.VisualBasic.FileIO;

namespace Lative.Application;

public class CsvProcessor : ICsvProcessor
{
    private readonly IIndexMap _indexMap;
    public CsvProcessor(IIndexMap indexMap)
    {
        _indexMap = indexMap;
    }
    public IEnumerable<SaleOperationModel> ReadCsv(string filePath)
    {
        using (var parser = new TextFieldParser(filePath) { TextFieldType = FieldType.Delimited, Delimiters = new[] { "," } })
        {
            var line = parser.ReadLine();
            _indexMap.SetIndexMap(line);

            while (!parser.EndOfData)
            {
                var fields = parser.ReadFields();
                var saleOperationModel = ParseSaleOperationModel(fields);
                ProcessDimensions(saleOperationModel, fields);
                yield return saleOperationModel;
            }
        }
    }

    public SaleOperationModel ParseSaleOperationModel(string[] fields)
    {
        var saleOperationModel = new SaleOperationModel
        {
            OpportunityID = fields[_indexMap.GetIndex("Opportunity ID")],
            OpportunityOwner = fields[_indexMap.GetIndex("Opportunity Owner")],
            Currency = fields[_indexMap.GetIndex("Currency")]
        };

        if (DateTime.TryParse(fields[_indexMap.GetIndex("Close Date")], out var closeDate))
        {
            saleOperationModel.CloseDate = closeDate;
        }
        else
        {
            // TODO: Log the error and move the line to an error file
            return null;
        }

        if (float.TryParse(fields[_indexMap.GetIndex("Amount")], out var amount))
        {
            saleOperationModel.Amount = amount;
        }
        else
        {
            // TODO: Log the error and move the line to an error file
            return null; 
        }

        return saleOperationModel;
    }

    public void ProcessDimensions(SaleOperationModel saleOperationModel, string[] fields)
    {
        foreach (var dimensionHeader in _indexMap.GetDimensionHeaders())
        {
            var dimensionValue = fields[dimensionHeader.NameIndex];
            var startDate = fields[dimensionHeader.StartDateIndex];
            var endDate = fields[dimensionHeader.EndDateIndex];

            if (!string.IsNullOrEmpty(dimensionValue))
            {
                var dimensionModel = new SaleDimensionModel
                {
                    Name = dimensionHeader.Name,
                    Value = dimensionValue
                };

                if (DateTime.TryParse(startDate, out var start))
                {
                    dimensionModel.StartDate = start;
                }

                if (DateTime.TryParse(endDate, out var end))
                {
                    dimensionModel.EndDate = end;
                }

                saleOperationModel.Dimensions.Add(dimensionHeader.Name, dimensionModel);
            }
        }
    }
}