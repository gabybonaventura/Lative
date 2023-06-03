using Lative.Domain;

namespace Lative.Application;

public interface IIndexMap
{
    void SetIndexMap(string header);
    int GetIndex(string header);
    List<DimensionHeaderModel> GetDimensionHeaders();
}