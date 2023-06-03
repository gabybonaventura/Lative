using Lative.Domain;
using Npgsql;
using NpgsqlTypes;

namespace Lative.DataAccess;

public class DataImporter : IDataImporter
{
    private readonly string _connectionString;

    public DataImporter(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void BulkInsertSalesAndDimensions(IEnumerable<SaleOperationModel> sales, int batchSize)
    {
        using var connection = new NpgsqlConnection(_connectionString);
        connection.Open();

        using var binaryImporterSales = connection.BeginBinaryImport(GetSaleImportSql(batchSize));
        using var binaryImporterDimensions = connection.BeginBinaryImport(GetDimensionImportSql(batchSize));

        var salesBatch = new List<SaleOperationModel>();
        var dimensionsBatch = new List<SaleDimensionModel>();

        foreach (var sale in sales)
        {
            salesBatch.Add(sale);

            foreach (var dimension in sale.Dimensions.Values)
            {
                dimensionsBatch.Add(dimension);
            }

            if (salesBatch.Count >= batchSize || dimensionsBatch.Count >= batchSize)
            {
                InsertSalesBatch(binaryImporterSales, salesBatch);
                InsertDimensionsBatch(binaryImporterDimensions, dimensionsBatch);

                salesBatch.Clear();
                dimensionsBatch.Clear();
            }
        }

        if (salesBatch.Count > 0 || dimensionsBatch.Count > 0)
        {
            InsertSalesBatch(binaryImporterSales, salesBatch);
            InsertDimensionsBatch(binaryImporterDimensions, dimensionsBatch);
        }

        binaryImporterSales.Complete();
        binaryImporterDimensions.Complete();
    }

    private void InsertSalesBatch(NpgsqlBinaryImporter binaryImporter, List<SaleOperationModel> salesBatch)
    {
        foreach (var sale in salesBatch)
        {
            binaryImporter.StartRow();
            binaryImporter.Write(sale.OpportunityID, NpgsqlDbType.Text);
            binaryImporter.Write(sale.OpportunityOwner, NpgsqlDbType.Text);
            binaryImporter.Write(sale.Currency, NpgsqlDbType.Text);
            binaryImporter.Write(sale.Amount, NpgsqlDbType.Numeric);
            binaryImporter.Write(sale.CloseDate, NpgsqlDbType.Date);
        }
    }

    private void InsertDimensionsBatch(NpgsqlBinaryImporter binaryImporter, List<SaleDimensionModel> dimensionsBatch)
    {
        foreach (var dimension in dimensionsBatch)
        {
            binaryImporter.StartRow();
            binaryImporter.Write(dimension.Name, NpgsqlDbType.Text);
            binaryImporter.Write(dimension.Value, NpgsqlDbType.Text);
            binaryImporter.Write(dimension.StartDate, NpgsqlDbType.Date);
            binaryImporter.Write(dimension.EndDate, NpgsqlDbType.Date);
            binaryImporter.Write(dimension.SaleOperationModelId, NpgsqlDbType.Text);
        }
    }
    private static string GetSaleImportSql(int batchSize)
    {
        return $"COPY Sales (OpportunityID, OpportunityOwner, Currency, Amount, CloseDate) " +
               $"FROM STDIN (FORMAT BINARY, ROWS {batchSize})";
    }

    private static string GetDimensionImportSql(int batchSize)
    {
        return $"COPY Dimensions (Name, Value, StartDate, EndDate, SaleOperationModelId) " +
               $"FROM STDIN (FORMAT BINARY, ROWS {batchSize})";
    }


}
