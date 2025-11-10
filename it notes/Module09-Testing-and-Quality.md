# Module 09: Testing & Quality Assurance

> **Goal:** Establish a quality-focused mindset with automated tests, coverage analysis, and continuous quality gates for .NET applications.

---

## Learning Outcomes

| # | Outcome | Evidence |
|---|---------|----------|
| 1 | Write unit tests using xUnit/NUnit/MSTest | Test project with >80% coverage on core library |
| 2 | Apply mocking and test doubles (Moq, NSubstitute) | Mocking scenarios covering services & repositories |
| 3 | Execute integration tests using TestServer/WebApplicationFactory | ASP.NET Core API integration suite |
| 4 | Measure code coverage & enforce with CI gates | Coverage reports (Coverlet) in pipeline |
| 5 | Apply static analysis and code review checklists | Documented quality charter |

---

## Testing Layers Overview

| Layer | Purpose | Tooling | Notes |
|-------|---------|---------|-------|
| Unit Tests | Validate smallest units in isolation | xUnit, MSTest, NUnit | Fast, deterministic |
| Integration Tests | Validate component interaction | xUnit + TestServer, Docker Compose | Use real or in-memory resources |
| End-to-End | Validate business flows | Playwright, Selenium | Run in staging environment |
| Performance | Validate throughput/latency | BenchmarkDotNet, k6 | Automate for critical paths |
| Static Analysis | Detect issues pre-runtime | Roslyn analyzers, SonarQube | Stage in CI |

---

## Core Practices

- **Arrange-Act-Assert** pattern; keep tests independent.
- Naming conventions: `MethodName_StateUnderTest_ExpectedBehavior`.
- Parameterized tests (`[Theory]`, `[InlineData]`).
- Shared context using `IClassFixture`, `CollectionFixture`.
- Mocking vs stubbing vs faking; prefer real collaborators when cheap.
- Snapshot testing for JSON payloads.
- Code coverage strategies: statement, branch, mutation testing (Stryker.NET).

---

## Detailed Notes

### 1. Testing Strategy & Framework Overview

Arrange tests across layers for balanced coverage. **Unit tests** target small units; **integration tests** ensure components work together; **end-to-end tests** validate user journeys. Choose frameworks:

- **xUnit:** Constructor injection for fixtures, `[Fact]` and `[Theory]`.
- **NUnit:** Rich attribute support (`[TestCase]`, `[TestFixture]`), explicit setup/teardown via `[SetUp]`, `[TearDown]`.
- **MSTest:** Legacy support, seamless integration with Visual Studio Test Explorer.

Organize tests mirroring production namespaces. Keep tests atomic and independent—no shared mutable state. Use descriptive method names: `CalculateDiscount_PremiumCustomer_ReturnsExpectedValue()`.

#### Quick Check (5 Questions)
1. How do unit, integration, and end-to-end tests differ in scope?
2. What advantages does xUnit’s constructor injection provide?
3. When might MSTest still be the preferred framework?
4. Why should tests avoid shared mutable state?
5. How do descriptive test names aid maintenance?

---

### 2. Unit Testing Patterns & Practices

Adopt the **Arrange-Act-Assert** template to keep tests readable:

```csharp
// Arrange
var calculator = new Calculator();

// Act
var result = calculator.Add(2, 3);

// Assert
result.Should().Be(5);
```

- **Data-driven tests:** Use `[Theory]` with `[InlineData]` or `[MemberData]`.
- **Assertions:** FluentAssertions provides expressive checks, improving failure messages.
- **Testing exceptions:** `Assert.Throws<InvalidOperationException>(() => service.Execute());`.
- **Time-dependent code:** Inject `IDateTimeProvider` to control time during tests.
- **Code coverage:** Ensure both success and failure paths tested; focus on behavior, not implementation details.

#### Quick Check (5 Questions)
1. What are the three phases of the AAA pattern?
2. How do you supply multiple datasets to a single test method in xUnit?
3. Why use FluentAssertions instead of `Assert.Equal`?
4. How can you test code dependent on current time?
5. What should unit tests assert: implementation details or observable behavior?

---

### 3. Mocking, Fakes & Test Doubles

Use test doubles to isolate units:

- **Mocks:** Verify interactions using Moq (`mock.Verify(repo => repo.Save(It.IsAny<Order>()));`).
- **Stubs:** Return canned responses; no behavior verification.
- **Fakes:** Lightweight implementations (e.g., in-memory repository).
- **AutoFixture:** Generates anonymous data to reduce manual setup.
- **Guidelines:** Mock boundaries (database, network); avoid mocking simple value objects. Keep tests resilient to refactoring by focusing on observable behavior.

#### Quick Check (5 Questions)
1. What distinguishes a mock from a stub?
2. How does AutoFixture simplify test data arrangement?
3. When should you verify method calls on mocks?
4. Why is overmocking considered harmful?
5. Give an example where an in-memory fake is preferable to a mock.

---

### 4. Integration & End-to-End Testing

Integration tests ensure modules collaborate correctly.

- **ASP.NET Core:** `WebApplicationFactory<TEntryPoint>` hosts the app in-memory. Customize configuration via `.WithWebHostBuilder`.
- **Databases:** Use transient resources (SQLite in-memory, Testcontainers) to avoid flaky tests. Seed data during fixture setup.
- **External Services:** Replace with wiremock/MockHTTP servers. Validate contracts via Pact or other contract testing tools.
- **End-to-End (E2E):** Playwright for web UI, BDD frameworks (SpecFlow) for behavior specifications.
- **Environment isolation:** Use dedicated appsettings (`appsettings.Testing.json`).

#### Quick Check (5 Questions)
1. How do you host an ASP.NET Core app in-memory for integration testing?
2. Why are disposable test databases preferable to shared ones?
3. What tools help simulate external HTTP services in tests?
4. When should you introduce Playwright or Selenium tests?
5. How can you override configuration specifically for integration tests?

---

### 5. Coverage, Static Analysis & Quality Gates

Quality gates prevent regressions from landing in main branches.

- **Coverlet Integration:** `dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura`.
- **Thresholds:** Enforce with `/p:Threshold=80 /p:ThresholdType=line`.
- **Static Analysis:** Integrate Roslyn analyzers, SonarQube, StyleCop. Address warnings proactively.
- **Mutation Testing:** Stryker.NET mutates code to ensure tests catch unexpected changes; high mutation score indicates strong tests.
- **CI/CD:** Automate `dotnet format`, `dotnet test`, coverage, and analyzer steps. Fail builds on violations.

#### Quick Check (5 Questions)
1. What command parameters configure Coverlet coverage thresholds?
2. How does mutation testing strengthen your test suite?
3. Why integrate static analysis into CI pipelines?
4. Which tool runs automated code formatting respecting `.editorconfig`?
5. What metrics or checks should gate pull request merges?

---

## Hands-On Labs

1. **Unit Test Bootcamp**
   - Create `CalculatorTests` using xUnit.
   - Practice `[Fact]` vs `[Theory]`.
   - Introduce guard clauses and test exceptions.

2. **Mocking Workshop**
   - Build service depending on repository interface.
   - Use Moq to simulate behaviors, verify interactions.
   - Introduce AutoFixture for auto-generated data.

3. **Integration Testing API**
   - Create ASP.NET Core minimal API.
   - Use `WebApplicationFactory` to test endpoints.
   - Seed in-memory database (Sqlite in-memory).

4. **Coverlet & Reporting**
   - Run `dotnet test /p:CollectCoverage=true`.
   - Output report (cobertura) for CI integration.
   - Set coverage threshold (`/p:Threshold=80`).

5. **Static Analysis Pipeline**
   - Configure `dotnet build` with analyzers.
   - Add Git hooks for `dotnet format`.
   - Integrate SonarCloud or GitHub Advanced Security (optional).

---

## Interview Check-In

- “How do you decide what to mock in a unit test?”
- “Difference between unit and integration tests in .NET?”
- “Explain how to test asynchronous code properly.”
- “What is the role of dependency injection in testability?”
- Live task: Write failing test first, then implement minimal code to pass (TDD).

---

## Quality Gates Checklist

- ✅ Unit tests pass locally and in CI.
- ✅ Code coverage above agreed threshold.
- ✅ Static analysis clean or warnings triaged.
- ✅ Code review checklist followed (naming, complexity, security).
- ✅ Pull request template updated with testing evidence.

---

## Resources

- [xUnit documentation](https://xunit.net/)
- [Testing ASP.NET Core](https://learn.microsoft.com/aspnet/core/test/)
- [Mocking with Moq](https://github.com/moq/moq4/wiki/Quickstart)
- [Coverlet coverage](https://github.com/coverlet-coverage/coverlet)
- [Stryker.NET mutation testing](https://stryker-mutator.io/docs/stryker-net/introduction/)

---

## Hindi Video Tutorials

- [Unit Testing in C# with xUnit (Hindi) – WsCube Tech](https://www.youtube.com/watch?v=7-7c9KuX3sI)
- [Moq Framework Tutorial in Hindi – CodeDecode](https://www.youtube.com/watch?v=wKwj0vW6B1k)
- [ASP.NET Core Integration Testing in Hindi – Geeky Shows](https://www.youtube.com/watch?v=8mxwdcNmK7w)

---

> ✅ **Completion Criteria:** A robust test suite exists, you understand testing trade-offs, and you can defend your quality strategy during interviews and code reviews.

