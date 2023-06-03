using Lative.Application;
using NUnit.Framework;

namespace Lative.Domain.Tests;

public class CsvProcessorTests
{
    private CsvProcessor csvProcessor;
    private string csvFilePath;

    [SetUp]
    public void SetUp()
    {
        csvProcessor = new CsvProcessor(new IndexMap());
        csvFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "sample_data_customer_1.csv");
    }

    [Test]
    public void ReadCsv_WithValidCsvFile_ParsesFieldsCorrectly()
    {
        // Act
        var saleOperationModels = csvProcessor.ReadCsv(csvFilePath).ToList();

        // Assert
        Assert.That(saleOperationModels.Count, Is.EqualTo(2));

        Assert.That(saleOperationModels[0].OpportunityID, Is.EqualTo("3707384-15976-1"));
        Assert.That(saleOperationModels[0].OpportunityOwner, Is.EqualTo("667332"));
        Assert.That(saleOperationModels[0].Currency, Is.EqualTo("USD"));
        Assert.That(saleOperationModels[0].Amount, Is.EqualTo(15020.00f));
        Assert.That(saleOperationModels[0].CloseDate, Is.EqualTo(new DateOnly(2023, 10, 31)));

        Assert.That(saleOperationModels[1].OpportunityID, Is.EqualTo("3702396-15704-2"));
        Assert.That(saleOperationModels[1].OpportunityOwner, Is.EqualTo("667391"));
        Assert.That(saleOperationModels[1].Currency, Is.EqualTo("USD"));
        Assert.That(saleOperationModels[1].Amount, Is.EqualTo(1890.00f));
        Assert.That(saleOperationModels[1].CloseDate, Is.EqualTo(new DateOnly(2023, 10, 1)));
    }

    [Test]
    public void ReadCsv_WithValidCsvFile_ParsesDimensionsCorrectly()
    {
        // Act
        var saleOperationModels = csvProcessor.ReadCsv(csvFilePath).ToList();

        // Assert
        Assert.That(saleOperationModels.Count, Is.EqualTo(2));
        
        Assert.That(saleOperationModels[0].Dimensions.Count, Is.EqualTo(7)); // Expect 7 dimensions

        Assert.That(saleOperationModels[1].Dimensions.Count, Is.EqualTo(7)); // Expect 7 dimensions
    }
}
