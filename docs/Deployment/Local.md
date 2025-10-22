# Deployment – Local

```bash
docker compose up -d
psql "Host=localhost;Port=5432;Database=subscriptiondb;Username=dev;Password=dev" -f scripts/sql/init_operational.sql
dotnet run --project src/Presentation/WebAPI/SubscriptionManagement.WebAPI.csproj
```
