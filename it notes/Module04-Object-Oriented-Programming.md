# Module 04: Object-Oriented Programming

> **Goal:** Internalize OOP principles in C#, design cohesive types, and apply SOLID and design patterns effectively.

---

## Learning Outcomes

| # | Outcome | Evidence of Mastery |
|---|---------|---------------------|
| 1 | Model real-world entities using classes, structs, and records | Domain model diagram |
| 2 | Apply encapsulation, inheritance, and polymorphism correctly | Refactored legacy code sample |
| 3 | Implement interfaces, abstract classes, and default interface members | Interface-based plugin system |
| 4 | Use SOLID principles and common design patterns (Singleton, Strategy, Factory, Observer) | Design document + code samples |
| 5 | Explain composition vs inheritance trade-offs | Technical blog post |

---

## Core Concepts

### Class Anatomy

- Fields, properties, auto-properties, backing fields.
- Access modifiers (`public`, `private`, `protected`, `internal`, `protected internal`, `private protected`).
- Object initialization patterns: constructors, object initializers, `with` expressions (records).
- Static members, static constructors, partial classes.

### Structs vs Classes vs Records

- Struct: value semantics, stack allocation, non-nullable by default.
- Class: reference semantics, inheritance support.
- Record: concise syntax, focus on immutability/equality semantics.

### Inheritance & Polymorphism

- Base/derived classes, `sealed`, `virtual`, `override`, `new`.
- Abstract classes vs interfaces.
- Polymorphism via interface injection.
- Diamond inheritance avoidance; use interfaces + composition.

### Encapsulation & SOLID

- **Single Responsibility:** one reason to change.
- **Open/Closed:** strategy pattern for extending behavior.
- **Liskov Substitution:** contract-based thinking.
- **Interface Segregation:** prefer small, purpose-driven interfaces.
- **Dependency Inversion:** sor rely on abstractions, not concretions.

---

## Detailed Notes

### 1. Building Classes & Encapsulation

Classes bundle data and behavior. Encapsulate state using **private fields** and expose behavior via **methods** or **properties**. Properties with private setters or computed getters provide controlled access. Use **access modifiers** to enforce boundaries: keep fields private, use `internal` for same-assembly sharing, `protected` for derived classes, and `public` only when necessary.

- **Constructors:** Initialize invariants; use constructor chaining (`: this(args)`) to avoid duplication. Provide overloads for different scenarios.
- **Object Initializers:** Combine with parameterless constructors for readability (`var customer = new Customer { Name = "Arya" };`).
- **Encapsulation patterns:** Use backing fields when you need validation or lazy loading. Provide methods like `Activate()` instead of exposing settable `IsActive`.
- **Static members:** Represent global state or helper functions; keep them stateless when possible to reduce coupling.
- **Records vs classes for POCOs:** Records provide value-based equality for data carriers; classes better for rich domain behavior.

#### Quick Check (5 Questions)
1. Why should most class fields be private and exposed through properties?
2. How does constructor chaining reduce duplication?
3. When would you prefer methods over settable properties for behavior?
4. What risks arise from excessive use of public static members?
5. How do auto-properties differ from properties with explicit backing fields?

---

### 2. Value Types, Reference Types & Records

Understanding type semantics helps avoid bugs and performance issues.

- **Structs:** Lightweight value types; useful for small, immutable objects (points, complex numbers). Avoid large structs (>16 bytes) to prevent copying overhead.
- **Classes:** Reference types; variables store references. Suitable for entities with identity.
- **Records:** Provide concise syntax, built-in equality, immutability (by default). `record class` vs `record struct` differentiate semantics.
- **Equality semantics:** Classes default to reference equality; override `Equals`/`GetHashCode` or use records. Structs compare field-by-field by default.
- **Copy behavior:** Value types copy entire data on assignment; reference types copy references, causing aliasing.

#### Quick Check (5 Questions)
1. Why can large structs negatively impact performance?
2. How do records implement value-based equality automatically?
3. In what scenarios would you use a `record struct` instead of a class?
4. How does assigning one class instance to another variable differ from struct assignment?
5. Why must you override `GetHashCode` when overriding `Equals`?

---

### 3. Inheritance & Polymorphism

Inheritance models **“is-a”** relationships. Use it sparingly to avoid brittle hierarchies.

- **Base classes:** Provide shared state/behavior. Mark methods `virtual` to allow overrides. Use `protected` to expose internal hooks to derived classes.
- **Overriding:** `override` extends or replaces base behavior. Call `base.Method()` when reusing base logic.
- **Sealing:** `sealed` prevents further inheritance; use to lock down hierarchies once stabilized.
- **Polymorphism:** Interact with objects through base references or interfaces (`Shape shape = new Circle(); shape.Draw();`).
- **Abstract classes vs interfaces:** Abstract classes share base implementation/state, interfaces define contracts.

#### Quick Check (5 Questions)
1. When should you mark a method `virtual`, and when should you avoid it?
2. What is the difference between hiding (`new`) and overriding (`override`) a method?
3. How does sealing a class or method help maintain invariants?
4. Why might deep inheritance hierarchies be problematic?
5. Provide an example of substituting a derived class where a base class is expected (LSP).

---

### 4. Working with Interfaces & Abstractions

Interfaces define contracts without implementation. They support multiple implementations and decouple dependencies.

- **Interface design:** Favor small, cohesive interfaces (`IEmailSender`, `IInvoiceRepository`) over “fat” ones. Use default interface methods sparingly to provide baseline implementations.
- **Dependency Inversion:** Consumers depend on abstractions (`ILogger`) rather than concrete types. Combine with DI containers to inject implementations.
- **Explicit interface implementation:** Hide members from public API while fulfilling interface requirements.
- **Polymorphic behavior:** Interfaces allow different implementations to be swapped seamlessly (e.g., `IStorageService` implemented by local file system, Azure Blob, or S3).
- **Testing:** Mock interfaces in unit tests to isolate behavior.

#### Quick Check (5 Questions)
1. Why is it beneficial to keep interfaces small and focused?
2. When would you prefer an abstract class instead of an interface?
3. How do default interface members affect implementers?
4. Provide a scenario where explicit interface implementation is useful.
5. How does dependency inversion improve testability?

---

### 5. SOLID Principles & Design Patterns

SOLID offers guidelines for maintainable software.

- **SRP:** Example—`InvoiceService` handles invoicing logic, while `InvoiceRepository` manages persistence.
- **OCP:** Extend behavior via new classes rather than modifying existing ones (Strategy pattern for discounts).
- **LSP:** Subclasses should not break expectations; e.g., `Square` should not inherit from `Rectangle` if setters behave differently.
- **ISP:** Prefer multiple interfaces (`IReadableFile`, `IWritableFile`) to avoid empty implementations.
- **DIP:** Depend on abstractions; inject dependencies via constructors.

Common patterns:

- **Factory Method:** Encapsulates object creation. Avoids exposing concrete types.
- **Strategy:** Swap behaviors at runtime (e.g., shipping calculators).
- **Observer:** Publish/subscribe updates (events, `IObservable`).
- **Decorator:** Wrap objects to extend behavior without altering core class.

#### Quick Check (5 Questions)
1. How does the Strategy pattern help satisfy the Open/Closed Principle?
2. Give an example where violating LSP causes runtime errors.
3. Why is Dependency Inversion critical for unit testing?
4. How can the Decorator pattern add behavior without modifying existing classes?
5. What symptoms indicate an interface is violating the Interface Segregation Principle?

---

## Guided Workshops

1. **Domain Modeling Exercise**
   - Choose a familiar domain (e.g., Library System).
   - Identify entities (Book, Member, Loan).
   - Map to classes, value objects, aggregate roots.
   - Draw UML diagram or use textual DSL (PlantUML).

2. **Design Pattern Showcase**
   - Implement `Factory Method` for creating payment processors.
   - Strategy pattern for discount evaluation.
   - Observer pattern with events for notification center.
   - Decorator pattern for adding behavior to logging.

3. **SOLID Refactor**
   - Start with a “God class” example.
   - Incrementally refactor applying each SOLID principle.
   - Document before/after metrics (lines of code, complexity).

4. **Interface-Driven Development**
   - Define `IRepository<T>`, implement memory and file variants.
   - Inject via constructor, demonstrate unit testing ease.

---

## Hands-On Checklist

- Build a class library `DomainModels`.
- Add XML documentation and generate API docs.
- Configure analyzers to enforce naming conventions.
- Implement `IDisposable` when managing unmanaged resources.
- Use records for DTOs and pattern match on property values.

---

## Interview Check-In

- “Differentiate between abstract class and interface in C# 8+ with default implementations.”
- “How do you enforce immutability in C#?”
- “Explain SOLID with practical examples.”
- Live exercise: implement `ILogger` decorator adding correlation IDs.
- Scenario: “Given BaseViewModel with OnPropertyChanged, refactor to use composition.”

---

## Self-Assessment & Reflection

- Can you tell when to prefer composition over inheritance?
- Do you recognize code smells (large classes, long methods)?
- Have you applied design patterns beyond examples?

---

## Resources

- [C# Records and Immutability](https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/record)
- [SOLID principles in .NET](https://learn.microsoft.com/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures)
- *Head First Design Patterns (C# examples)*
- [Refactoring.Guru pattern catalog](https://refactoring.guru/design-patterns)

---

## Hindi Video Tutorials

- [Object-Oriented Programming in C# (Hindi) – WsCube Tech](https://www.youtube.com/watch?v=1p0ov3R5hgY)
- [SOLID Principles Explained in Hindi – Geeky Shows](https://www.youtube.com/watch?v=Vd7n1xXHrpM)
- [Design Patterns with Real Life Examples in Hindi – CodeWithHarry](https://www.youtube.com/watch?v=O-Y4qGxZ7R0)

---

> ✅ **Completion Criteria:** You can design object models aligned with SOLID, choose appropriate type semantics (class/struct/record), and defend your architectural decisions in interviews.

