# Module 07: .NET Libraries & Tooling

> **Goal:** Navigate the .NET ecosystem efficiently, leverage key base class libraries (BCL), and use productivity tooling to build maintainable applications.

---

## Learning Outcomes

| # | Outcome | Evidence |
|---|---------|----------|
| 1 | Utilize core BCL namespaces for common tasks (collections, IO, threading) | Utility library project |
| 2 | Manage configuration, logging, and DI using `Microsoft.Extensions.*` packages | Console app with structured logging |
| 3 | Use NuGet effectively, handle package versioning, and manage feeds | Private feed setup documentation |
| 4 | Employ Roslyn analyzers, formatting tools, and code metrics | Analyzer results + action plan |
| 5 | Automate repetitive tasks with CLI tooling and scripts | `dotnet` tool manifest with custom tools |

---

## Core Library Highlights

### Collections & LINQ Helpers

- `System.Collections.Generic`, `System.Collections.Concurrent`, `System.Collections.Immutable`.
- Immutable collections advantages in multi-threading.

### IO & Serialization

- File and directory APIs (`File`, `FileInfo`, `DirectoryInfo`).
- `Stream`, `StreamReader`, `StreamWriter`, `MemoryStream`.
- Serialization: `System.Text.Json`, `JsonSerializerOptions`, converters.
- XML serialization via `XmlSerializer`, when to use.

### Date & Time

- `DateTime` vs `DateTimeOffset`, `TimeZoneInfo`.
- `Stopwatch` for performance timing.
- `System.Globalization` for localization.

### Configuration, Logging, DI

- `ConfigurationBuilder`, JSON/environment providers.
- `ILogger<T>`, logging providers (Console, Debug, Serilog integration).
- `IServiceCollection`, lifetime management (Transient, Scoped, Singleton).
- Options pattern (`IOptions<T>`, `IOptionsSnapshot<T>`).

---

## Tooling Proficiency

### NuGet & Package Management

- `dotnet add package`, `dotnet restore`.
- Package reference formats, central package management.
- Creating private feeds with Azure Artifacts or GitHub Packages.
- Semantic version ranges, floating versions, lock files.

### CLI & Global Tools

- `dotnet tool install` local vs global.
- Recommended tools: `dotnet-format`, `dotnet-ef`, `dotnet-outdated`, `dotnet-serve`.
- Script automation with `dotnet script` (C# scripting).

### Analyzers & Formatting

- Enable nullable reference types, warnings as errors.
- Use `.editorconfig` to enforce coding style.
- Roslyn analyzers (Sonar, FxCop) integration.
- Code metrics: maintainability index, cyclomatic complexity.

### IDE Productivity

- Visual Studio features: Live Unit Testing, IntelliTrace, CodeLens.
- VS Code: tasks, launch configurations, integrated terminal.
- Rider: inspections, code cleanup profiles.

---

## Detailed Notes

### 1. Base Class Library Essentials

The **Base Class Library (BCL)** offers reusable building blocks for most .NET apps.

- **Collections:** `List<T>`, `Dictionary<TKey,TValue>`, `HashSet<T>` for general use; `ConcurrentDictionary` for multi-threaded scenarios. Immutable collections provide safe sharing.
- **File & Directory APIs:** `File`, `Directory`, `Path` classes simplify file operations. `FileStream` supports buffered reading/writing; wrap in `using`.
- **Serialization:** `System.Text.Json` is the modern JSON serializer; configure with `JsonSerializerOptions` (camel-case naming, reference handling).
- **Networking:** `HttpClient` for HTTP calls; use `IHttpClientFactory` to avoid socket exhaustion.
- **Threading:** `Task`, `TaskFactory`, `Parallel.ForEach`, `CancellationToken` enable parallelism.

#### Quick Check (5 Questions)
1. Why should `HttpClient` be reused instead of instantiated per request?
2. How do immutable collections help in concurrent applications?
3. What options allow you to customize `System.Text.Json` serialization?
4. Which namespace contains file system helpers like `Path.Combine`?
5. When would you choose `ConcurrentDictionary` over `Dictionary`?

---

### 2. Configuration Management

`Microsoft.Extensions.Configuration` provides hierarchical configuration from multiple sources.

- **Providers:** JSON files, environment variables, command-line args, user secrets, Azure Key Vault.
- **Binding:** Use `ConfigurationBinder.Bind` or `services.Configure<TOptions>` to map sections to strongly typed settings classes.
- **Reload-on-change:** Enable with `reloadOnChange: true` for JSON files to refresh configuration dynamically.
- **Environment-specific settings:** `appsettings.Development.json`, `appsettings.Production.json` override base settings.
- **Secrets management:** Use `dotnet user-secrets` during development, Key Vault for production.

#### Quick Check (5 Questions)
1. How do you bind configuration sections to typed options classes?
2. When is reload-on-change useful and how do you enable it?
3. Why should secrets not live in source-controlled JSON files?
4. How do environment variables override JSON configuration values?
5. Describe how to load configuration from Azure Key Vault.

---

### 3. Logging Strategies

`Microsoft.Extensions.Logging` defines abstractions for structured logging.

- **Log Levels:** Trace, Debug, Information, Warning, Error, Critical. Configure filters via `appsettings.json`.
- **Providers:** Console, Debug, EventSource, Seq, Serilog, NLog. Add multiple providers to broadcast log events.
- **Structured Logging:** Use message templates (`logger.LogInformation("Order {OrderId} created", id);`) for queryable logs.
- **Scopes:** Enrich logs with contextual data (correlation IDs). `using (logger.BeginScope("OrderId:{OrderId}", id)) { ... }`
- **Serilog:** Popular provider with sinks for files, Elasticsearch, Seq. Configure via code or appsettings.

#### Quick Check (5 Questions)
1. What differentiates structured logging from plain text logging?
2. How do you dynamically control log levels without redeploying?
3. Why should you wrap log statements with scopes?
4. What benefits does Serilog provide compared to the default console logger?
5. How do logging providers compose within ASP.NET Core?

---

### 4. Dependency Injection & Options Pattern

ASP.NET Core (and generic host) use built-in **dependency injection** (DI).

- **Service lifetimes:** Transient (new instance per request), Scoped (per web request), Singleton (application-wide).
- **Registration:** `services.AddTransient<IEmailSender, SmtpEmailSender>();`
- **Options pattern:** `services.Configure<MySettings>(configuration.GetSection("MySettings"));` Inject `IOptions<T>` or `IOptionsSnapshot<T>` for environment-specific values.
- **Constructor injection:** Preferred pattern; avoid service locator anti-pattern.
- **Validation:** `services.AddOptions<MySettings>().Bind(config).ValidateDataAnnotations();`

#### Quick Check (5 Questions)
1. When should you choose scoped lifetime versus singleton?
2. How does `IOptionsSnapshot<T>` differ from `IOptions<T>`?
3. What is constructor injection, and why is it considered best practice?
4. How can you validate configuration-bound options?
5. What issues arise from using the service locator pattern?

---

### 5. Tooling & Productivity Enhancers

- **NuGet:** `dotnet add package`, central package management via `Directory.Packages.props`. Use `PackageReference` to declare dependencies.
- **Global Tools:** Install CLI extensions (`dotnet tool install --global dotnet-ef`). Use tool manifest (`dotnet new tool-manifest`) for reproducibility.
- **Analyzers & Formatting:** `dotnet format` enforces style; Roslyn analyzers catch code smells. `.editorconfig` centralizes style rules.
- **Source Generators:** Add compile-time code generation to reduce reflection overhead.
- **IDE Extensions:** In Visual Studio, use Code Cleanup, analyzers, Live Templates; in VS Code, configure tasks for build/test.

#### Quick Check (5 Questions)
1. How does a tool manifest improve team tooling consistency?
2. What command formats code according to `.editorconfig` rules?
3. Why should large solutions adopt central package management?
4. How do source generators differ from T4 templates?
5. Which analyzers would you add to enforce coding guidelines?

---

## Hands-On Labs

1. **Utility Toolkit**
   - Create `CommonUtilities` class library.
   - Implement file utilities, JSON helpers, retry policies.
   - Add unit tests and XML documentation.

2. **Configuration Demo**
   - Console app reading from `appsettings.json`, environment variables.
   - Support `IOptions<T>` binding to POCO.
   - Demonstrate reload-on-change.

3. **Structured Logging**
   - Integrate Serilog with console & file sinks.
   - Implement correlation IDs using scopes.
   - Add custom enrichment (machine name, request id).

4. **Analyzer Enforcement**
   - Introduce `StyleCop.Analyzers`.
   - Fix warnings, configure rule severity.
   - Document rule exceptions and reasoning.

5. **NuGet Publishing**
   - Create a sample package with `dotnet pack`.
   - Publish to local feed (`nuget locals`).
   - Optional: push to nuget.org (use sandbox API key).

---

## Interview Check-In

- “How does dependency injection work in ASP.NET Core?”
- “What are the benefits of using configuration providers?”
- “Explain the difference between transient and scoped lifetime.”
- “How do you ensure consistent code style across a team?”
- Live exercise: add Serilog to an existing console app within 5 minutes.

---

## Resources

- [Microsoft.Extensions.* overview](https://learn.microsoft.com/dotnet/core/extensions/)
- [NuGet documentation](https://learn.microsoft.com/nuget/)
- [System.Text.Json guide](https://learn.microsoft.com/dotnet/standard/serialization/system-text-json-overview)
- [dotnet tools catalog](https://learn.microsoft.com/dotnet/core/tools/global-tools)

---

## Hindi Video Tutorials

- [.NET Configuration and Dependency Injection in Hindi – WsCube Tech](https://www.youtube.com/watch?v=rWbSleqrs9U)
- [Logging in ASP.NET Core (Hindi) – CodeWithHarry](https://www.youtube.com/watch?v=kX5lHObbIVY)
- [NuGet Package Manager & Tools in Hindi – Geeky Shows](https://www.youtube.com/watch?v=jacr4Aa4CBs)

---

> ✅ **Completion Criteria:** You can scaffold reusable libraries, enforce code quality via tooling, and configure cross-cutting concerns (config/logging/DI) in any .NET project.

