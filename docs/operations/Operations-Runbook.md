# Operations Runbook

## First run
```bash
dotnet restore && dotnet build && dotnet test
dotnet run --project src/Presentation/WebAPI/SubscriptionManagement.WebAPI.csproj
```

## Database
- Connection string: `ConnectionStrings:Default`
- Startup performs **migrate + seed** (3 plans)
- For docker: `docker compose up --build` (Postgres + healthcheck)

## Health
- GET `/health` → 200 OK
- Prometheus scraping endpoint exposed (metrics compose file)

## Logging
- Serilog console. Enrichers add machine/thread and request context.

## Troubleshooting
- 409 on activate → active exists per rule; check key uniqueness
- 422 → validation; check request body
- Idempotency replay → same response until TTL expires

— End —
