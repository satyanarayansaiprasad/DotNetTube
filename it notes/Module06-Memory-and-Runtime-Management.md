# Module 06: Memory & Runtime Management

> **Goal:** Understand how the CLR manages memory, diagnose performance issues, and write memory-efficient applications.

---

## Learning Outcomes

| # | Outcome | Evidence |
|---|---------|----------|
| 1 | Explain CLR architecture, JIT compilation, and execution model | Architecture diagram (draw.io/mermaid) |
| 2 | Differentiate stack vs heap allocation and boxing/unboxing | Annotated sample code with comments |
| 3 | Use garbage collection diagnostics and tuning settings | `dotnet-counters` or `PerfView` report |
| 4 | Implement `IDisposable` patterns and `IAsyncDisposable` | Resource wrapper with unit tests |
| 5 | Analyze memory usage with profiling tools; optimize allocations | Before/after benchmark results |

---

## Key Concepts

### CLR & JIT

- Execution pipeline: IL → JIT → native code.
- Tiered compilation, ReadyToRun images.
- RyuJIT features: SIMD, hardware intrinsics.
  
### Memory Layout

- Stack frames, value types, reference types.
- Heap generations: Gen 0, Gen 1, Gen 2, Large Object Heap (LOH).
- Boxing/unboxing costs, string interning.

### Garbage Collector

- Mark-and-sweep, compacting collector.
- Workstation vs server GC.
- Configuring GC via `runtimeconfig.json`.
- Finalization queue, `GC.SuppressFinalize`.

### Resource Management

- `IDisposable` pattern, `using` statements.
- Safe handles for unmanaged resources.
- `IAsyncDisposable` and `await using`.

### Diagnostics & Tools

- `dotnet-counters`, `dotnet-gcdump`, `dotnet-trace`.
- Visual Studio Diagnostic Tools, JetBrains dotMemory.
- PerfCollect on Linux, ETW providers on Windows.

---

## Detailed Notes

### 1. CLR Architecture & JIT Compilation

The CLR loads assemblies, verifies IL, and compiles methods to native code via **RyuJIT**. Tiered compilation starts with quick Tier 0 methods for faster startup, then re-JITs frequently executed methods into optimized Tier 1 versions.

- **Metadata tables** store type information, enabling reflection and dynamic loading.
- **Security & verification:** PE verification ensures IL adheres to type safety. Skip verification with `SecurityCritical` code when necessary (rare).
- **ReadyToRun (R2R):** Precompiled native images included in assemblies reduce JIT cost at runtime; trade-off is larger binaries.
- **NGen vs Crossgen2:** Legacy vs modern tools for ahead-of-time compilation.
- **Profiling APIs:** CLR exposes hooks for profilers to intercept JIT events.

#### Quick Check (5 Questions)
1. What advantages does tiered compilation provide compared to single-pass JIT?
2. How does the CLR ensure IL is safe before execution?
3. When might you enable ReadyToRun publishing?
4. What is the role of metadata tables within assemblies?
5. How can profilers interact with the JIT process?

---

### 2. Memory Layout: Stack, Heap & Boxing

Each thread maintains a **stack** storing value types and method frames; the **managed heap** stores reference type instances. Boxing occurs when a value type is wrapped as `object`, allocating on the heap.

- **Stack frames:** Contain return addresses, local variables, and evaluation stack.
- **Managed heap generations:** Gen 0 (short-lived), Gen 1 (medium-lived), Gen 2 (long-lived), LOH (>85 KB). Promotions happen when objects survive collections.
- **Boxing/unboxing:** Implicit boxing on method calls accepting `object` or interfaces; unboxing casts back to value type. Minimize by using generics.
- **String Interning:** CLR maintains pool of literal strings; reuse via `string.Intern`.
- **Pinning:** Fix references in memory for interop; use `fixed` or `GCHandle`. Excess pinning fragments the heap.

#### Quick Check (5 Questions)
1. How does the CLR decide when to promote objects between generations?
2. What performance issues arise from frequent boxing?
3. Why should large objects be handled carefully with respect to LOH?
4. How does pinning affect the garbage collector?
5. When would manual string interning be beneficial?

---

### 3. Garbage Collection Mechanics

.NET uses a **generational, mark-and-compact** garbage collector. Collection steps: mark live objects, sweep unreachable, compact memory to reduce fragmentation.

- **Workstation vs Server GC:** Workstation optimized for responsive desktop apps; Server GC for throughput on multi-core servers (background GC).
- **Background GC:** Processes Gen 2 concurrently with application threads, reducing pauses.
- **Configuration:** `runtimeconfig.json` options (`System.GC.Server`, `System.GC.HeapHardLimit`, `System.GC.LatencyLevel`) adjust behavior.
- **Finalizers:** Run on special finalizer thread; objects needing finalization move to finalization queue. Use `GC.SuppressFinalize` after manual cleanup.
- **Large object heap (LOH):** Not compacted by default; minimize large allocations or use array pooling.

#### Quick Check (5 Questions)
1. How does background GC differ from non-concurrent GC?
2. When would you enable server GC in a web application?
3. What is the purpose of `GC.SuppressFinalize`?
4. How can you reduce LOH fragmentation?
5. Which settings in `runtimeconfig.json` influence GC behavior?

---

### 4. Resource Management & Disposal Patterns

Use `IDisposable` for deterministic release of unmanaged resources (file handles, sockets). Implement the **Dispose pattern** to free managed/unmanaged resources safely.

```csharp
public class FileProcessor : IDisposable
{
    private readonly FileStream _stream;
    private bool _disposed;

    public FileProcessor(string path) => _stream = File.OpenRead(path);

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing) _stream.Dispose();
        _disposed = true;
    }
}
```

- **SafeHandle:** Wrap unmanaged handles to avoid finalizer pitfalls.
- **IAsyncDisposable:** Use for async cleanup (e.g., asynchronous streams) with `await using`.
- **Using declarations:** `using var stream = File.OpenRead(path);` ensures disposal even on exceptions.
- **Finalizers:** Only when unmanaged resources must be released as last resort.
- **Resource leaks:** Detect via `IDisposable` analyzers and memory profilers.

#### Quick Check (5 Questions)
1. Why should `Dispose` call `GC.SuppressFinalize`?
2. When do you need both finalizers and `IDisposable`?
3. How does `await using` improve asynchronous resource cleanup?
4. What role does `SafeHandle` play in resource management?
5. How can Roslyn analyzers help enforce disposal best practices?

---

### 5. Diagnostics & Performance Tooling

Profiling tools reveal allocation hotspots and leaks.

- **dotnet-counters:** Live metrics (GC heap size, CPU usage). Use `dotnet-counters monitor --process-id`.
- **dotnet-trace:** Collects EventPipe traces for later analysis; integrate with PerfView or speedscope.
- **dotnet-gcdump:** Captures GC heap snapshots to inspect object graphs and references.
- **Visual Studio Diagnostics:** Memory Usage tool, Timeline profiler, performance snapshots.
- **Third-party tools:** JetBrains dotMemory, Redgate ANTS for deeper analysis; PerfCollect for Linux capturing perf events.

Workflow: reproduce issue, capture baseline, apply fix, recapture to verify improvement. Combine with `BenchmarkDotNet` for microbenchmarks ensuring changes deliver measurable gains.

#### Quick Check (5 Questions)
1. When would you use `dotnet-gcdump` instead of `dotnet-trace`?
2. How do live performance counters assist in diagnosing production issues?
3. Describe a workflow to analyze memory leaks using Visual Studio.
4. What advantages do third-party profilers offer over built-in tools?
5. How does BenchmarkDotNet complement profiling tools?

---

## Practical Labs

1. **Memory Visualizer**
   - Create sample app allocating arrays.
   - Track GC collections in console using `GC.CollectionCount`.
   - Observe generation promotions.

2. **Boxing Detector**
   - Use analyzers (`CA1800`) to spot boxing.
   - Refactor code to avoid unnecessary boxing, compare benchmarks.

3. **Dispose Correctly**
   - Implement class wrapping `FileStream`, override finalizer.
   - Demonstrate pitfalls when forgetting to dispose.
   - Use `IDisposable` pattern with guards.

4. **LOH Analysis**
   - Allocate large arrays (>85,000 bytes).
   - Use `dotnet-gcdump` to inspect LOH usage.
   - Experiment with array pooling to mitigate.

5. **Async Resource Handling**
   - Build a streaming API using `IAsyncEnumerable`.
   - Ensure async disposal is used with `await using`.

---

## Interview Check-In

- “Describe how the garbage collector works in .NET.”
- “When does boxing occur? Why is it expensive?”
- “How would you investigate a memory leak in production?”
- “Explain finalizers vs `IDisposable`. When do you need both?”
- Whiteboard generational GC lifecycle.

---

## Monitoring Checklist

- Enable `DOTNET_GCHeapHardLimit` to simulate constrained memory.
- Add counters to `EventCounters` to monitor GC at runtime.
- Document metrics: GC pause time, allocations/sec, LOH usage.

---

## Resources

- [Memory management fundamentals](https://learn.microsoft.com/dotnet/standard/garbage-collection/fundamentals)
- [Performance diagnostics tools](https://learn.microsoft.com/dotnet/core/diagnostics/)
- [IDisposable guidelines](https://learn.microsoft.com/dotnet/standard/garbage-collection/implementing-dispose)
- *Pro .NET Memory Management* by Konrad Kokosa.

---

## Hindi Video Tutorials

- [CLR and Garbage Collection Explained in Hindi – Geeky Shows](https://www.youtube.com/watch?v=IuOjFkWDXLI)
- [.NET Memory Management Basics in Hindi – WsCube Tech](https://www.youtube.com/watch?v=kVxE8JMBxwU)
- [IDisposable and Using Statement in Hindi – CodeDecode](https://www.youtube.com/watch?v=0d0n0KCbF70)

---

> ✅ **Completion Criteria:** You can articulate CLR internals, optimize memory usage, and diagnose runtime issues with professional tooling.

