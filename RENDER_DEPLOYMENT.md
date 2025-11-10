# Step-by-Step Guide: Deploy to Render

## Prerequisites
- ✅ Code pushed to GitHub (already done!)
- ✅ GitHub account
- ✅ Render account (we'll create this)

---

## Step 1: Create Render Account

1. Go to [render.com](https://render.com)
2. Click **"Get Started for Free"** or **"Sign Up"**
3. Choose **"Sign up with GitHub"** (recommended - easier integration)
4. Authorize Render to access your GitHub account
5. Complete the signup process

---

## Step 2: Create New Web Service

1. Once logged in, you'll see the Render Dashboard
2. Click the **"New +"** button (top right)
3. Select **"Web Service"** from the dropdown

---

## Step 3: Connect Your Repository

1. You'll see a list of your GitHub repositories
2. Find and click on **"DotNetTube"** repository
3. If you don't see it, click **"Configure account"** to grant access
4. Click **"Connect"** next to your repository

---

## Step 4: Configure Your Service

Fill in the following details:

### Basic Settings:
- **Name**: `dotnet-notes-sales` (or any name you prefer)
- **Region**: Choose closest to your users (e.g., `Singapore` for India)
- **Branch**: `main` (should be selected by default)
- **Root Directory**: Leave empty (or `src/NoteSalesWeb` if you want to be specific)

### Build & Deploy:
- **Environment**: Select **".NET Core"** from dropdown
- **Build Command**: 
  ```
  dotnet publish src/NoteSalesWeb/NoteSalesWeb.csproj -c Release -o ./publish
  ```
- **Start Command**: 
  ```
  dotnet ./publish/NoteSalesWeb.dll
  ```

### Instance Type:
- **Free**: 750 hours/month, spins down after 15 min inactivity (good for testing)
- **Starter ($7/month)**: Always on, better performance (recommended for production)
- Choose **Free** for now (you can upgrade later)

---

## Step 5: Add Environment Variables

1. Scroll down to **"Environment Variables"** section
2. Click **"Add Environment Variable"**
3. Add these variables one by one:

   **Variable 1:**
   - **Key**: `ASPNETCORE_ENVIRONMENT`
   - **Value**: `Production`
   - Click **"Add"**

   **Variable 2:**
   - **Key**: `ASPNETCORE_URLS`
   - **Value**: `http://+:10000`
   - Click **"Add"`

   **Variable 3:**
   - **Key**: `Razorpay__WebhookSecret`
   - **Value**: (Your Razorpay webhook secret - get this from Razorpay dashboard)
   - Click **"Add"**

   **Variable 4 (Optional - for custom port):**
   - **Key**: `PORT`
   - **Value**: `10000`
   - Click **"Add"**

---

## Step 6: Deploy!

1. Scroll to the bottom of the page
2. Click **"Create Web Service"**
3. Render will start building your application
4. You'll see build logs in real-time
5. First deployment takes 5-10 minutes

---

## Step 7: Get Your Application URL

1. Once deployment completes, you'll see:
   - **Status**: Live ✅
   - **URL**: `https://dotnet-notes-sales.onrender.com` (or your custom name)

2. Click on the URL to open your application
3. Your landing page should be live!

---

## Step 8: Configure Razorpay Webhook

1. **Get your Render URL**: Copy the URL from Step 7
2. **Go to Razorpay Dashboard**: [dashboard.razorpay.com](https://dashboard.razorpay.com)
3. Navigate to **Settings** → **Webhooks**
4. Click **"Add New Webhook"** or edit existing webhook
5. **Webhook URL**: `https://your-app-name.onrender.com/api/payment/webhook`
   - Replace `your-app-name` with your actual Render app name
6. **Select Events**:
   - ✅ `payment.captured`
   - ✅ `payment.failed` (optional)
7. **Copy Webhook Secret** and add it to Render environment variables:
   - Go back to Render Dashboard
   - Click on your service
   - Go to **Environment** tab
   - Add/Update: `Razorpay__WebhookSecret` = (your secret)
   - Click **"Save Changes"**
   - Render will automatically redeploy

---

## Step 9: Test Your Deployment

1. **Visit your live URL**: `https://your-app.onrender.com`
2. **Test the landing page**: Should show all 3 pricing plans
3. **Click a payment button**: Should open Razorpay payment link
4. **Test webhook** (after payment):
   - Make a test payment
   - Check Render logs: Dashboard → Your Service → **Logs** tab
   - Should see webhook received message

---

## Step 10: Custom Domain (Optional)

1. In Render Dashboard, go to your service
2. Click **"Settings"** tab
3. Scroll to **"Custom Domains"**
4. Click **"Add Custom Domain"**
5. Enter your domain (e.g., `notes.yourdomain.com`)
6. Follow DNS configuration instructions
7. Update Razorpay webhook URL with new domain

---

## Troubleshooting

### Build Fails:
- Check build logs in Render dashboard
- Verify build command is correct
- Ensure all dependencies are in `.csproj`

### App Won't Start:
- Check start command
- Verify PORT environment variable
- Check logs for errors

### Webhook Not Working:
- Verify webhook URL in Razorpay matches Render URL
- Check webhook secret matches in both places
- View Render logs for webhook requests
- Test webhook URL manually (should return 400 Bad Request for GET)

### App Spins Down (Free Tier):
- Free tier apps sleep after 15 min inactivity
- First request after sleep takes ~30 seconds
- Upgrade to Starter ($7/month) for always-on

---

## Monitoring & Logs

1. **View Logs**: Dashboard → Your Service → **Logs** tab
2. **Metrics**: Dashboard → Your Service → **Metrics** tab
3. **Events**: Dashboard → Your Service → **Events** tab

---

## Updating Your App

1. **Make changes locally**
2. **Commit and push to GitHub**:
   ```bash
   git add .
   git commit -m "Update description"
   git push
   ```
3. **Render automatically detects changes** and redeploys
4. **Monitor deployment** in Render dashboard

---

## Cost Estimate

- **Free Tier**: $0/month (750 hours, spins down)
- **Starter**: $7/month (always on, 512MB RAM)
- **Standard**: $25/month (1GB RAM, better performance)

For production with payment processing, **Starter ($7/month)** is recommended.

---

## Next Steps After Deployment

1. ✅ Test payment flow end-to-end
2. ✅ Verify webhook receives payments
3. ✅ Test download functionality
4. ✅ Set up email notifications (optional)
5. ✅ Configure custom domain (optional)
6. ✅ Set up monitoring alerts

---

## Need Help?

- Render Docs: https://render.com/docs
- Render Support: support@render.com
- .NET on Render: https://render.com/docs/deploy-dotnet-core

---

**Your app should now be live! 🚀**
