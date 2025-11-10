# Module 12: Microservices & Messaging

> **Goal:** Architect distributed systems with .NET, leveraging microservices patterns, messaging, and resilience techniques.

---

## Learning Outcomes

| # | Outcome | Evidence |
|---|---------|----------|
| 1 | Design microservice boundaries and communication strategies | Context map diagram |
| 2 | Implement REST and gRPC services with proper contracts | Dual-protocol demo |
| 3 | Integrate messaging systems (RabbitMQ, Azure Service Bus) | Event-driven sample |
| 4 | Apply resilience patterns (retry, circuit breaker, bulkhead) | Polly policy library |
| 5 | Implement service discovery, configuration, and observability | Docker Compose + monitoring stack |

---

## Architectural Foundations

- Monolith vs modular monolith vs microservices trade-offs.
- Bounded contexts, domain-driven design alignment.
- Data ownership per service; eventual consistency.
- API gateways (YARP, Ocelot) and BFF (Backend-for-Frontend) patterns.

### Communication Patterns

- Synchronous: REST, gRPC, GraphQL.
- Asynchronous: message queues, event streams, pub/sub.
- Saga pattern for long-running transactions.

---

## Detailed Notes

### 1. Domain-Driven Boundaries & Architecture

Microservices align with **bounded contexts** from Domain-Driven Design. Each service owns its domain model, database, and release lifecycle.

- **Trade-offs:** Monoliths centralize logic; microservices improve scalability and autonomy but increase operational complexity.
- **Context Mapping:** Identify upstream/downstream relationships, customer/supplier patterns.
- **Database per service:** Avoid shared schemas to prevent coupling; communicate via APIs/events.
- **API Gateway:** Central entry point for routing, aggregation, rate limiting. YARP or Ocelot provide routing and policy enforcement.
- **BFF Pattern:** Create specialized gateways for different clients (web, mobile) to prevent over-fetching.

#### Quick Check (5 Questions)
1. Why should each microservice own its data store?
2. What problems do shared databases across services introduce?
3. How does an API Gateway simplify client interactions?
4. When is a Backend-for-Frontend pattern appropriate?
5. How do bounded contexts align with service boundaries?

---

### 2. Communication Styles: REST, gRPC, GraphQL

- **REST:** Human-readable, cache-friendly, widely supported. Use for public APIs or when simplicity matters.
- **gRPC:** High-performance binary protocol using HTTP/2; ideal for internal service-to-service communication. Contracts defined via `.proto`.
- **GraphQL:** Client-driven queries; reduces over/under-fetching. Suitable for BFF scenarios.
- **Versioning & Contracts:** Use semantic versioning, compatibility strategies, and contract testing (Pact) to prevent breaking changes.
- **Serialization:** JSON (REST), Protobuf (gRPC). Benchmark for latency/throughput differences.

#### Quick Check (5 Questions)
1. When is gRPC a better choice than REST?
2. How do you version REST APIs without breaking clients?
3. What advantages does GraphQL provide in microservice ecosystems?
4. How do contract tests help mitigate communication breakages?
5. Why is HTTP/2 important for gRPC services?

---

### 3. Messaging & Event-Driven Design

Asynchronous communication enables loose coupling and resilience.

- **Message Brokers:** RabbitMQ, Azure Service Bus, Kafka. Choose based on ordering, persistence, throughput needs.
- **Event Types:** Domain events signal business changes; integration events share data across bounded contexts.
- **Delivery Semantics:** At-most-once, at-least-once, exactly-once (rare). Implement idempotency and deduplication to handle retries.
- **Saga Pattern:** Coordinate distributed transactions using choreography (events) or orchestration (central coordinator).
- **Dead-letter queues:** Capture poison messages for inspection; implement retry policies with exponential backoff.

#### Quick Check (5 Questions)
1. What differentiates domain events from integration events?
2. How do you guarantee idempotency when consuming events?
3. When should you choose choreography vs orchestration for sagas?
4. What role do dead-letter queues play in messaging systems?
5. How does Azure Service Bus differ from RabbitMQ?

---

### 4. Resilience & Fault Tolerance

Distributed systems must tolerate transient failures.

- **Polly Policies:** Implement retries (`WaitAndRetry`), circuit breakers (`CircuitBreaker`), timeouts, fallback, bulkhead isolation.
- **Transient Fault Handling:** Use exponential backoff and jitter to avoid thundering herd.
- **Idempotency:** Ensure repeated requests produce same outcome; store request IDs.
- **API Gateways:** Apply global policies at edge layer for consistency.
- **Chaos Engineering:** Introduce controlled failures to validate resilience strategies.

#### Quick Check (5 Questions)
1. What problem does the circuit breaker pattern solve?
2. How do retry policies with jitter prevent cascading failures?
3. Define idempotency and give an API example.
4. When should bulkhead isolation be applied?
5. How can chaos engineering improve system reliability?

---

### 5. Observability, Deployment & Configuration

Operational excellence relies on visibility and automation.

- **Observability Pillars:** Logs, metrics, traces. Use OpenTelemetry to standardize telemetry collection.
- **Service Discovery:** Consul, Eureka, Kubernetes DNS provide discovery. Clients resolve service endpoints dynamically.
- **Configuration Management:** Centralize using Consul KV, Azure App Configuration, or environment variables; reload at runtime when possible.
- **Containerization:** Multi-stage Dockerfiles reduce image size; orchestrate with Kubernetes for scaling, rolling updates, and self-healing.
- **CI/CD Pipelines:** Automate builds, tests, image publishing, and deployments; use blue/green or canary strategies.

#### Quick Check (5 Questions)
1. Why is distributed tracing essential in microservices?
2. How does Kubernetes perform service discovery?
3. What benefits does centralized configuration management deliver?
4. Why use multi-stage Docker builds for .NET services?
5. What deployment strategies minimize downtime during releases?

---

## Hands-On Projects

1. **Service Catalog**
   - Define `CatalogService` (product data) and `OrderService`.
   - Expose REST endpoints (`/products`, `/orders`).
   - Implement API gateway with YARP.

2. **gRPC Interop**
   - Create gRPC service for inventory checks.
   - Share proto contracts; generate C# clients.
   - Integrate with existing REST-based services.

3. **Messaging Integration**
   - Use RabbitMQ (Docker container) for order events.
   - Publish `OrderCreated` events; subscribe in `NotificationService`.
   - Implement dead-letter queue handling.

4. **Resilience Policies**
   - Use Polly for retries with exponential backoff.
   - Add circuit breaker and timeout policies.
   - Log policy outcomes for monitoring.

5. **Service Discovery & Config**
   - Implement configuration via Consul or Steeltoe + Spring Cloud Config.
   - Demonstrate environment-based overrides.
   - Containerize services with Docker Compose or Kubernetes manifests.

6. **Observability Stack**
   - Collect logs with Serilog + Seq/ELK.
   - Expose metrics via Prometheus exporters.
   - Trace requests end-to-end with OpenTelemetry + Jaeger.

---

## Interview Check-In

- “How do you decide when to break a monolith into microservices?”
- “Explain eventual consistency and how to handle it in .NET.”
- “What is the Saga pattern, and how would you implement it?”
- “Compare REST vs gRPC in terms of performance and contract enforcement.”
- Scenario: design a payment workflow with idempotency and retries.

---

## Deployment Considerations

- Containerization: Dockerfiles, multi-stage builds.
- Orchestration: Docker Compose vs Kubernetes (AKS).
- Rolling updates, blue/green deployments.
- Observing service health with liveness/readiness probes.

---

## Resources

- [.NET Microservices Architecture Guide](https://dotnet.microsoft.com/en-us/apps/aspnet/microservices-architecture)
- [gRPC for .NET](https://learn.microsoft.com/aspnet/core/grpc/)
- [Polly resilience library](https://www.thepollyproject.org/)
- [Dapr (Distributed Application Runtime)](https://docs.dapr.io/)
- [Azure Service Bus documentation](https://learn.microsoft.com/azure/service-bus-messaging/)

---

## Hindi Video Tutorials

- [Microservices Architecture in Hindi – WsCube Tech](https://www.youtube.com/watch?v=J4fLNRr1IuY)
- [RabbitMQ Messaging with .NET (Hindi) – CodeDecode](https://www.youtube.com/watch?v=89M0CuxnxO8)
- [Polly Resilience Library Explained in Hindi – Geeky Shows](https://www.youtube.com/watch?v=5Q5S-mMtXgY)

---

> ✅ **Completion Criteria:** You can design and implement robust microservices with proper communication, resilience, and observability patterns suitable for production environments.

