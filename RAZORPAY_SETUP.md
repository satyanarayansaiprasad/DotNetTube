# Razorpay Payment Integration Setup Guide

## ✅ What's Been Implemented

1. **Updated Pricing**: ₹299 (Starter), ₹599 (Career), ₹999 (Elite)
2. **Payment Links**: Integrated your Razorpay payment links
3. **Thank You Page**: Automatic redirect after payment
4. **Webhook Handler**: Verifies payments and grants access
5. **Download System**: Secure token-based download access

## 🔧 Razorpay Dashboard Configuration

### Step 1: Configure Webhook URL

1. Log in to [Razorpay Dashboard](https://dashboard.razorpay.com)
2. Go to **Settings** → **Webhooks**
3. Click **Add New Webhook**
4. Enter your webhook URL:
   ```
   https://your-domain.com/api/payment/webhook
   ```
   For local testing, use a tool like [ngrok](https://ngrok.com):
   ```
   ngrok http 5238
   ```
   Then use: `https://your-ngrok-url.ngrok.io/api/payment/webhook`

5. Select events to listen for:
   - ✅ `payment.captured`
   - ✅ `payment.failed` (optional)

6. Copy the **Webhook Secret** and add it to `appsettings.json`:
   ```json
   "Razorpay": {
     "WebhookSecret": "your_webhook_secret_here"
   }
   ```

### Step 2: Update Payment Links (Optional Redirect)

Since Razorpay Payment Links don't have a built-in redirect option in the UI, we'll handle redirects via webhook. However, you can add a note in the payment link description:

1. Go to **Payment Links** in Razorpay Dashboard
2. Edit each payment link
3. In the **Description** field, add:
   ```
   After payment, you will be redirected to: https://your-domain.com/thank-you
   ```

## 🚀 How It Works

### Payment Flow:

1. **User clicks plan button** → Opens Razorpay payment link
2. **User completes payment** → Razorpay processes payment
3. **Razorpay sends webhook** → Your server verifies and saves purchase
4. **User redirected** → Thank you page with download access

### Manual Redirect (If webhook is delayed):

Users can manually visit:
```
https://your-domain.com/thank-you?payment_id=pay_xxxxx
```

The system will verify the payment and grant access.

## 📝 Testing

### Test Mode:

1. Use Razorpay test mode
2. Use test cards: `4111 1111 1111 1111`
3. Webhook will be called automatically

### Production:

1. Switch to live mode in Razorpay
2. Update webhook URL to production domain
3. Test with a small real payment (₹1 if possible)

## 🔐 Security Notes

- Webhook signature verification is implemented
- Download tokens are unique per purchase
- Payments are verified before granting access
- Store webhook secret securely (use Azure Key Vault or environment variables in production)

## 📦 File Structure

```
src/NoteSalesWeb/
├── Controllers/
│   ├── PaymentController.cs    # Webhook handler
│   └── ThankYouController.cs   # Thank you & download pages
├── Models/
│   ├── Purchase.cs             # Purchase data model
│   ├── PurchasePlan.cs         # Plan definitions
│   └── ThankYouViewModel.cs    # View model
├── Services/
│   └── PurchaseService.cs      # Purchase management
└── Views/
    └── ThankYou/
        ├── Index.cshtml        # Thank you page
        └── Download.cshtml     # Download page
```

## 🎯 Next Steps

1. **Deploy your application** to a hosting service (Azure, AWS, etc.)
2. **Configure webhook URL** in Razorpay dashboard
3. **Add webhook secret** to production `appsettings.json` or environment variables
4. **Test the complete flow** with a real payment
5. **Set up email notifications** (optional) to send download links automatically

## 📧 Email Automation (Optional)

You can enhance the system by sending automatic emails with download links:

1. Add email service (SendGrid, SMTP, etc.)
2. In `PaymentController.cs`, after saving purchase, send email with download link
3. Include: `https://your-domain.com/thank-you/download?token={downloadToken}`

## 🆘 Troubleshooting

### Webhook not receiving events:
- Check webhook URL is accessible (use ngrok for local testing)
- Verify webhook secret matches
- Check Razorpay dashboard → Webhooks → Logs for errors

### Payment not verified:
- Check webhook is receiving `payment.captured` event
- Verify amount mapping (299, 599, 999 in rupees)
- Check application logs for errors

### Download link not working:
- Verify purchase is saved with `IsVerified = true`
- Check download token is correct
- Ensure user is accessing correct URL

---

**Need Help?** Check Razorpay documentation: https://razorpay.com/docs/

