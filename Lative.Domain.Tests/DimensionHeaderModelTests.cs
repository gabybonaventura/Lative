namespace Lative.Domain.Tests;
using NUnit.Framework;

[TestFixture]
public class DimensionHeaderModelTests
{
    [Test]
    public void DimensionHeaderModel_ConstructedWithValues_PropertiesAreSet()
    {
        // Arrange
        var name = "DimensionHeader";
        var nameIndex = 1;
        var startDateIndex = 2;
        var endDateIndex = 3;

        // Act
        var dimensionHeader = new DimensionHeaderModel
        {
            Name = name,
            NameIndex = nameIndex,
            StartDateIndex = startDateIndex,
            EndDateIndex = endDateIndex
        };

        // Assert
        Assert.That(dimensionHeader.Name, Is.EqualTo(name));
        Assert.That(dimensionHeader.NameIndex, Is.EqualTo(nameIndex));
        Assert.That(dimensionHeader.StartDateIndex, Is.EqualTo(startDateIndex));
        Assert.That(dimensionHeader.EndDateIndex, Is.EqualTo(endDateIndex));
    }
}
