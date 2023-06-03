using Lative.Application;
using NUnit.Framework;

namespace Lative.Domain.Tests;

[TestFixture]
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
    public void ParseSaleOperationModel_ValidFields_ReturnsSaleOperationModel()
    {
        // Arrange
        var fields = new[]
        {
            "3707384-15976-1",
            "667332",
            "USD",
            "15,020.00",
            "31/10/2023",
            "United States",
            "FS AM Enterprise",
            "Recurring Adjustment",
            "Recurring Adjustment",
            "PCM",
            "DealCloud",
            "DealCloud",
            "31/10/2023",
            "",
            "31/10/2023",
            "",
            "31/10/2023",
            "",
            "31/10/2023",
            "",
            "31/10/2023",
            "",
            "31/10/2023",
            "",
            "31/10/2023",
            "",
            "31/10/2023"
        };

        // Act
        var saleOperationModel = csvProcessor.ParseSaleOperationModel(fields);

        // Assert
        Assert.That(saleOperationModel.OpportunityID, Is.EqualTo("3707384-15976-1"));
        Assert.That(saleOperationModel.OpportunityOwner, Is.EqualTo("667332"));
        Assert.That(saleOperationModel.Currency, Is.EqualTo("USD"));
        Assert.That(saleOperationModel.Amount, Is.EqualTo(15020.00f));
        Assert.That(saleOperationModel.CloseDate, Is.EqualTo(new DateOnly(2023, 10, 31)));
    }

    [Test]
    public void ProcessDimensions_ValidFields_AddsDimensionsToSaleOperationModel()
    {
        // Arrange
        var saleOperationModel = new SaleOperationModel();
        var fields = new[]
        {
            "3707384-15976-1",
            "667332",
            "USD",
            "15,020.00",
            "31/10/2023",
            "United States",
            "FS AM Enterprise",
            "Recurring Adjustment",
            "Recurring Adjustment",
            "PCM",
            "DealCloud",
            "DealCloud",
            "31/10/2023",
            "",
            "31/10/2023",
            "",
            "31/10/2023",
            "",
            "31/10/2023",
            "",
            "31/10/2023",
            "",
            "31/10/2023",
            "",
            "31/10/2023",
            "",
            "31/10/2023"
        };

        // Act
        csvProcessor.ProcessDimensions(saleOperationModel, fields);

        // Assert
        Assert.That(saleOperationModel.Dimensions.Count, Is.EqualTo(6));
        Assert.That(saleOperationModel.Dimensions.ContainsKey("Location"), Is.True);
        Assert.That(saleOperationModel.Dimensions.ContainsKey("Segment"), Is.True);
        Assert.That(saleOperationModel.Dimensions.ContainsKey("Category"), Is.True);
        Assert.That(saleOperationModel.Dimensions.ContainsKey("ACV Type"), Is.True);
        Assert.That(saleOperationModel.Dimensions.ContainsKey("Subvertical"), Is.True);
        Assert.That(saleOperationModel.Dimensions.ContainsKey("Product Family"), Is.True);
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
}
