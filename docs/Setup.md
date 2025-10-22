# Setup & Installation

## Prerequisites
- .NET SDK 8.x
- Docker Desktop (for Postgres + optional RabbitMQ/Prometheus)
- PowerShell 7+

## Local Infra
```bash
docker compose up -d
# Postgres at localhost:5432 (credentials in docker-compose.yml)
# Prometheus at http://localhost:9090 (if included)
```

## Database
Initialize operational tables (idempotency/outbox):
```bash
psql "Host=localhost;Port=5432;Database=subscriptiondb;Username=dev;Password=dev" -f scripts/sql/init_operational.sql
```

## Run
```bash
dotnet build
dotnet run --project src/Presentation/WebAPI/SubscriptionManagement.WebAPI.csproj
```

## Tests
```bash
dotnet test -c Release
```

## Environment Variables
- `ConnectionStrings__Postgres`
- `OIDC__Authority` / `OIDC__Audience` (optional in dev)
- `IDEMPOTENCY__TTL_HOURS` (default 24)
