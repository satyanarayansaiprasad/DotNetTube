using Microsoft.AspNetCore.Mvc;
using NoteSalesWeb.Models;
using NoteSalesWeb.Services;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace NoteSalesWeb.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly PurchaseService _purchaseService;
    private readonly ILogger<PaymentController> _logger;
    private readonly IConfiguration _configuration;

    public PaymentController(PurchaseService purchaseService, ILogger<PaymentController> logger, IConfiguration configuration)
    {
        _purchaseService = purchaseService;
        _logger = logger;
        _configuration = configuration;
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> Webhook()
    {
        try
        {
            var json = await new StreamReader(Request.Body).ReadToEndAsync();
            var webhookData = JsonSerializer.Deserialize<JsonElement>(json);

            var eventType = webhookData.GetProperty("event").GetString();
            var payload = webhookData.GetProperty("payload");

            // Verify webhook signature (add your Razorpay webhook secret in appsettings.json)
            var webhookSecret = _configuration["Razorpay:WebhookSecret"];
            if (!string.IsNullOrEmpty(webhookSecret))
            {
                var signature = Request.Headers["X-Razorpay-Signature"].ToString();
                if (string.IsNullOrEmpty(signature))
                {
                    _logger.LogWarning("Missing webhook signature header");
                    return BadRequest("Missing signature");
                }
                if (!VerifySignature(json, signature, webhookSecret))
                {
                    _logger.LogWarning("Invalid webhook signature");
                    return BadRequest("Invalid signature");
                }
            }
            else
            {
                _logger.LogWarning("Webhook secret not configured - skipping signature verification");
            }

            if (eventType == "payment.captured")
            {
                var payment = payload.GetProperty("payment").GetProperty("entity");
                var paymentId = payment.GetProperty("id").GetString();
                var amount = payment.GetProperty("amount").GetInt64() / 100; // Convert paise to rupees
                var email = payment.GetProperty("email").GetString() ?? "";
                var orderId = payment.GetProperty("order_id").GetString() ?? "";

                // Determine plan based on amount
                PurchasePlan plan = amount switch
                {
                    299 => PurchasePlan.Starter,
                    599 => PurchasePlan.Career,
                    999 => PurchasePlan.Elite,
                    _ => PurchasePlan.Starter
                };

                var purchase = new Purchase
                {
                    Email = email,
                    Plan = plan,
                    Amount = amount,
                    RazorpayPaymentId = paymentId ?? "",
                    RazorpayOrderId = orderId,
                    IsVerified = true
                };

                _purchaseService.SavePurchase(purchase);

                _logger.LogInformation($"Payment verified: {paymentId} for plan {plan}");

                return Ok(new { status = "success" });
            }

            return Ok(new { status = "ignored" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing webhook");
            return StatusCode(500, new { error = "Internal server error" });
        }
    }

    private bool VerifySignature(string payload, string signature, string secret)
    {
        try
        {
            var key = Encoding.UTF8.GetBytes(secret);
            var message = Encoding.UTF8.GetBytes(payload);
            using var hmac = new HMACSHA256(key);
            var hash = hmac.ComputeHash(message);
            var computedSignature = Convert.ToHexString(hash).ToLower();
            return computedSignature == signature.ToLower();
        }
        catch
        {
            return false;
        }
    }
}

