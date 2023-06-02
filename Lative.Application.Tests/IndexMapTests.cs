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
        const string header = "Opportunity ID,Opportunity Owner,Currency,Amount,Close Date";

        indexMap.SetIndexMap(header);

        Assert.That(indexMap.GetIndex("Opportunity ID"), Is.EqualTo(0));
        Assert.That(indexMap.GetIndex("Opportunity Owner"), Is.EqualTo(1));
        Assert.That(indexMap.GetIndex("Currency"), Is.EqualTo(2));
        Assert.That(indexMap.GetIndex("Amount"), Is.EqualTo(3));
        Assert.That(indexMap.GetIndex("Close Date"), Is.EqualTo(4));
    }

    [Test]
    public void GetIndex_WithUnknownHeader_ReturnsNegativeOne()
    {
        const string header = "Opportunity ID,Opportunity Owner,Currency,Amount,Close Date";

        indexMap.SetIndexMap(header);

        var index = indexMap.GetIndex("Unknown Header");

        Assert.That(index, Is.EqualTo(-1));
    }
}
