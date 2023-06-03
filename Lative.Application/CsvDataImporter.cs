using Lative.DataAccess;

namespace Lative.Application;

public class CsvDataImporter : ICsvDataImporter
{
    private readonly ICsvProcessor _csvProcessor;
    private readonly IDataImporter _dataImporter;

    public CsvDataImporter(ICsvProcessor csvProcessor, IDataImporter dataImporter)
    {
        _csvProcessor = csvProcessor;
        _dataImporter = dataImporter;
    }

    public void ImportCsv(string filePath)
    {
        var sales = _csvProcessor.ReadCsv(filePath);
        _dataImporter.BulkInsertSalesAndDimensions(sales, 10);
    }
}