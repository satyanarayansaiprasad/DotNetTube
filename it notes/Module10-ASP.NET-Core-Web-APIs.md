# Module 10: ASP.NET Core Web APIs

> **Goal:** Build production-ready RESTful APIs with ASP.NET Core, covering routing, controllers, validation, middleware, and API lifecycle management.

---

## Learning Outcomes

| # | Outcome | Evidence |
|---|---------|----------|
| 1 | Scaffold and configure an ASP.NET Core Web API project | Git repo with `WeatherForecast` replacement |
| 2 | Design clean routing, controllers, and action methods | API adhering to REST conventions |
| 3 | Implement model binding, validation, and filters | Validation failure responses documented |
| 4 | Integrate authentication/authorization (JWT, policies) | Secure endpoints + Postman collection |
| 5 | Document APIs with OpenAPI/Swagger and versioning | Swagger UI + versioned endpoints |
| 6 | Introduce cross-cutting concerns (logging, caching, error handling) | Middleware pipeline diagram |

---

## Architecture Overview

- Program bootstrap (`Program.cs` minimal hosting).
- Middleware pipeline order, built-in vs custom.
- Controllers vs minimal APIs vs endpoint routing.
- Model binding lifecycle, input/output formatters.
- Filters: authorization, resource, action, exception, result filters.

---

## Detailed Notes

### 1. Hosting Model & Middleware Pipeline

ASP.NET Core apps bootstrap through **generic host** in `Program.cs`. The host configures logging, configuration, and dependency injection before building the middleware pipeline.

- **Middleware order matters:** Each middleware can short-circuit or pass requests down the pipeline using `await next()`; order determines behaviors such as authentication before authorization.
- **Common middleware:** `UseRouting`, `UseAuthentication`, `UseAuthorization`, `UseEndpoints`. Place custom middleware using `app.Use(async (context, next) => { ... })`.
- **Exception Handling:** Global handling via `UseExceptionHandler`, return `ProblemDetails` responses.
- **Minimal Hosting Model:** .NET 6+ uses top-level statements for concise `Program.cs`.
- **Kestrel Server:** Default cross-platform web server; configure limits (request size, timeouts).

#### Quick Check (5 Questions)
1. Why does middleware order matter in ASP.NET Core?
2. How do you configure global exception handling for APIs?
3. When should you inject custom middleware into the pipeline?
4. What role does `UseRouting` play in request processing?
5. How do you configure Kestrel for request size limits?

---

### 2. Routing, Controllers & Action Results

Routing maps incoming HTTP requests to controller actions.

- **Endpoint Routing:** Define routes via attribute routing (`[HttpGet("api/products/{id:int}")]`) or conventional routes.
- **Controller structure:** Derive from `ControllerBase`. Use `ActionResult<T>` for typed responses with proper status codes.
- **Model-View separation:** Use DTOs for input/output; map from domain models using AutoMapper or manual mapping.
- **Action Results:** Return `Ok()`, `CreatedAtAction()`, `NoContent()`, `NotFound()`, etc. Support asynchronous actions returning `Task<ActionResult<T>>`.
- **Minimal APIs:** Suitable for lightweight endpoints; controllers better for complex APIs needing filters or conventions.

#### Quick Check (5 Questions)
1. How does attribute routing improve clarity over conventional routing?
2. When should an action return `ActionResult<T>` vs `IActionResult`?
3. Why use DTOs instead of exposing domain entities directly?
4. Provide an example of returning `CreatedAtAction`.
5. When are minimal APIs a better fit than MVC controllers?

---

### 3. Model Binding, Validation & Filters

Model binding converts HTTP input into CLR types. Validation ensures data integrity.

- **Binding sources:** `[FromBody]`, `[FromQuery]`, `[FromRoute]`, `[FromHeader]`. By default, complex types bind from body, simple types from route/query.
- **Validation:** Data annotations (`[Required]`, `[StringLength]`) or FluentValidation for complex rules. Invalid model state returns 400 automatically when `ApiController` attribute is present.
- **Filters:** Execute cross-cutting logic. Use action filters for logging, result filters to mutate responses, exception filters to handle errors.
- **Problem Details:** Standardized error responses per RFC 7807; configure `options.InvalidModelStateResponseFactory`.
- **File uploads & multipart:** Use `IFormFile`, configure limits (`[RequestSizeLimit]`).

#### Quick Check (5 Questions)
1. How does the `[ApiController]` attribute change model binding behavior?
2. When would you choose FluentValidation over data annotations?
3. What scenarios benefit from custom action filters?
4. How do you return standardized validation errors?
5. How do you handle file uploads securely in APIs?

---

### 4. Authentication & Authorization

Secure APIs with JWT, OAuth2, or API keys.

- **Authentication:** Configure `AddAuthentication` with schemes (JWT bearer). Validate tokens using issuer, audience, signing keys.
- **Authorization:** Decorate actions with `[Authorize]`, `[AllowAnonymous]`. Policy-based authorization uses requirements and handlers for complex rules.
- **Role vs Policy:** Roles check static membership; policies evaluate claims or custom logic.
- **Identity Providers:** Azure AD, IdentityServer, Auth0 supply tokens. Use OpenID Connect to obtain user info.
- **Secure Secrets:** Use `dotnet user-secrets` locally, Key Vault or AWS Secrets Manager in production.

#### Quick Check (5 Questions)
1. What are the steps to configure JWT authentication in ASP.NET Core?
2. How do policies extend authorization beyond roles?
3. When should `[AllowAnonymous]` be used?
4. How do you validate JWT tokens to prevent replay attacks?
5. Where should API secrets be stored in production?

---

### 5. Documentation, Versioning & Observability

Well-documented APIs improve adoption.

- **Swagger/OpenAPI:** Add `AddEndpointsApiExplorer()` and `AddSwaggerGen()`. Include XML comments for summaries and remarks.
- **Versioning:** Use `Asp.Versioning` to support URL, query-string, or header-based versions. Deprecate old versions gracefully.
- **Response Caching & Rate Limiting:** Use `[ResponseCache]` and built-in rate limiting middleware to protect resources.
- **Health Checks:** `services.AddHealthChecks()` with endpoints `/health`. Include readiness/liveness checks for container orchestrators.
- **Telemetry:** Integrate OpenTelemetry exporters, log correlation IDs. Add metrics (request duration, status counts).

#### Quick Check (5 Questions)
1. How do you enable Swagger UI only in development environments?
2. What strategies support API versioning without breaking clients?
3. Why are health check endpoints important in microservices?
4. How can you correlate logs for a single API request?
5. When should you enable rate limiting, and how do you configure it?

---

## Hands-On Build Plan

1. **Project Setup**
   ```bash
   dotnet new webapi -n CleanTodoApi
   ```
   - Remove sample controller, create domain-specific controllers.
   - Configure logging, Serilog integration.

2. **Entity & DTO Definition**
   - Use `record` DTOs, `AutoMapper` for mapping.
   - Introduce `ProblemDetails` responses for errors.

3. **Routing & Actions**
   - Attribute routing with constraints.
   - Supporting pagination (`?page=1&pageSize=20`).
   - Return proper HTTP status codes (201, 204, 404).

4. **Validation Layer**
   - Data annotations vs FluentValidation.
   - Custom validation attributes.
   - Global exception handling middleware returning standardized responses.

5. **Authentication & Authorization**
   - JWT Bearer authentication with `Microsoft.AspNetCore.Authentication.JwtBearer`.
   - Role-based, policy-based authorization.
   - Secure secrets with user secrets/Azure Key Vault.

6. **API Versioning & Documentation**
   - Add `Asp.Versioning.Http`.
   - Configure Swagger per version, include XML comments.
   - Add `Swashbuckle` filters for examples.

7. **Caching & Performance**
   - Response caching, cache profiles.
   - `IMemoryCache` and `IDistributedCache`.
   - Rate limiting middleware (ASP.NET Core 7+ built-in).

8. **Testing APIs**
   - Integration tests with `WebApplicationFactory`.
   - Contract tests with Postman/Newman or Pact.
   - Load testing using `k6`.

---

## Interview Check-In

- “Explain middleware pipeline and how request flows through it.”
- “How does model binding work for complex types?”
- “Difference between filters and middleware?”
- “How do you implement API versioning while maintaining backward compatibility?”
- Live coding: add `PATCH` endpoint with JSON Patch support.

---

## Observability

- Logging correlation IDs with middleware.
- Structured logging output to Elasticsearch (Serilog + Elastic sink).
- Metrics via `Prometheus-net` or `OpenTelemetry`.
- Health checks (`Microsoft.Extensions.Diagnostics.HealthChecks`).
- Distributed tracing with ActivitySource.

---

## Deployment Considerations

- `dotnet publish`, self-contained deployments.
- Hosting options: Kestrel behind Nginx/IIS, Azure App Service.
- Environment-specific configuration (Development/Staging/Production).
- API gateway integration (YARP, Ocelot).

---

## Resources

- [ASP.NET Core Web API documentation](https://learn.microsoft.com/aspnet/core/web-api/)
- [Minimal APIs vs Controllers](https://learn.microsoft.com/aspnet/core/fundamentals/minimal-apis)
- [FluentValidation](https://docs.fluentvalidation.net/)
- [API Versioning library](https://github.com/dotnet/aspnet-api-versioning)
- [ASP.NET Core Rate Limiting](https://learn.microsoft.com/aspnet/core/performance/rate-limit)

---

## Hindi Video Tutorials

- [ASP.NET Core Web API Full Course in Hindi – WsCube Tech](https://www.youtube.com/playlist?list=PLjpp5kBQLNTRu3FihZlyb-V0g9wGZHX0z)
- [JWT Authentication in ASP.NET Core (Hindi) – CodeWithHarry](https://www.youtube.com/watch?v=Q06F0g21aRM)
- [Swagger Documentation in Hindi – Geeky Shows](https://www.youtube.com/watch?v=JZxD8j04YfA)

---

> ✅ **Completion Criteria:** You can design, secure, document, and test a professional-grade Web API ready for deployment.

