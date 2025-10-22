# Clean Architecture

## Layers
- **Domain**: Aggregates, Entities, Value Objects, Domain Events, and invariants.
- **Application**: Use cases via CQRS (MediatR); validation (FluentValidation); projection via AutoMapper.
- **Infrastructure**: EF Core + Npgsql; Idempotency store; Outbox with background processor; Redis; OTEL exporters; bus adapters.
- **Presentation**: ASP.NET Core WebAPI with a hardened middleware pipeline.

```mermaid
flowchart LR
  UI --> P[Presentation]
  P --> A[Application]
  A --> D[Domain]
  A --> I[Infrastructure]
  I --> DB[(PostgreSQL)]
  I --> Cache[(Redis)]
  I --> Bus[(RabbitMQ)]
```

## Middleware Order (critical)
1. `UseExceptionHandler` (ProblemDetails)
2. `CorrelationIdMiddleware`
3. `SecurityHeadersMiddleware`
4. `UseRateLimiter`
5. `UseCors`
6. `UseAuthentication`
7. `UseAuthorization`
8. `MapControllers`
