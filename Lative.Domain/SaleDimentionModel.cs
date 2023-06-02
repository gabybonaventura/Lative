namespace Lative.Domain;

public class SaleDimentionModel
{
    public string Name { get; set; }
    public string Value { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}