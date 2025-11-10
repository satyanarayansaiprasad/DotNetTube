# DotNetTube - .NET Mastery Notes Sales Platform

A comprehensive ASP.NET Core MVC application for selling .NET learning notes with integrated Razorpay payment processing.

## Features

- 🎯 Beautiful landing page with course-selling psychology
- 💳 Integrated Razorpay payment links (₹299, ₹599, ₹999 plans)
- 📦 Automated purchase verification via webhooks
- 🔐 Secure token-based download system
- 📱 Responsive design with modern UI
- ✅ Thank you pages with download access

## Project Structure

```
dot net/
├── src/
│   ├── NoteSalesWeb/          # Main MVC application
│   │   ├── Controllers/       # Payment & ThankYou controllers
│   │   ├── Models/            # Purchase & Plan models
│   │   ├── Services/          # Purchase management service
│   │   └── Views/             # Razor views
│   └── Playground/            # Console playground project
├── it notes/                  # Learning module notes (15 modules)
└── LearnDotNet.sln           # Solution file
```

## Tech Stack

- **.NET 9.0** - Latest .NET framework
- **ASP.NET Core MVC** - Web framework
- **Razorpay** - Payment gateway integration
- **Bootstrap 5** - UI framework

## Setup Instructions

### Prerequisites

- .NET 9.0 SDK
- Razorpay account with payment links configured

### Local Development

1. Clone the repository:
   ```bash
   git clone https://github.com/satyanarayansaiprasad/DotNetTube.git
   cd DotNetTube
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Configure Razorpay webhook secret in `appsettings.json`:
   ```json
   {
     "Razorpay": {
       "WebhookSecret": "your_webhook_secret_here"
     }
   }
   ```

4. Run the application:
   ```bash
   dotnet run --project src/NoteSalesWeb/NoteSalesWeb.csproj
   ```

5. Open browser: `http://localhost:5238`

## Payment Plans

- **Starter Blueprint** - ₹299
- **Career Accelerator** - ₹599 (Best Value)
- **Elite Placement Suite** - ₹999

## Razorpay Configuration

1. Create payment links in Razorpay Dashboard
2. Configure webhook URL: `https://your-domain.com/api/payment/webhook`
3. Add webhook secret to `appsettings.json`
4. Select events: `payment.captured`

See `RAZORPAY_SETUP.md` for detailed instructions.

## Deployment

### Render (Recommended)

- Supports .NET natively
- Free tier available
- Easy GitHub integration
- Automatic HTTPS

### Other Options

- Railway
- Fly.io
- Azure App Service
- AWS Elastic Beanstalk

## Learning Modules

The `it notes/` folder contains 15 comprehensive .NET learning modules:

1. Orientation & Setup
2. C# Fundamentals
3. Control Flow & Data Structures
4. Object-Oriented Programming
5. Advanced C# Features
6. Memory & Runtime Management
7. .NET Libraries & Tooling
8. Data Access & Persistence
9. Testing & Quality
10. ASP.NET Core Web APIs
11. Web UI & Blazor
12. Microservices & Messaging
13. Cloud & DevOps with Azure
14. Security & Compliance
15. Interview Mastery & Capstone

## License

This project is private and proprietary.

## Author

Satyanarayana Sai Prasad

---

Built with ❤️ using .NET

