# Module 08: Data Access & Persistence

> **Goal:** Master data access strategies in .NET, from ADO.NET basics to Entity Framework Core, and understand when to choose alternative ORMs or direct SQL.

---

## Learning Outcomes

| # | Outcome | Evidence |
|---|---------|----------|
| 1 | Connect to relational databases using ADO.NET | Sample repo executing CRUD with `SqlConnection` |
| 2 | Model entities and relationships with Entity Framework Core | Migration history + EF Core diagrams |
| 3 | Optimize performance with tracking options, eager/lazy loading, and raw SQL | Benchmark and query plan analysis |
| 4 | Implement transactions, concurrency control, and data validation | Integration tests with concurrency scenarios |
| 5 | Evaluate when to use Dapper or other micro-ORMs | Decision log with performance comparison |

---

## Topics Overview

### ADO.NET Essentials

- Connection strings, `SqlConnection`, `SqlCommand`, `SqlDataReader`.
- Parameterized queries to prevent SQL injection.
- Stored procedures, output parameters.
- Transactions with `SqlTransaction`, `TransactionScope`.

### Entity Framework Core

- DbContext configuration (`OnConfiguring`, dependency injection).
- DbSet, entity configuration (conventions vs Fluent API).
- Relationships: one-to-one, one-to-many, many-to-many.
- Shadow properties, owned types, value conversions.
- Migrations: add, remove, revert, seed data.

### Query Behavior

- Change tracking vs `AsNoTracking`.
- Loading strategies: eager (`Include`), explicit, lazy (proxies).
- Raw SQL (`FromSqlInterpolated`), stored procedure mapping.
- Compiled queries for hot paths.

### Performance & Resilience

- Batching (`SaveChanges` vs `SaveChangesAsync`).
- Connection pooling behavior.
- Retry policies (Polly, transient fault handling).
- Database logging, interceptors.

### Alternatives & Ecosystem

- Dapper basics (`Query`, `Execute`, multi-mapping).
- Repo/unit-of-work pattern pros/cons.
- NoSQL options: Cosmos DB SDK, MongoDB driver.

---

## Detailed Notes

### 1. ADO.NET Fundamentals

ADO.NET offers low-level access to relational databases.

- **Connections:** `SqlConnection`, `NpgsqlConnection`, `MySqlConnection`; wrap in `using` to ensure closure.
- **Commands:** `SqlCommand` executes SQL or stored procedures. Parameterize queries to prevent SQL injection (`command.Parameters.AddWithValue("@Id", id);`).
- **Data Readers:** `SqlDataReader` streams results forward-only; efficient for large datasets. Use `GetFieldValue<T>` for typed access.
- **DataAdapters/DataSets:** Legacy disconnected model; less common in modern apps but useful for older architectures.
- **Async APIs:** `ExecuteReaderAsync`, `ExecuteNonQueryAsync` prevent blocking threads in ASP.NET Core.

#### Quick Check (5 Questions)
1. Why must you always parameterize SQL commands?
2. How does `SqlDataReader` differ from `DataSet`?
3. What is the purpose of the `using` statement with database connections?
4. How do asynchronous ADO.NET methods improve server scalability?
5. When would you use stored procedures instead of inline SQL?

---

### 2. Entity Framework Core Modeling

EF Core simplifies object-relational mapping.

- **DbContext:** Central unit managing entity sets; configure in `OnConfiguring` or via DI.
- **Entities:** POCO classes; configure with data annotations or Fluent API (`modelBuilder.Entity<Order>().HasKey(o => o.Id);`).
- **Relationships:** Use navigation properties; configure one-to-many (`HasMany`/`WithOne`), many-to-many (shadow join entity or `HasMany().WithMany()` in EF Core 5+).
- **Migrations:** `dotnet ef migrations add InitialCreate`; apply with `dotnet ef database update`.
- **Value Conversions:** Map complex types (enums, value objects) to columns; use `OwnsOne` for owned entity types.

#### Quick Check (5 Questions)
1. How do you register a `DbContext` with dependency injection?
2. When should you use Fluent API over data annotations?
3. What steps generate and apply an EF Core migration?
4. How do you model a many-to-many relationship without explicitly creating a join entity?
5. How can value converters help map enums to friendly column types?

---

### 3. Query Execution & Performance

EF Core translates LINQ queries to SQL. Understand query lifecycle to avoid surprises.

- **Tracking vs No-Tracking:** `AsNoTracking()` disables change tracking for read-only queries, improving performance.
- **Eager vs Lazy Loading:** Eager uses `Include`, lazy loading requires proxies and can trigger N+1 issues. Explicit loading via `Entry` API for precise control.
- **Compiled Queries:** `EF.CompileQuery` caches query plans for repeated operations.
- **Raw SQL:** `FromSqlInterpolated` executes custom SQL while still mapping to entities. Use `ExecuteSqlRaw` for commands.
- **Query optimization:** Inspect SQL via `ToQueryString()`. Use indexes, limit projection (`Select` into DTOs), and pagination.

#### Quick Check (5 Questions)
1. When should you use `AsNoTracking()`?
2. What risks do lazy-loaded navigation properties introduce?
3. How do compiled queries improve EF Core performance?
4. How can you inspect the SQL generated by a LINQ query?
5. When is it appropriate to use `FromSqlInterpolated`?

---

### 4. Transactions, Validation & Concurrency

Ensure data integrity with transactions and validation.

- **Transactions:** Use `DbContext.Database.BeginTransaction()` or ambient `TransactionScope`. Always commit/rollback.
- **Concurrency Tokens:** Configure timestamp/rowversion columns; catch `DbUpdateConcurrencyException` to resolve conflicts.
- **Validation:** Use data annotations or FluentValidation to enforce constraints before hitting the database.
- **Unit of Work:** `DbContext` acts as unit of work; call `SaveChanges` to persist modifications in a transaction.
- **Optimistic vs Pessimistic Concurrency:** EF Core favors optimistic; use raw SQL or stored procedures for locks if needed.

#### Quick Check (5 Questions)
1. How do you start and commit a transaction manually in EF Core?
2. What is optimistic concurrency and how does EF Core implement it?
3. How do you surface validation errors before saving data?
4. When would you use `TransactionScope` over `BeginTransaction`?
5. How should you handle a `DbUpdateConcurrencyException`?

---

### 5. Alternative Data Access Approaches

- **Dapper:** Micro-ORM focused on performance. `connection.Query<T>("SELECT * FROM Orders")` maps rows to objects. Supports multi-mapping for joins.
- **Repository Pattern:** Provides abstraction over data access; can add complexity. Evaluate carefully; EF Core already offers unit-of-work semantics.
- **NoSQL:** Azure Cosmos DB for globally distributed data; use SDK and partition keys. MongoDB driver integrates with strongly typed models.
- **Hybrid Approaches:** Combine EF Core for CRUD, Dapper for reporting. Ensure connection management is consistent.
- **Caching:** Use Redis or in-memory cache to reduce database round-trips for frequently accessed data.

#### Quick Check (5 Questions)
1. Why might Dapper outperform EF Core in read-heavy scenarios?
2. What are the downsides of the generic repository pattern with EF Core?
3. How does partitioning work in Azure Cosmos DB?
4. When is a hybrid data access approach justified?
5. How can caching layers complement your persistence strategy?

---

## Hands-On Labs

1. **Northwind Explorer**
   - Scaffold EF Core model from existing database (`dotnet ef dbcontext scaffold`).
   - Build console UI to query products, categories, orders.
   - Add CLI arguments for filtering and pagination.

2. **Migration Workflow**
   - Create code-first schema using migrations.
   - Apply migrations per environment (`Development`, `Staging`).
   - Implement seeders for test data.

3. **Transaction Scenarios**
   - Simulate funds transfer between accounts.
   - Ensure atomicity using `TransactionScope`.
   - Introduce error to validate rollback.

4. **Concurrency Control**
   - Add concurrency token (`[ConcurrencyCheck]` or rowversion).
   - Simulate conflicting updates.
   - Handle `DbUpdateConcurrencyException`.

5. **Dapper Performance Check**
   - Implement reporting query in both EF Core and Dapper.
   - Use BenchmarkDotNet to compare speed.
   - Document trade-offs.

---

## Interview Check-In

- “Explain the difference between eager and lazy loading.”
- “How do migrations work in EF Core?”
- “What strategies help prevent SQL injection?”
- “When would you prefer Dapper over EF Core?”
- Live question: “Design a repository pattern for `Order` with unit of work; discuss drawbacks.”

---

## Best Practices Checklist

- Centralize connection strings in configuration, don’t hardcode.
- Use async methods for database calls to avoid blocking threads.
- Validate inputs at both domain and database levels.
- Monitor queries via `ToQueryString()` and SQL Profiler.
- Log slow queries and capture execution plans.

---

## Resources

- [Entity Framework Core Docs](https://learn.microsoft.com/ef/core/)
- [ADO.NET documentation](https://learn.microsoft.com/dotnet/framework/data/adonet/)
- [Dapper GitHub](https://github.com/DapperLib/Dapper)
- [Polly for resilience](https://www.thepollyproject.org/)

---

## Hindi Video Tutorials

- [Entity Framework Core Tutorial in Hindi – WsCube Tech](https://www.youtube.com/watch?v=r5BnS5KXc5Q)
- [ADO.NET Complete Course in Hindi – Geeky Shows](https://www.youtube.com/playlist?list=PLbGui_ZYuhig6FY6kuoF_ItrHjSje4QdR)
- [Dapper Micro ORM in Hindi – CodeDecode](https://www.youtube.com/watch?v=w7RCzPSx_DS)

---

> ✅ **Completion Criteria:** You can design data models, implement robust data access layers, and tune for performance and reliability in production scenarios.

