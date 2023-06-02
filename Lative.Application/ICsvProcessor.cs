using Lative.Domain;

namespace Lative.Application;

public interface ICsvProcessor
{
    IEnumerable<SaleOperationModel> ReadCsv(string filePath);
}