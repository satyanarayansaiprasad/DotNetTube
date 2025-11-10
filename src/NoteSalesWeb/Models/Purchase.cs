namespace NoteSalesWeb.Models;

public class Purchase
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Email { get; set; } = string.Empty;
    public PurchasePlan Plan { get; set; }
    public decimal Amount { get; set; }
    public string RazorpayPaymentId { get; set; } = string.Empty;
    public string RazorpayOrderId { get; set; } = string.Empty;
    public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
    public bool IsVerified { get; set; }
    public string DownloadToken { get; set; } = Guid.NewGuid().ToString();
}

