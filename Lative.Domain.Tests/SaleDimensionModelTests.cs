namespace Lative.Domain.Tests;

using NUnit.Framework;

[TestFixture]
public class SaleDimensionModelTests
{
    [Test]
    public void SaleDimensionModel_ConstructedWithValues_PropertiesAreSet()
    {
        // Arrange
        var name = "SaleDimension";
        var value = "Value";
        DateTime? startDate = new DateTime(2023, 6, 1);
        DateTime? endDate = new DateTime(2023, 6, 30);
        var saleOperationModelId = "12345";

        // Act
        var saleDimension = new SaleDimensionModel
        {
            Name = name,
            Value = value,
            StartDate = startDate,
            EndDate = endDate,
            SaleOperationModelId = saleOperationModelId
        };

        // Assert
        Assert.That(saleDimension.Name, Is.EqualTo(name));
        Assert.That(saleDimension.Value, Is.EqualTo(value));
        Assert.That(saleDimension.StartDate, Is.EqualTo(startDate));
        Assert.That(saleDimension.EndDate, Is.EqualTo(endDate));
        Assert.That(saleDimension.SaleOperationModelId, Is.EqualTo(saleOperationModelId));
    }
}
