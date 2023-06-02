namespace Lative.Domain.Tests;

using Lative.Application;
using NUnit.Framework;

[TestFixture]
public class IndexMapTests
{
    private IndexMap indexMap;

    [SetUp]
    public void SetUp()
    {
        indexMap = new IndexMap();
    }

    [Test]
    public void SetIndexMap_WithValidHeader_SetsIndicesCorrectly()
    {
        var header = "Opportunity ID,Opportunity Owner,Currency,Amount,Close Date";

        indexMap.SetIndexMap(header);

        Assert.AreEqual(0, indexMap.GetIndex("Opportunity ID"));
        Assert.AreEqual(1, indexMap.GetIndex("Opportunity Owner"));
        Assert.AreEqual(2, indexMap.GetIndex("Currency"));
        Assert.AreEqual(3, indexMap.GetIndex("Amount"));
        Assert.AreEqual(4, indexMap.GetIndex("Close Date"));
    }

    [Test]
    public void GetIndex_WithUnknownHeader_ReturnsNegativeOne()
    {
        var header = "Opportunity ID,Opportunity Owner,Currency,Amount,Close Date";

        indexMap.SetIndexMap(header);

        var index = indexMap.GetIndex("Unknown Header");

        Assert.AreEqual(-1, index);
    }
}
