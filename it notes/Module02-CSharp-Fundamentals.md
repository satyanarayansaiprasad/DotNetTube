# Module 02: C# Fundamentals

> **Goal:** Build a solid foundation in C# syntax, primitive types, operators, and essential programming constructs. This module ensures you can write, read, and debug small programs confidently.

---

## Learning Outcomes

| # | Outcome | Evidence of Mastery |
|---|---------|---------------------|
| 1 | Use primitive types, literals, and type inference appropriately | Coding exercise workbook |
| 2 | Apply arithmetic, comparison, logical, and bitwise operators safely | Unit-tested snippets |
| 3 | Control program flow with conditionals and loops | Mini projects: calculator & number guessing game |
| 4 | Write reusable methods with proper parameters, return types, and documentation | XML comments + generated docs |
| 5 | Debug with breakpoints, step-through, watch windows, and `Console.WriteLine` | Recorded debugging session |

---

## Micro-Topics Map

### Core Syntax

- Namespaces, `using` directives, `Main` method structure
- Statements vs expressions, semicolons, indentation conventions
- Comments (`//`, `/* */`, `///`) and XML documentation

### Types & Variables

- Value types (`int`, `double`, `bool`, `char`, `decimal`)
- Reference types (`string`, `object`)
- Implicit (`var`) vs explicit declarations
- Numeric literal suffixes, binary/hex literals, digit separators
- Nullable types (`int?`) basics

### Operators

- Arithmetic, assignment, compound assignment (`+=`)
- Comparison (`==`, `!=`, `<`, `>`), equality pitfalls with `double`
- Logical (`&&`, `||`, `!`), short-circuit evaluation
- Bitwise (`&`, `|`, `^`, `~`, `<<`, `>>`)
- Null-coalescing (`??`) and null-conditional (`?.`)

### Control Flow

- `if/else`, `switch` (classic + pattern matching forms)
- Loops: `for`, `foreach`, `while`, `do..while`
- Jump statements: `break`, `continue`, `return`
- Exception basics: `try/catch/finally`, throw expressions

### Methods & Parameters

- Method signature anatomy
- Pass-by-value vs `ref`/`out` parameters
- Named and optional arguments
- Expression-bodied members for brevity

---

## Detailed Notes

### 1. Syntax & Program Structure

A C# program begins with namespaces, type definitions, and an entry point (`Main`). C# uses **block scoping** with braces `{ }` and enforces semicolons to terminate statements. The Roslyn compiler supports **top-level statements**, allowing concise scripts for simple programs. Organize code into namespaces reflecting folder structure, and adopt **PascalCase** for types/methods and **camelCase** for locals.

- **Statements vs expressions:** Statements perform actions; expressions evaluate to values. For example, `var sum = a + b;` contains an expression (`a + b`) within an assignment statement.
- **Documentation comments:** `///` comments generate XML docs. Apply to public APIs to assist IntelliSense.
- **Using directives:** At top of file, import namespaces. .NET 6 introduced **implicit global usings**, reducing repetitive directives.
- **Top-level programs:** Great for quick utilities; for larger apps revert to explicit `Program` class to maintain clarity.

#### Quick Check (5 Questions)
1. What is the difference between a statement and an expression in C#?
2. When should you use top-level statements, and when should you avoid them?
3. How do implicit global usings change the way you structure files?
4. Why are XML documentation comments valuable in larger projects?
5. Which naming conventions should you follow for classes, methods, and variables?

---

### 2. Types, Variables & Conversions

C# divides types into **value types** (stored on the stack when possible) and **reference types** (heap-allocated). Value types include primitives (`int`, `float`, `bool`, `struct`) and are copied by value. Reference types (classes, arrays, delegates) store a reference to data. Use `var` for **implicit typing** when the right-hand side reveals type at compile time—this is still statically typed.

- **Numeric literals:** Add suffixes (`L` for long, `f` for float, `m` for decimal). Use binary (`0b1010`) and hex (`0xFF`) for clarity. `_` digit separators improve readability (`1_000_000`).
- **Conversion methods:** Explicit casting `(int)doubleValue`, helper classes (`Convert.ToInt32`), parsing (`int.Parse`, `int.TryParse`).
- **Nullable value types:** `int?` wraps `Nullable<int>`, allowing representation of missing data. Use `HasValue` or null-coalescing to supply defaults.
- **Strings:** Immutable reference type; concatenation creates new instances. Prefer `StringBuilder` for large loops.

#### Quick Check (5 Questions)
1. How does implicit typing with `var` still preserve static typing?
2. Give an example where `decimal` is a better choice than `double`.
3. What happens when you cast a `double` with fraction to `int`?
4. Why should you prefer `TryParse` over `Parse` for user input?
5. How do nullable value types help when interacting with databases?

---

### 3. Operators & Expressions

Operators perform manipulations across operands. Mastery of precedence and associativity prevents logic bugs.

- **Arithmetic:** `+`, `-`, `*`, `/`, `%` follow BIDMAS; integer division truncates decimals.
- **Assignment:** `+=`, `-=`, `*=`, etc., reduce verbosity. Use `??=` to assign defaults when null.
- **Comparison & equality:** `==` checks value equality; override for custom types. Use `ReferenceEquals` for reference comparison.
- **Logical:** `&&` and `||` short-circuit. Use `&` and `|` (non-short-circuit) cautiously.
- **Pattern matching:** `is`, `switch` expressions enable concise logic. Example:

```csharp
var result = temperature switch
{
    < 0 => "Freezing",
    >= 0 and <= 25 => "Mild",
    > 25 => "Hot"
};
```

#### Quick Check (5 Questions)
1. How does integer division differ from floating-point division in C#?
2. When would you use the null-coalescing assignment operator?
3. Explain the difference between `&&` and `&` in conditional statements.
4. What advantages do switch expressions offer over traditional `switch` statements?
5. How do you implement custom equality for a type to work correctly with `==`?

---

### 4. Control Flow Techniques

Control structures guide execution paths.

- **Conditionals:** `if`, `else if`, `else` handle branching; use pattern matching within `switch` for readability.
- **Loops:** `for` suits index-based iteration, `foreach` for enumerables, `while` for sentinel-controlled loops, `do..while` for post-validated loops.
- **Break/continue:** `break` exits loop/switch; `continue` skips to next iteration.
- **Exception handling:** Use `try/catch/finally` to manage errors gracefully. Throw specific exceptions (`ArgumentNullException`, `InvalidOperationException`). Use `when` filters to narrow conditions.
- **Pattern-based switch:** Combine relational, logical, and type patterns to reduce nested conditionals.

#### Quick Check (5 Questions)
1. When should you prefer `foreach` over `for`?
2. How do you avoid infinite loops with `while` loops?
3. Provide an example where a `switch` expression increases readability.
4. What is the purpose of the `finally` block in exception handling?
5. How do exception filters (`when`) help tailor catch blocks?

---

### 5. Methods, Parameters & Debugging

Methods encapsulate logic, promoting reuse. Define return types, method names, and parameter lists thoughtfully.

- **Parameters:** Pass-by-value by default; use `ref` for read/write references, `out` for output-only, `in` for read-only references. Optional parameters provide defaults; named arguments improve clarity when multiple optional parameters exist.
- **Expression-bodied members:** Provide concise syntax for simple property getters or one-line methods.
- **Overloading:** Methods with same name but different signatures; ensure behavior remains intuitive.
- **Debugging workflow:** Set breakpoints (`F9`), step into (`F11`), step over (`F10`), inspect locals, evaluate expressions in Immediate window. Use `Console.WriteLine` logging sparingly; prefer debugger.
- **Assertions:** `Debug.Assert` for invariants during development.

#### Quick Check (5 Questions)
1. How do `ref` and `out` parameters differ semantically?
2. When should you use expression-bodied members?
3. What benefits do named arguments provide when calling methods?
4. Why is stepping into functions useful during debugging?
5. How can assertions assist in catching logic errors early?

---

## Study Routine (Suggested 1 Day)

| Phase | Duration | Focus | Deliverable |
|-------|----------|-------|-------------|
| Primer | 1 hour | Read docs/tutorials | Annotated notes |
| Guided Practice | 2 hours | Follow pair-programming style walk-through | Recording or journal |
| Independent Coding | 3 hours | Implement exercises below | Git repo with tags |
| Reflection | 30 min | Summarize learning & questions | Journal entry |

---

## Practice Exercises

1. **Data Type Playground**
   - Create `TypePlayground.cs`.
   - Declare variables for each primitive type, showing default values via `default(T)`.
   - Write methods demonstrating implicit/explicit casts, `Convert` class usage, and `Parse/TryParse`.
   - Add unit tests verifying conversions and overflow checks.

2. **CLI Calculator**
   - Prompt user for two numbers and an operation (`+`, `-`, `*`, `/`, `%`).
   - Validate inputs, handle divide-by-zero gracefully.
   - Offer loop to continue operations until user exits.

3. **Number Guessing Game**
   - Random number 1-100, with clues “higher/lower”.
  - Track attempts and display performance message.
  - Optional: Implement difficulty levels and scoreboard persisted to file.

4. **FizzBuzz Variations**
   - Classic FizzBuzz (1..100).
   - Parameterized version allowing custom divisors and words.
   - Implement using `switch` expression for modern style.

---

## Debugging Lab

- Set breakpoints in the calculator application.
- Inspect variables with locals window and watch expressions.
- Step into/out/over functions.
- Use conditional breakpoints (e.g., break when `operation` invalid).
- Capture screenshot or log of debugging session.

---

## Interview Check-In

- Explain difference between `value` and `reference` types.
- Describe how `var` works; is it dynamic typing?
- Walk through what happens when casting from `double` to `int`.
- Solve this live: “Reverse a string without using `Array.Reverse`.”
- Whiteboard the flow for your number guessing game.

---

## Self-Assessment

- Can you write a program from scratch without IDE templates?
- Do you rely on code completion, or can you type the syntax manually?
- Are you confident reading existing code and predicting its output?
- Log common mistakes (e.g., off-by-one errors) and prevention strategies.

---

## Resources

- [C# Fundamentals on Microsoft Learn](https://learn.microsoft.com/training/paths/csharp-first-steps/)
- [C# Language Reference](https://learn.microsoft.com/dotnet/csharp/language-reference/)
- [Sharplab.io](https://sharplab.io) — explore how code compiles to IL
- [dotnetfiddle.net](https://dotnetfiddle.net) — practice in browser
- Book recommendation: *“C# 12 in a Nutshell”* (Albahari)

---

## Hindi Video Tutorials

- [C# Programming Basics in Hindi – WsCube Tech Playlist](https://www.youtube.com/playlist?list=PLjpp5kBQLNTSdK6U_1FpiU6Bfq5lH0YV8)
- [C# Operators and Control Statements in Hindi – Harshit Vashisth](https://www.youtube.com/watch?v=Eg8l7W82dKo)
- [C# Methods and Functions Explained in Hindi – Geeky Shows](https://www.youtube.com/watch?v=SzuI8Qap2ZA)

---

> ✅ **Completion Criteria:** You can implement non-trivial console programs, debug them, and explain fundamental C# constructs to someone else.

