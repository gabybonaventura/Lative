using System.Diagnostics.CodeAnalysis;
using Lative.Domain;

namespace Lative.Application;

public class IndexMap : IIndexMap
{
    private readonly Dictionary<string, int> _map = new();
    private readonly List<DimensionHeaderModel> _dimensionHeaders = new();
    public void SetIndexMap(string header)
    {
        var headerSplit = header.Split(',');
        for (var i = 0; i < headerSplit.Length; i++)
        {
            _map.Add(headerSplit[i], i);
        }
    }
    public int GetIndex(string header)
    {
        if(_map.TryGetValue(header, out var index))
            return index;
        return -1;
    }
    public List<DimensionHeaderModel> GetDimensionHeaders()
    {
        if(_dimensionHeaders.Count != 0)
            return _dimensionHeaders;
        
        var dimensionHeaders = _map.Keys.Where(key => key.EndsWith(" Start Date")).Select(key => key.Substring(0, key.Length - 11));
        foreach (var header in dimensionHeaders)
        {
            _dimensionHeaders.Add(new DimensionHeaderModel
            {
                Name = header,
                NameIndex = GetIndex(header),
                StartDateIndex = GetIndex(header + " Start Date"),
                EndDateIndex = GetIndex(header + " End Date")
            });
        }
        return _dimensionHeaders;
    }
}