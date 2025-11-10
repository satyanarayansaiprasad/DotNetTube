namespace NoteSalesWeb.Models;

public enum PurchasePlan
{
    Starter = 1,      // ₹299
    Career = 2,        // ₹599
    Elite = 3          // ₹999
}

public class PlanDetails
{
    public PurchasePlan Plan { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string PaymentLink { get; set; } = string.Empty;
    public List<string> Modules { get; set; } = new();
    public List<string> Features { get; set; } = new();
}

