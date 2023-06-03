using Lative.Domain;

namespace Lative.DataAccess;

public interface IDataImporter
{
    void BulkInsertSalesAndDimensions(IEnumerable<SaleOperationModel> sales, int batchSize);
}