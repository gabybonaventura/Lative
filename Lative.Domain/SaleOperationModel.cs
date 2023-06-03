using System.Globalization;

namespace Lative.Domain;

public class SaleOperationModel
{
    public SaleOperationModel()
    {
        Dimensions = new Dictionary<string, SaleDimensionModel>();
    }
    public string OpportunityID { get; set; }
    public string OpportunityOwner { get; set; }
    public string Currency { get; set; }
    public float Amount { get; set; }
    public DateTime CloseDate { get; set; }
    public Dictionary<string, SaleDimensionModel> Dimensions { get; set; }
}