using Microsoft.AspNetCore.Mvc;
using NoteSalesWeb.Models;
using NoteSalesWeb.Services;

namespace NoteSalesWeb.Controllers;

public class ThankYouController : Controller
{
    private readonly PurchaseService _purchaseService;
    private readonly ILogger<ThankYouController> _logger;

    public ThankYouController(PurchaseService purchaseService, ILogger<ThankYouController> logger)
    {
        _purchaseService = purchaseService;
        _logger = logger;
    }

    public IActionResult Index(string? payment_id, string? plan)
    {
        ViewData["Title"] = "Thank You for Your Purchase";

        Purchase? purchase = null;
        PlanDetails? planDetails = null;

        // Try to get purchase by payment ID
        if (!string.IsNullOrEmpty(payment_id))
        {
            purchase = _purchaseService.GetPurchaseByPaymentId(payment_id);
        }

        // If no purchase found, try to determine plan from query parameter
        if (purchase == null && !string.IsNullOrEmpty(plan))
        {
            var plans = _purchaseService.GetPlans();
            planDetails = plans.FirstOrDefault(p => 
                p.Plan.ToString().Equals(plan, StringComparison.OrdinalIgnoreCase));
        }
        else if (purchase != null)
        {
            var plans = _purchaseService.GetPlans();
            planDetails = plans.FirstOrDefault(p => p.Plan == purchase.Plan);
        }

        var model = new ThankYouViewModel
        {
            Purchase = purchase,
            PlanDetails = planDetails,
            DownloadToken = purchase?.DownloadToken
        };

        return View(model);
    }

    [HttpGet("download")]
    public IActionResult Download(string? token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return RedirectToAction("Index", "Home");
        }

        var purchase = _purchaseService.GetPurchaseByToken(token);
        if (purchase == null || !purchase.IsVerified)
        {
            TempData["Error"] = "Invalid or expired download link.";
            return RedirectToAction("Index", "Home");
        }

        var plans = _purchaseService.GetPlans();
        var planDetails = plans.FirstOrDefault(p => p.Plan == purchase.Plan);

        ViewData["Title"] = "Download Your Notes";
        var model = new ThankYouViewModel
        {
            Purchase = purchase,
            PlanDetails = planDetails,
            DownloadToken = token
        };

        return View("Download", model);
    }

    [HttpGet("access")]
    public IActionResult Access()
    {
        ViewData["Title"] = "Access Your Purchase";
        return View();
    }

    [HttpPost("access")]
    public IActionResult Access(string? paymentId, string? email)
    {
        ViewData["Title"] = "Access Your Purchase";

        Purchase? purchase = null;

        // Try to find by payment ID first
        if (!string.IsNullOrWhiteSpace(paymentId))
        {
            purchase = _purchaseService.GetPurchaseByPaymentId(paymentId);
        }

        // If not found, try by email
        if (purchase == null && !string.IsNullOrWhiteSpace(email))
        {
            purchase = _purchaseService.GetPurchaseByEmail(email);
        }

        if (purchase == null || !purchase.IsVerified)
        {
            TempData["Error"] = "Purchase not found. Please check your payment ID or email. If you just made a payment, please wait a few moments for it to be processed.";
            return View();
        }

        // Redirect to thank you page with payment ID
        return RedirectToAction("Index", new { payment_id = purchase.RazorpayPaymentId });
    }
}

