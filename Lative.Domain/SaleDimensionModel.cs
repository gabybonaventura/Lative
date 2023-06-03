namespace Lative.Domain;

public class SaleDimensionModel
{
    public string Name { get; set; }
    public string Value { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string SaleOperationModelId { get; set; }
}