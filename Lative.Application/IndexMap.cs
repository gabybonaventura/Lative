using System.Diagnostics.CodeAnalysis;

namespace Lative.Application;

public class IndexMap : IIndexMap
{
    private Dictionary<string, int> _map = new();
    
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
}