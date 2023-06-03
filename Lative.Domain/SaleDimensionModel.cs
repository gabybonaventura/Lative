namespace Lative.Domain;

public class SaleDimensionModel
{
    public string Name { get; set; }
    public string Value { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string SaleOperationModelId { get; set; }
}