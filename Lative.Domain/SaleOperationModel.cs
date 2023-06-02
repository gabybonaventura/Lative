using System.Globalization;

namespace Lative.Domain;

public class SaleOperationModel
{
    public string OpportunityID { get; set; }
    public string OpportunityOwner { get; set; }
    public string Currency { get; set; }
    public float Amount { get; set; }
    public DateOnly CloseDate { get; set; }
    public Dictionary<string, SaleDimentionModel> Dimentions { get; set; }
}