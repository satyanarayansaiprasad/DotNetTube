# Module 11: Web UI & Blazor

> **Goal:** Build rich front-end experiences using ASP.NET Core Razor Pages, MVC views, and modern Blazor components—covering both server-side and WebAssembly hosting models.

---

## Learning Outcomes

| # | Outcome | Evidence |
|---|---------|----------|
| 1 | Create Razor Pages and MVC views with layouts, partials, and tag helpers | Multi-page site with shared layout |
| 2 | Build interactive Blazor components (Server & WASM) | Component library + demo app |
| 3 | Manage state, routing, and forms in Blazor | Complex form with validation |
| 4 | Integrate REST APIs and SignalR with Blazor | Real-time dashboard project |
| 5 | Optimize and deploy Blazor apps, including prerendering | Deployment guide (Azure Static Web Apps/App Service) |

---

## Razor Pages & MVC Fundamentals

- Layouts, sections, partials, view components.
- Tag helpers vs HTML helpers.
- Model binding and validation for forms.
- Anti-forgery tokens, TempData, ViewData, ViewBag.

### Hands-On

- Build `StudentPortal` with Razor Pages:
  - CRUD operations.
  - Validation summary with `asp-validation-summary`.
  - Localization (resource files).

---

## Blazor Essentials

### Hosting Models

- **Blazor Server:** SignalR-based, fast load, server resources.
- **Blazor WebAssembly:** Client-side, offline capable, larger payload.
- Hybrid (MAUI).

### Component Model

- Components (`.razor`), parameters, lifecycle (`OnInitialized`, `OnParametersSet`, `Dispose`).
- Event handling, `EventCallback`, two-way binding (`@bind`).
- Render fragments, templated components.

### State Management

- Cascading values, dependency injection.
- In-memory state container, browser storage (local/session).
- Fluxor/Redux patterns.

### Forms & Validation

- `EditForm`, `InputText`, `InputSelect`.
- Data annotations and FluentValidation integration.
- Custom validation attributes with `ValidationMessage`.

### JS Interop

- `IJSRuntime`, invoking JavaScript functions.
- Registering .NET methods callable from JS.
- Using existing JS libraries (charts, maps).

---

## Detailed Notes

### 1. Razor Pages & MVC Architecture

Razor Pages and MVC share the MVC pipeline but differ in organization.

- **Razor Pages:** Each page has a `.cshtml` view and `.cshtml.cs` PageModel with handlers (`OnGet`, `OnPost`). Good for page-focused apps.
- **Layouts & Partials:** `_Layout.cshtml` defines shared shell; `RenderBody` injects page content. Use partials and view components for reusable UI.
- **Tag Helpers:** Replace HTML helpers with expressive tags (`<form asp-action="Create">`). Custom tag helpers encapsulate logic.
- **Model Binding:** Combines query, route, form data into PageModel properties; `[BindProperty]` binds automatically.
- **Security:** Anti-forgery tokens automatically inserted; use `ValidateAntiForgeryToken` for POST handlers.

#### Quick Check (5 Questions)
1. How do Razor Pages differ from MVC controllers conceptually?
2. When should you create a view component instead of a partial view?
3. How do tag helpers improve maintainability compared to HTML helpers?
4. What does `[BindProperty]` do in Razor Pages?
5. How are anti-forgery tokens automatically integrated into Razor forms?

---

### 2. Blazor Hosting Models

- **Blazor Server:** Renders UI on server, communicates via SignalR. Advantages: small downloads, centralized processing; challenges: latency sensitive, requires persistent connection.
- **Blazor WebAssembly:** Runs entirely in browser via WebAssembly; supports offline usage but larger initial payload & limited by browser sandbox.
- **Blazor Hybrid (MAUI):** Combines native apps with Blazor UI sharing.
- **Prerendering:** Pre-render pages on server for better SEO and perceived performance.
- **Publishing:** WASM deployable to static hosts (Azure Static Web Apps); server requires ASP.NET hosting environment.

#### Quick Check (5 Questions)
1. What factors influence choosing Blazor Server over WebAssembly?
2. How does SignalR facilitate Blazor Server communication?
3. Why is prerendering beneficial even for Blazor WebAssembly apps?
4. When would you consider Blazor Hybrid with .NET MAUI?
5. What limitations does the browser sandbox impose on Blazor WebAssembly?

---

### 3. Component Composition & State Management

Blazor components are `.razor` files with markup and C#.

- **Parameters:** `[Parameter]` for inputs; `[CascadingParameter]` for shared state.
- **Lifecycle Methods:** `OnInitialized{Async}`, `OnParametersSet{Async}`, `OnAfterRender{Async}`; always call `StateHasChanged` when state updates asynchronously.
- **State Containers:** Singleton services, local storage, or third-party libraries (Fluxor) maintain shared state.
- **Routing:** Use `@page "/route"` to define routes; `NavLink` for navigation with active class.
- **EventCallbacks:** Provide asynchronous-safe event handlers; use `EventCallback<T>` to avoid re-render issues.

#### Quick Check (5 Questions)
1. When should you use `CascadingParameter` instead of regular parameters?
2. What is the difference between `OnInitialized` and `OnInitializedAsync`?
3. How do you trigger UI updates when background tasks finish?
4. Why is `EventCallback<T>` preferred over `Action` callbacks?
5. What strategies exist for persisting state across navigation in Blazor?

---

### 4. Forms, Validation & Authentication

- **Forms:** `EditForm` wires inputs to models; use `InputText`, `InputSelect`, `InputNumber`. Bind to model with `@bind-Value`.
- **Validation:** Data annotations or FluentValidation; include `<ValidationSummary>` and `<ValidationMessage>` components.
- **Authentication:** Use `AuthenticationStateProvider` to fetch user information. For Blazor WASM, integrate MSAL or custom auth providers; for server, reuse ASP.NET Core identity.
- **Authorization:** `AuthorizeView` to conditionally render UI; `[Authorize]` on components (with route view).
- **Error Handling:** Implement `ErrorBoundary` for component-level error capture.

#### Quick Check (5 Questions)
1. How does `EditForm` connect UI inputs to a model?
2. How do you display field-specific validation errors in Blazor?
3. What role does `AuthenticationStateProvider` play?
4. How do you hide UI elements from unauthorized users?
5. When should you use `ErrorBoundary` in Blazor components?

---

### 5. JavaScript Interop & Performance Optimization

Blazor interoperates with JavaScript via `IJSRuntime`.

- **JS Interop:** `await JS.InvokeVoidAsync("functionName", args);` to call JS. Use `DotNetObjectReference` to call .NET from JS.
- **Libraries Integration:** Wrap charting libraries or maps within Blazor components using JS interop.
- **Performance:** Use `<Virtualize>` for large lists, lazy load assemblies via `LazyAssemblyLoader`, bundle static assets via `dotnet publish`.
- **Caching:** For WASM, configure service worker (`wwwroot/service-worker.published.js`) for offline support.
- **Diagnostics:** Use browser dev tools and `dotnet monitor` to trace performance; pre-render and enable compression (Brotli).

#### Quick Check (5 Questions)
1. How do you call a JavaScript function from a Blazor component?
2. When do you need `DotNetObjectReference`?
3. What does the `<Virtualize>` component do?
4. How can service workers enhance Blazor WebAssembly apps?
5. Which tools help diagnose performance issues in Blazor apps?

---

## Blazor Labs

1. **Component Library**
   - Create `SharedComponents` project.
   - Build reusable components: `Button`, `Modal`, `DataGrid`.
   - Support parameters for styling, events.

2. **Weather Dashboard**
   - Fetch data from public API.
   - Display charts using JS interop (Chart.js).
   - Add caching and offline fallback.

3. **Real-Time Notifications**
   - Integrate SignalR for push updates.
   - Show toast notifications with custom component.

4. **Authentication Flow**
   - Implement Azure AD B2C or Identity for Blazor Server.
   - Protect routes with `[Authorize]`.
   - Use `AuthenticationStateProvider`.

5. **Deployment Pipeline**
   - Configure CI to build Blazor WebAssembly.
   - Publish to Azure Static Web Apps.
   - Enable prerendering and compression.

---

## Interview Check-In

- “Explain differences between Razor Pages, MVC, and Blazor.”
- “How does state management differ for Blazor Server vs WebAssembly?”
- “Describe the lifecycle methods of Blazor components.”
- “How do you call JavaScript from Blazor and vice versa?”
- Live task: Create a reusable Blazor `Pagination` component.

---

## Performance Considerations

- Lazy loading assemblies (`<BlazorWebAssemblyLoadAllGlobalizationData>`).
- Virtualize large lists using `<Virtualize>` component.
- Pre-rendering on server for faster first paint.
- Analyze payload with browser dev tools.

---

## Resources

- [ASP.NET Core Razor Pages](https://learn.microsoft.com/aspnet/core/razor-pages/)
- [Blazor documentation](https://learn.microsoft.com/aspnet/core/blazor/)
- [Awesome Blazor Components](https://github.com/AdrienTorris/awesome-blazor)
- [Azure Static Web Apps deployment](https://learn.microsoft.com/azure/static-web-apps/)

---

## Hindi Video Tutorials

- [ASP.NET Core Razor Pages Tutorial in Hindi – WsCube Tech](https://www.youtube.com/watch?v=gdqJzi10BGY)
- [Blazor WebAssembly Full Course (Hindi) – Geeky Shows](https://www.youtube.com/playlist?list=PLbGui_ZYuhidWOb8KJ8LtfD-RS9YvV6yr)
- [Blazor Server vs Blazor WASM Explained in Hindi – CodeDecode](https://www.youtube.com/watch?v=2X0uGAshEA0)

---

> ✅ **Completion Criteria:** You can deliver responsive, production-ready UI experiences using ASP.NET Core Razor and Blazor, with proper state management, validation, and deployment practices.

