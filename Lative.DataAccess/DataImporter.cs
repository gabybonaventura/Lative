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

        using var binaryImporterSales = connection.BeginBinaryImport(GetSaleImportSql());

        using var connectionDimensions = new NpgsqlConnection(_connectionString);
        connectionDimensions.Open();
        using var binaryImporterDimensions = connectionDimensions.BeginBinaryImport(GetDimensionImportSql());

        var salesBatch = new List<SaleOperationModel>();
        foreach (var sale in sales)
        {
            salesBatch.Add(sale);

            if (salesBatch.Count >= batchSize)
            {
                WriteSalesToBinaryImporter(binaryImporterSales, salesBatch);
                WriteDimensionsToBinaryImporter(binaryImporterDimensions, salesBatch);
                salesBatch.Clear();
            }
        }

        // Write any remaining sales in the last batch
        if (salesBatch.Count > 0)
        {
            WriteSalesToBinaryImporter(binaryImporterSales, salesBatch);
            WriteDimensionsToBinaryImporter(binaryImporterDimensions, salesBatch);
        }

        binaryImporterSales.Complete();
        binaryImporterDimensions.Complete();
    }

    private void WriteSalesToBinaryImporter(NpgsqlBinaryImporter binaryImporter, IEnumerable<SaleOperationModel> sales)
    {
        foreach (var sale in sales)
        {
            binaryImporter.StartRow();
            binaryImporter.Write(sale.OpportunityID, NpgsqlDbType.Text);
            binaryImporter.Write(sale.OpportunityOwner, NpgsqlDbType.Text);
            binaryImporter.Write(sale.Currency, NpgsqlDbType.Text);
            binaryImporter.Write(sale.Amount, NpgsqlDbType.Numeric);
            binaryImporter.Write(sale.CloseDate, NpgsqlDbType.Date);
        }
    }

    private void WriteDimensionsToBinaryImporter(NpgsqlBinaryImporter binaryImporter, IEnumerable<SaleOperationModel> sales)
    {
        foreach (var sale in sales)
        {
            foreach (var dimension in sale.Dimensions.Values)
            {
                binaryImporter.StartRow();
                binaryImporter.Write(dimension.Name, NpgsqlDbType.Text);
                binaryImporter.Write(dimension.Value, NpgsqlDbType.Text);
                binaryImporter.Write(dimension.StartDate, NpgsqlDbType.Date);
                binaryImporter.Write(dimension.EndDate, NpgsqlDbType.Date);
                binaryImporter.Write(sale.OpportunityID, NpgsqlDbType.Text);
            }
        }
    }

    private static string GetSaleImportSql()
    {
        return "COPY Sales (OpportunityID, OpportunityOwner, Currency, Amount, CloseDate) " +
            "FROM STDIN (FORMAT BINARY)";
    }

    private static string GetDimensionImportSql()
    {
        return "COPY Dimensions (Name, Value, StartDate, EndDate, SaleOperationModelId) " +
            "FROM STDIN (FORMAT BINARY)";
    }



}
