using NoteSalesWeb.Models;

namespace NoteSalesWeb.Services;

public class PurchaseService
{
    private static readonly Dictionary<string, Purchase> _purchases = new();
    private static readonly object _lock = new();

    public void SavePurchase(Purchase purchase)
    {
        lock (_lock)
        {
            _purchases[purchase.RazorpayPaymentId] = purchase;
        }
    }

    public Purchase? GetPurchaseByPaymentId(string paymentId)
    {
        lock (_lock)
        {
            return _purchases.TryGetValue(paymentId, out var purchase) ? purchase : null;
        }
    }

    public Purchase? GetPurchaseByToken(string token)
    {
        lock (_lock)
        {
            return _purchases.Values.FirstOrDefault(p => p.DownloadToken == token && p.IsVerified);
        }
    }

    public List<PlanDetails> GetPlans()
    {
        return new List<PlanDetails>
        {
            new PlanDetails
            {
                Plan = PurchasePlan.Starter,
                Name = "Starter Blueprint",
                Price = 299,
                PaymentLink = "https://rzp.io/rzp/3q5im4F",
                Modules = new List<string>
                {
                    "Module 01: Orientation & Setup",
                    "Module 02: C# Fundamentals",
                    "Module 03: Control Flow & Data Structures",
                    "Module 04: Object-Oriented Programming",
                    "Module 05: Advanced C# Features",
                    "Module 06: Memory & Runtime Management",
                    "Module 07: .NET Libraries & Tooling",
                    "Module 08: Data Access & Persistence",
                    "Module 09: Testing & Quality",
                    "Module 10: ASP.NET Core Web APIs",
                    "Module 11: Web UI & Blazor",
                    "Module 12: Microservices & Messaging",
                    "Module 13: Cloud & DevOps with Azure",
                    "Module 14: Security & Compliance",
                    "Module 15: Interview Mastery & Capstone"
                },
                Features = new List<string>
                {
                    "Core 15 modules with structured notes",
                    "Daily action checklists & recap prompts",
                    "Hindi video links for each topic",
                    "Printable progress tracker"
                }
            },
            new PlanDetails
            {
                Plan = PurchasePlan.Career,
                Name = "Career Accelerator",
                Price = 599,
                PaymentLink = "https://rzp.io/rzp/VZQm8Np",
                Modules = new List<string>
                {
                    "All Starter Blueprint modules",
                    "Interview question bank (5 per micro-topic)",
                    "Case-study based practice scenarios",
                    "Portfolio storytelling prompts"
                },
                Features = new List<string>
                {
                    "Everything in Starter Blueprint",
                    "Interview question bank (5 per micro-topic)",
                    "Case-study based practice scenarios",
                    "Portfolio storytelling prompts"
                }
            },
            new PlanDetails
            {
                Plan = PurchasePlan.Elite,
                Name = "Elite Placement Suite",
                Price = 999,
                PaymentLink = "https://rzp.io/rzp/76tChXho",
                Modules = new List<string>
                {
                    "All Career Accelerator modules",
                    "System design cheat sheets + ADR templates",
                    "DevOps automation blueprints & IaC samples",
                    "Lifetime updates + priority upgrade alerts"
                },
                Features = new List<string>
                {
                    "Everything in Career Accelerator",
                    "System design cheat sheets + ADR templates",
                    "DevOps automation blueprints & IaC samples",
                    "Lifetime updates + priority upgrade alerts"
                }
            }
        };
    }
}

