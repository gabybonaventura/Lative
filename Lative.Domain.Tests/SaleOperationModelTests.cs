namespace Lative.Domain.Tests;

using NUnit.Framework;
using System.Collections.Generic;

[TestFixture]
public class SaleOperationModelTests
{
    [Test]
    public void SaleOperationModel_ConstructedWithValues_PropertiesAreSet()
    {
        // Arrange
        var opportunityId = "12345";
        var opportunityOwner = "John Doe";
        var currency = "USD";
        var amount = 15020.00f;
        var closeDate = new DateTime(2023, 12, 31);
        var dimensions = new Dictionary<string, SaleDimensionModel>
        {
            { "Dimension1", new SaleDimensionModel() },
            { "Dimension2", new SaleDimensionModel() }
        };

        // Act
        var saleOperation = new SaleOperationModel
        {
            OpportunityID = opportunityId,
            OpportunityOwner = opportunityOwner,
            Currency = currency,
            Amount = amount,
            CloseDate = closeDate,
            Dimensions = dimensions
        };

        // Assert
        Assert.That(saleOperation.OpportunityID, Is.EqualTo(opportunityId));
        Assert.That(saleOperation.OpportunityOwner, Is.EqualTo(opportunityOwner));
        Assert.That(saleOperation.Currency, Is.EqualTo(currency));
        Assert.That(saleOperation.Amount, Is.EqualTo(amount));
        Assert.That(saleOperation.CloseDate, Is.EqualTo(closeDate));
        Assert.That(saleOperation.Dimensions, Is.EqualTo(dimensions));
    }
}
