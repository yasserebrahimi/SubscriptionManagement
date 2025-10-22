# Testing Strategy

## Unit Tests
- Domain: entity invariants and state transitions
- Validators: required fields for commands
- Application: handlers (success, not found, conflict)

## Integration Tests
- WebApplicationFactory<Program> for API surface:
  - /health → 200
  - POST /subscriptions/activate without Idempotency-Key → 400
  - (optional) with in-memory DB or Testcontainers PostgreSQL

## Coverage (optional)
Use Coverlet with XPlat to produce OpenCover for Sonar or local inspection.

### Commands
```powershell
dotnet test --configuration Release --collect:"XPlat Code Coverage" -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format=opencover
```
Outputs under `TestResults/` in each test project.

## Performance (basic)
- LoggingBehavior + PerformanceBehavior measure handler timings.
- For load testing, use k6/locust (not included) and monitor Prometheus.

— End —
