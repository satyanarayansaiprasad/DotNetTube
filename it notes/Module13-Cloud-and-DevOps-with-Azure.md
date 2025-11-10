# Module 13: Cloud & DevOps with Azure

> **Goal:** Deploy, operate, and automate .NET applications in the cloud using Azure services and modern DevOps practices.

---

## Learning Outcomes

| # | Outcome | Evidence |
|---|---------|----------|
| 1 | Deploy .NET apps to Azure App Service, Azure Functions, and Containers | Deployed endpoints + screenshots |
| 2 | Manage infrastructure as code with Bicep/Terraform | IaC repository with deployment history |
| 3 | Implement CI/CD pipelines (GitHub Actions/Azure DevOps) | Pipeline YAML + successful runs |
| 4 | Configure monitoring, logging, and alerting (App Insights) | Dashboard with custom metrics |
| 5 | Utilize cloud data services (Azure SQL, Cosmos DB, Storage) | Proof-of-concept integration |

---

## Azure Service Overview

- **Compute Options:** App Service, Azure Functions, Azure Container Apps, AKS.
- **Data Storage:** Azure SQL Database, Cosmos DB, Table Storage, Blob Storage.
- **Networking:** Application Gateway, Front Door, VNet integration.
- **Security:** Azure Key Vault, Managed Identities, Azure AD.

---

## Detailed Notes

### 1. Azure Compute & Hosting Options

- **Azure App Service:** PaaS for web APIs and sites. Supports deployment slots, autoscaling, integrated authentication. Publish via zip deploy or container images.
- **Azure Functions:** Serverless compute for event-driven workloads. Plans: Consumption (pay per execution), Premium (pre-warmed instances), Dedicated (App Service plan).
- **Azure Container Apps (ACA):** Run containers with Dapr integration, scale to zero, integrated ingress. Ideal for microservices without full Kubernetes complexity.
- **Azure Kubernetes Service (AKS):** Managed Kubernetes; offers autoscaling, node pools, integrated Azure CNI networking.
- **Static Web Apps:** Deploy static front-ends (Blazor WASM, SPA) with integrated API endpoints (Functions).

#### Quick Check (5 Questions)
1. When should you choose App Service over Container Apps?
2. How do deployment slots enable zero-downtime releases?
3. What scenarios favor Azure Functions Premium plan?
4. How does ACA simplify microservice deployment compared to AKS?
5. What limitations should you consider with Static Web Apps?

---

### 2. Data Services, Networking & Security

- **Azure SQL Database:** Managed relational service; configure DTU/vCore models, geo-replication, automatic backups.
- **Cosmos DB:** Multi-model NoSQL database with partitioning, global distribution, consistency levels.
- **Storage Accounts:** Blob (object storage), Table (key-value), Queue (messaging), File shares.
- **Networking:** Virtual Networks (VNet) isolate resources; use Private Endpoints for secure data access. Application Gateway and Front Door provide load balancing and WAF.
- **Security:** Managed identities provide Azure resource authentication. Azure Key Vault stores secrets/keys/certs; integrate with .NET via `DefaultAzureCredential`.

#### Quick Check (5 Questions)
1. How do you choose a partition key in Cosmos DB?
2. What advantages do private endpoints offer compared to public endpoints?
3. When should you use Azure Table Storage over Cosmos DB?
4. How do managed identities simplify secret management?
5. Why is geo-replication important for Azure SQL?

---

### 3. CI/CD Pipelines & DevOps Automation

- **GitHub Actions:** Define workflows (`.github/workflows`) for build/test/deploy. Use actions like `actions/setup-dotnet`, `Azure/webapps-deploy`.
- **Azure DevOps Pipelines:** YAML or classic pipelines; integrate with Boards, Repos, Artifacts.
- **Pipeline Stages:** Build (restore, build, test), Publish artifacts (zip, container), Deploy (App Service, Functions, AKS).
- **Secrets Management:** Store secrets in GitHub Secrets or Azure Key Vault. Use `azure/login` action with OIDC for passwordless deployments.
- **Quality Gates:** Integrate linting, tests, coverage, and approvals; use environments in GitHub for manual gates.

#### Quick Check (5 Questions)
1. How do you authenticate GitHub Actions to Azure without storing secrets?
2. What steps should a .NET build stage include?
3. How can you reuse pipeline logic across repositories?
4. Why are deployment approvals important in production environments?
5. What is the purpose of publishing artifacts before deployment?

---

### 4. Infrastructure as Code (IaC)

- **Bicep:** Declarative language for Azure resources. Supports modules, parameters, outputs. Use `az deployment group create -f main.bicep`.
- **Terraform:** Cloud-agnostic; maintain state in Azure Storage. Use modules for reuse, `terraform plan/apply` for change control.
- **ARM Templates:** JSON-based, superseded by Bicep but still supported.
- **GitOps:** Store IaC in Git; deploy via pipelines or Flux/ArgoCD on AKS.
- **Testing IaC:** Use `what-if` deployments, Terraform plan, and automated validation in pipelines.

#### Quick Check (5 Questions)
1. How does Bicep improve over raw ARM templates?
2. Where should Terraform state be stored in Azure?
3. What does `az deployment group what-if` do?
4. How do modules improve maintainability in IaC?
5. Why should IaC definitions live alongside application code?

---

### 5. Monitoring, Observability & Cost Control

- **Application Insights:** Collects telemetry, requests, dependencies, live metrics. Configure sampling, custom events, availability tests.
- **Azure Monitor:** Central platform for logs, metrics, alerts. Log Analytics queries (KQL) analyze data.
- **Dashboards:** Create custom dashboards for operations; integrate with Power BI.
- **Cost Management:** Budgets, alerts, recommendations. Use tagging for cost allocation. Leverage Azure Advisor for optimization tips.
- **Incident Response:** Define runbooks, use Azure Automation or Logic Apps for remediation, integrate with PagerDuty/Teams.

#### Quick Check (5 Questions)
1. How do you enable distributed tracing for a .NET API on Azure?
2. What is KQL and where is it used?
3. How can sampling reduce Application Insights costs?
4. Why is tagging important for cost management?
5. What steps form an effective incident response runbook?

---

## Hands-On Roadmap

1. **Azure Setup**
   - Create Azure account, resource groups (`rg-dotnet-learning-dev`).
   - Install Azure CLI: `az login`, `az account set`.

2. **Deploy Web API to App Service**
   - `dotnet publish` to zip.
   - `az webapp up --runtime "DOTNET|8.0"`.
   - Configure deployment slots (staging, production).

3. **Serverless Functions**
   - Create HTTP-triggered Azure Function (`func init`).
   - Bind to queue/storage triggers.
   - Deploy with `func azure functionapp publish`.

4. **Containerization & ACA**
   - Dockerize API with multi-stage build.
   - Push to Azure Container Registry (ACR).
   - Deploy to Azure Container Apps or AKS.

5. **Database Integration**
   - Provision Azure SQL, configure firewall rules.
   - Apply EF Core migrations via pipeline.
   - Explore Cosmos DB (SQL API) for distributed scenarios.

6. **Monitoring & Alerts**
   - Enable Application Insights.
   - Configure distributed tracing, custom events.
   - Create alert rules (response time, error rate).

7. **CI/CD Pipeline**
   - GitHub Actions workflow: build, test, publish.
   - Deploy via `azure/webapps-deploy` action.
   - Use environments with approvals.
   - Record secrets management with GitHub Secrets/Azure Key Vault.

8. **Infrastructure as Code**
   - Author Bicep file for App Service + SQL + Insights.
   - Deploy via `az deployment group create`.
   - Terraform alternative: state management, modules.

9. **Cost Management**
   - Set budgets, alerts.
   - Use Azure Pricing Calculator.
   - Cleanup script to delete unused resources.

---

## Interview Check-In

- “How do you deploy a .NET API to Azure with zero downtime?”
- “Explain CI/CD pipeline stages for a .NET project.”
- “How do you secure application secrets in Azure?”
- “What’s the difference between Azure Functions consumption vs premium plans?”
- Scenario: design multi-region deployment with failover.

---

## Best Practices

- Use Managed Identity instead of storing credentials.
- Apply blue/green or canary deployments using slots.
- Automate health checks and warm-up.
- Tag resources for governance (`Environment`, `Owner`).
- Document runbooks for incident response.

---

## Resources

- [Azure for .NET Developers](https://learn.microsoft.com/azure/developer/dotnet/)
- [Azure CLI Reference](https://learn.microsoft.com/cli/azure/)
- [GitHub Actions for Azure](https://learn.microsoft.com/azure/developer/github/)
- [Bicep Documentation](https://learn.microsoft.com/azure/azure-resource-manager/bicep/)
- [Azure Well-Architected Framework](https://learn.microsoft.com/azure/architecture/framework/)

---

## Hindi Video Tutorials

- [Azure App Service Deployment in Hindi – WsCube Tech](https://www.youtube.com/watch?v=njcdpShUMpU)
- [GitHub Actions for .NET Deployment (Hindi) – CodeDecode](https://www.youtube.com/watch?v=M80p2qxGn8E)
- [Azure DevOps CI/CD Pipeline in Hindi – Geeky Shows](https://www.youtube.com/watch?v=pCVh0b07XH4)

---

> ✅ **Completion Criteria:** You can deploy and operate .NET workloads on Azure with automated pipelines, infrastructure as code, and observability baked in.

