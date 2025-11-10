# Module 03: Control Flow & Data Structures

> **Goal:** Master advanced control constructs, understand memory semantics of collections, and leverage core data structures effectively in day-to-day coding.

---

## Learning Outcomes

| # | Outcome | Evidence of Mastery |
|---|---------|---------------------|
| 1 | Apply pattern matching, switch expressions, and conditional expressions fluently | Refactored legacy `if` chains |
| 2 | Select appropriate collection types for scenarios (arrays, lists, dictionaries, sets) | Decision matrix document |
| 3 | Utilize `Span<T>`, `ReadOnlySpan<T>`, and `Memory<T>` for performance-sensitive tasks | BenchmarkDotNet report |
| 4 | Manipulate immutable collections and LINQ operations safely | Functional-style utility library |
| 5 | Understand stack vs heap allocation implications for data structures | Whiteboard explanation |

---

## Key Concepts

### Control Enhancements

- Switch expressions with guards and relational patterns.
- Pattern matching: type patterns, positional patterns, `when` clauses.
- `goto` (rare), labeled statements, `switch` fallthrough prevention.
- Null propagation patterns: `??`, `??=`, `?.`, `is null` pattern.

### Collections Overview

| Category | Type | Use Case | Notes |
|----------|------|----------|-------|
| Indexed | `array`, `List<T>` | Ordered, positional access | Arrays fixed length; List resizable |
| Keyed | `Dictionary<TKey,TValue>`, `SortedDictionary`, `ConcurrentDictionary` | Fast lookups by key | Understand hashing, concurrency semantics |
| Ordered | `Queue<T>`, `Stack<T>` | FIFO/LIFO | Ideal for BFS/DFS algorithm prep |
| Set-based | `HashSet<T>`, `SortedSet<T>` | Uniqueness enforcement | Compare O(1) vs O(log n) operations |
| Immutable | `ImmutableList<T>`, `ImmutableDictionary` | Thread-safe snapshots | Requires `System.Collections.Immutable` |

### Memory-Friendly Types

- `Span<T>` and stack allocation (`stackalloc`) basics.
- `ReadOnlySpan<T>` for safe slicing.
- `ArrayPool<T>` reuse to reduce GC pressure.

---

## Detailed Notes

### 1. Advanced Control Flow Patterns

Modern C# emphasizes readability and intent-driven branching. Replace deeply nested `if/else` chains with **pattern-based switches** or **guard clauses**. Guard clauses exit early to avoid pyramid-of-doom.

- **Switch Expressions:** Introduced in C# 8; return values directly and support pattern matching, reducing boilerplate.
- **Relational & Logical Patterns:** Combine relational operators and logical connectors in `switch` arms (e.g., `>= 0 and < 10`).
- **Property Patterns:** Match on sub-properties (`{ IsActive: true, Role: "Admin" }`).
- **Guard Clauses:** `if (input is null) throw new ArgumentNullException(nameof(input));` improves clarity.
- **Null-handling:** Use `??`, `??=`, and null pattern to short-circuit null checks.

#### Quick Check (5 Questions)
1. How do guard clauses help improve the readability of deeply nested conditionals?
2. Provide an example of using a relational pattern inside a switch expression.
3. When should you use property patterns instead of simple equality checks?
4. How does the null pattern (`is null`) differ from `== null`?
5. What advantages do switch expressions offer over statement-based switches?

---

### 2. Pattern Matching Techniques

Pattern matching in C# enables checking shape, type, and relational properties simultaneously.

- **Type Patterns:** `if (obj is Customer customer)` safely casts and evaluates in one step.
- **Positional Patterns:** Deconstruct tuples or records inside patterns (`case (int x, int y) when x == y:`).
- **`when` Clauses:** Add extra boolean predicates to patterns.
- **List Patterns (C# 11+):** Match on array/list shapes (`if (numbers is [0, .. var rest])`).
- **Exhaustiveness:** For switch expressions, compile-time checks ensure all possible cases handled when using enums or non-nullable types.

#### Quick Check (5 Questions)
1. How does a type pattern reduce boilerplate compared to traditional casting?
2. What is a positional pattern and when is it useful?
3. Explain how list patterns improve handling of structured data like arrays.
4. When would you attach a `when` clause to a pattern match?
5. How does the compiler enforce exhaustiveness in switch expressions?

---

### 3. Collection Selection Strategy

Choosing the right collection impacts performance and clarity.

- **Arrays vs Lists:** Arrays (`T[]`) are fixed-length and best for performance-critical fixed datasets. `List<T>` resizes dynamically via internal array doubling.
- **Dictionaries & Hashing:** Use `Dictionary<TKey,TValue>` for O(1) average lookups. Understand need for stable `GetHashCode` implementations.
- **Queues & Stacks:** Implement FIFO/LIFO behavior; helpful for BFS/DFS algorithms or undo stacks.
- **Sets:** Enforce uniqueness; `HashSet<T>` uses hashing, `SortedSet<T>` keeps order using balanced trees.
- **Concurrent Collections:** `ConcurrentDictionary` and `ConcurrentQueue` support multi-threaded access without manual locking.

#### Quick Check (5 Questions)
1. When would you choose an array over a `List<T>` despite reduced flexibility?
2. What requirements must types meet to be reliable dictionary keys?
3. Provide a scenario where a queue is more appropriate than a list.
4. How do concurrent collections simplify multi-threaded programming?
5. Why might `SortedSet<T>` be preferable to sorting a `List<T>` repeatedly?

---

### 4. Memory-Friendly Types (`Span<T>`, `Memory<T>`)

`Span<T>` represents contiguous memory blocks and can refer to stack or heap data without allocations. It is a `ref struct`, restricted to stack use, making it efficient for slicing arrays or parsing strings.

- **Creating Spans:** From arrays (`var span = array.AsSpan()`), stack allocations (`stackalloc int[10]`), or pointers.
- **ReadOnlySpan:** Safe variant for read-only operations; can be created from strings (`ReadOnlySpan<char> span = "hello".AsSpan();`).
- **Memory<T>:** Heap-friendly counterpart that can live on managed heap, useful for async methods.
- **ArrayPool<T>:** Rent and return arrays to reduce pressure on the garbage collector.
- **Safety:** Spans respect bounds checking but avoid new allocations, significantly improving performance in hot loops.

#### Quick Check (5 Questions)
1. Why can `Span<T>` only live on the stack and what benefit does that provide?
2. When should you use `ReadOnlySpan<T>` instead of `Span<T>`?
3. How does `ArrayPool<T>` help reduce GC pressure?
4. Why is `Memory<T>` necessary in asynchronous code?
5. Describe a scenario where slicing with spans outperforms string splitting.

---

### 5. Immutable & Functional Collections

Immutable collections from `System.Collections.Immutable` provide structural sharing, enabling thread-safe reads without locking.

- **ImmutableList/Dictionary:** Modifications return new collections sharing memory, minimizing copying.
- **Value Objects:** Combine with records to maintain immutability across domain models.
- **LINQ Pipelines:** Compose transformations using `Select`, `Where`, `GroupBy`; remember deferred execution.
- **Persistent Data Structures:** Efficient for undo/redo functionality due to versioned snapshots.
- **Functional Patterns:** Favor pure functions, avoid side-effects to maintain predictability.

#### Quick Check (5 Questions)
1. How do immutable collections achieve efficiency despite returning new instances?
2. When should you prefer immutable collections over locking with mutable collections?
3. What is deferred execution in LINQ, and how can it surprise developers?
4. Provide a use case where persistent data structures are ideal.
5. Why is immutability beneficial in multi-threaded applications?

---

## Guided Practice Sequence

1. **Pattern Matching Refactor**
   - Take nested `if/else` code and convert to `switch` expressions with guards.
   - Example scenario: shipping cost calculator based on region, loyalty level, weight.
2. **Collections Deep Dive**
   - Implement contact management list using `List<T>`, then refactor to `Dictionary`.
   - Add persistence with JSON, maintaining insertion order using `OrderedDictionary`.
3. **Span-Based Text Processing**
   - Parse CSV line using `ReadOnlySpan<char>` without allocations.
   - Compare performance with string splitting.
4. **Immutable Data Pipeline**
   - Build pipeline of transformations using immutable collections.
   - Practice with concurrency: share read-only data across threads.

---

## Coding Kata Ideas

| Kata | Focus | Stretch Goal |
|------|-------|--------------|
| Roman Numeral Converter | Switch expressions, dictionaries | Support subtractive notation, validation |
| Anagram Finder | `Dictionary` with frequency maps | Optimize using `Span<T>` and pooling |
| LRU Cache | LinkedList + Dictionary composite structure | Thread-safe version with `lock` |
| Log Analyzer | Queue for sliding window | Implement with `Channel<T>` for async |

---

## Debugging & Diagnostics

- Use Visual Studio “Diagnostic Tools” to inspect heap allocations.
- Inspect collection contents with debugger visualizers.
- Utilize `dotnet trace` or `PerfView` to analyze allocation hot paths.

---

## Interview Check-In

- “When would you choose `List<T>` vs `LinkedList<T>`? Why is `LinkedList` rarely needed?”
- “Explain the difference between `IEnumerable<T>` and `ICollection<T>`.”
- “How do pattern matching enhancements in C# 9+ improve readability?”
- Implement a live demo: “Group words by first letter using LINQ without extra lists.”
- Big-O complexity quiz for each collection type.

---

## Self-Assessment

- Can you predict the output and complexity of code using nested collections?
- Are you comfortable reading docs and choosing the right data structure?
- Have you benchmarked different implementations to justify decisions?

---

## Resources

- [Pattern matching reference](https://learn.microsoft.com/dotnet/csharp/fundamentals/functional/pattern-matching)
- [Collections best practices](https://learn.microsoft.com/dotnet/standard/collections/)
- [DotNet Runtime labs on Span](https://learn.microsoft.com/dotnet/standard/memory-and-spans/)
- [BenchmarkDotNet](https://benchmarkdotnet.org)

---

## Hindi Video Tutorials

- [C# Pattern Matching in Hindi – CodeWithHarry](https://www.youtube.com/watch?v=yth1cGukd0U)
- [Collections in C# Explained in Hindi – WsCube Tech](https://www.youtube.com/watch?v=nWCYmxzqUPQ)
- [Data Structures using C# (Hindi) – Geeky Shows](https://www.youtube.com/watch?v=Ifxg8UcbLZ8)

---

> ✅ **Completion Criteria:** You consciously choose optimal control structures and collections, can justify those choices, and have empirical evidence via benchmarks or profiling.

