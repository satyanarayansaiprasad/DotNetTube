# Module 05: Advanced C# Features

> **Goal:** Develop mastery over modern C# constructs beyond OOP, enabling expressive, concise, and high-performance code.

---

## Learning Outcomes

| # | Outcome | Evidence |
|---|---------|----------|
| 1 | Implement generics with constraints and understand variance | Generic repository with unit tests |
| 2 | Use delegates, events, lambdas, and expression trees | Event-driven notification system |
| 3 | Apply LINQ (method & query syntax) including deferred execution | Data processing pipeline with benchmarks |
| 4 | Leverage reflection, attributes, dynamic types cautiously | Plugin loader with attribute discovery |
| 5 | Implement asynchronous workflows with `async/await`, tasks, and cancellation | Responsive I/O demo with cancellation |

---

## Topics Breakdown

### Generics & Variance

- Generic classes, methods, interfaces.
- Constraints: `where T : class`, `struct`, `new()`, interface, base class.
- Covariance (`out`), contravariance (`in`) in delegates/interfaces.
- Generic type inference, `default` literal, `typeof(T)`.

### Delegates, Events, and Lambdas

- Delegate declaration vs `Func<>`, `Action<>`.
- Multicast delegates, invocation list.
- Events: `event` keyword, custom event accessors.
- Anonymous methods vs lambda expressions.
- Expression-bodied members, capturing variables closures.

### LINQ in Depth

- Standard query operators: `Select`, `Where`, `OrderBy`, `GroupBy`, `Join`.
- Deferred vs immediate execution.
- `IQueryable` vs `IEnumerable`, expression trees.
- Custom LINQ provider basics (conceptual).
- Parallel LINQ (PLINQ) considerations and pitfalls.

### Dynamic Features & Reflection

- Reflection APIs: `Assembly.Load`, `Type`, `MethodInfo`.
- Attributes: built-in (`[Obsolete]`, `[Serializable]`) and custom.
- `dynamic` keyword, DLR dispatch, runtime binding errors.
- Source generators overview (C# 9+).

### Asynchronous Programming

- Task-based async pattern (TAP).
- `async`/`await` flow, synchronization context.
- Creating tasks with `Task.Run`, `TaskCompletionSource`.
- Cancellation tokens, cooperative cancellation patterns.
- Handling exceptions in async code, `ConfigureAwait`.

---

## Detailed Notes

### 1. Generics, Constraints & Variance

Generics enable type-safe reuse. Define generic methods (`T Max<T>(T a, T b) where T : IComparable<T>`) and classes (`Repository<T>`) to avoid boxing and casting.

- **Constraints:** Limit type parameters to ensure available members. `where T : class` enforces reference types; `where T : new()` ensures parameterless constructors.
- **Variance:** `IEnumerable<Animal>` can reference `List<Dog>` because `out` (covariant) allows read-only use. `IComparer<Dog>` can be assigned to `IComparer<Animal>` using `in` (contravariant) because it only consumes values.
- **Generics & static members:** Each closed generic type has its own static fields.
- **Generic Math (C# 11+):** Interfaces like `INumber<T>` enable numeric generic algorithms.
- **Pitfalls:** Avoid using reference type constraints when value types are needed; be cautious with open generics in DI containers.

#### Quick Check (5 Questions)
1. How do generic constraints improve compile-time safety?
2. Provide an example of covariance in .NET collection interfaces.
3. Why does each closed generic type have independent static fields?
4. When would you use `where T : unmanaged`?
5. What problems do generic math interfaces solve in numeric algorithms?

---

### 2. Delegates, Events & Lambda Expressions

Delegates are type-safe function pointers. Built-in delegate types (`Func`, `Action`, `Predicate`) simplify declarations. Lambdas offer inline anonymous methods capturing outer variables (**closures**).

- **Multicast delegates:** Combine delegates; invocation order follows addition sequence. Handle exceptions to avoid short-circuiting.
- **Events:** Restrict subscription/unsubscription with `event` keyword. Implement custom add/remove for advanced scenarios.
- **Closures:** Lambda captures by reference; be mindful of loop variables (C# 5+ corrected capture semantics).
- **Expression-bodied members:** Provide concise implementations (`int Square(int x) => x * x;`).
- **Expression Trees:** `Expression<Func<T, bool>>` represent code as data; essential for LINQ providers generating SQL.

#### Quick Check (5 Questions)
1. How do delegates differ from events in C#?
2. What is a closure, and how does it affect lambdas inside loops?
3. Why are `Func` and `Action` delegates preferred over custom delegate types?
4. When would you choose expression trees over compiled delegates?
5. How do you prevent event handlers from leaking memory?

---

### 3. LINQ Deep Dive

Language Integrated Query (LINQ) provides declarative data transformations across collections, XML, datasets, and databases.

- **Deferred execution:** Query definitions are not executed until enumerated; be cautious with changing sources.
- **Query vs Method Syntax:** Query syntax translates to method calls; method syntax offers more operators.
- **Projection:** Use `Select` and anonymous types to shape results. Combine with `let` in query syntax for intermediate values.
- **Grouping & Joining:** `GroupBy`, `Join`, `GroupJoin` support relational operations. Understand difference between inner and outer joins (`DefaultIfEmpty`).
- **PLINQ:** Parallelizes queries; ensure operators are thread-safe and order is preserved when needed (`AsOrdered`).

#### Quick Check (5 Questions)
1. What is deferred execution and how can it cause unexpected results?
2. How does `SelectMany` differ from `Select`?
3. When should you use `AsEnumerable` vs `AsQueryable`?
4. What are the pitfalls of using PLINQ indiscriminately?
5. How would you perform a left outer join in LINQ query syntax?

---

### 4. Reflection, Attributes & Dynamic Features

Reflection allows inspecting metadata and constructing objects at runtime. Use `Assembly.GetExecutingAssembly()` to discover types, `Type.GetMethods()` to inspect members, and `Activator.CreateInstance` for dynamic instantiation.

- **Attributes:** Decorate code with metadata. Retrieve via `GetCustomAttributes<T>()`. Custom attributes inherit from `Attribute`.
- **Dynamic keyword:** Bypass compile-time checks; resolves at runtime via DLR; useful for COM interop but prone to runtime errors.
- **Emit & Source Generators:** Reflection emit can build types at runtime; Roslyn source generators create compile-time code—safer alternative.
- **Performance:** Reflection is slower; cache metadata (e.g., `PropertyInfo`) and avoid repeated lookups.
- **Security:** Limit reflection in sandboxed environments; consider `DynamicMethod` and expression compilation for performance-critical scenarios.

#### Quick Check (5 Questions)
1. How do you retrieve custom attributes applied to a class?
2. Why should reflection metadata be cached?
3. What risks does the `dynamic` keyword introduce?
4. How do source generators differ from reflection emit?
5. Provide a scenario where reflection is the best tool despite its cost.

---

### 5. Asynchronous Programming Patterns

Task-based asynchronous programming keeps threads unblocked.

- **Async/Await:** `async` methods return `Task`/`Task<T>`; `await` yields control until completion. Use `ValueTask` for high-frequency scenarios with synchronous completion paths.
- **Synchronization Context:** UI frameworks have contexts; `ConfigureAwait(false)` prevents capturing context in library code.
- **Cancellation:** `CancellationToken` signals cooperative cancellation. Pass through call chain and check `token.ThrowIfCancellationRequested()`.
- **Exception handling:** Exceptions propagate through tasks; wrap awaited calls in try/catch. For background tasks, observe `Task.Exception`.
- **IAsyncEnumerable:** Stream results asynchronously with `await foreach`.

#### Quick Check (5 Questions)
1. Why should library code typically use `ConfigureAwait(false)`?
2. How do you propagate cancellation tokens through asynchronous methods?
3. What is the difference between `Task` and `ValueTask`?
4. How are exceptions surfaced from awaited tasks?
5. When is `IAsyncEnumerable` preferable to returning a list?

---

## Labs & Projects

1. **Generic Cache**
   - Create `ICache<TKey, TValue>` with constraints.
   - Support absolute and sliding expiration.
   - Implement `MemoryCache` and `DistributedCache` (mock).

2. **Event Aggregator**
   - Central event hub using delegates and events.
   - Subscribers register/unregister.
   - Include logging to illustrate invocation order.

3. **LINQ Analytics**
   - Parse CSV dataset.
   - Use query syntax to compute aggregates, groupings.
   - Compare PLINQ performance; document speedup or regressions.

4. **Reflection-Based Plugin Loader**
   - Scan `./plugins` for assemblies.
   - Discover types marked with `[Plugin]` attribute.
   - Instantiate and execute `Run()` method dynamically.

5. **Async File Downloader**
   - Download multiple files concurrently.
   - Support cancellation, progress reporting.
   - Handle throttling via `SemaphoreSlim`.

---

## Interview Check-In

- “Explain covariance and contravariance in delegates.”
- “Why is deferred execution in LINQ powerful and dangerous?”
- “What does `ConfigureAwait(false)` do?”
- “How do you define and use custom attributes?”
- Live coding: implement `IAsyncEnumerable<int>` producing Fibonacci numbers.

---

## Best Practices

- Avoid blocking calls (`.Result`, `.Wait()`) in async code.
- Use `using`/`await using` for async disposable resources.
- Keep expression trees simple; complex dynamic code can hurt maintainability.
- For generics, prefer constraints to catch errors at compile time.

---

## Resources

- [Asynchronous programming with async and await](https://learn.microsoft.com/dotnet/csharp/programming-guide/concepts/async/)
- [LINQ 101 Samples](https://learn.microsoft.com/dotnet/csharp/programming-guide/concepts/linq/linq-samples-and-tutorials)
- [Generics in .NET](https://learn.microsoft.com/dotnet/standard/generics/)
- *“Concurrency in C# Cookbook”* by Stephen Cleary

---

## Hindi Video Tutorials

- [C# Generics Explained in Hindi – WsCube Tech](https://www.youtube.com/watch?v=FfOykGGBq3g)
- [Delegates, Events and Lambda Expressions in Hindi – Geeky Shows](https://www.youtube.com/watch?v=QKqRR3YKHyA)
- [Async/Await in C# (Hindi) – CodeWithHarry](https://www.youtube.com/watch?v=GZfi4_zyGvA)

---

> ✅ **Completion Criteria:** You can write expressive, asynchronous, generic code, while understanding performance implications and being ready to discuss implementation details in interviews.

