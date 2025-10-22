# Overview

**Subscription Management** is a backend API that manages subscription plans and user subscriptions with production-grade concerns out of the box.

- Language/Runtime: **.NET 8**
- Persistence: **PostgreSQL** (EF Core/Npgsql)
- Messaging: **Outbox** (EF) → ready for **RabbitMQ/MassTransit**
- Caching: **Redis** (optional)
- Security: **JWT/OIDC**, **Policy-based authorization**
- Observability: **OpenTelemetry**, **Prometheus /metrics**
- API: **Swagger/OpenAPI**, **Versioning**, **ProblemDetails**

## Repository Goals
- Demonstrate a **real-world, senior-level** design that can scale from **modular monolith → microservices**.
- Provide **decision records (ADR)** and **production playbooks**.
- Offer a solid DX: Idempotency, consistent errors, correlation-ready logs.

