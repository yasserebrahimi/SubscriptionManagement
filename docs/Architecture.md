# Architecture

## Clean Architecture Layers
- **Domain**: entities, value objects, domain events, invariants.
- **Application**: CQRS (MediatR) handlers, validators (FluentValidation), mapping (AutoMapper).
- **Infrastructure**: EF Core + Npgsql, Idempotency store, Outbox (EF + background worker), MassTransit/RabbitMQ, Redis, OpenTelemetry exporters.
- **Presentation**: ASP.NET Core WebAPI (Swagger, API Versioning, middlewares).

## Diagrams

### High level


```mermaid
flowchart LR
  UI["Clients"]
  API["WebAPI"]
  APP["Application"]
  DOM["Domain"]
  INF["Infrastructure"]
  DB["/ PostgreSQL"]
  BUS["/ RabbitMQ"]
  REDIS["/ Redis"]
  METRICS["Prometheus /metrics"]
  UI --> API
  API --> APP
  APP --> DOM
  APP --> INF
  INF --> DB
  INF --> BUS
  INF --> REDIS
  API --> METRICS
```


