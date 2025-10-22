# Test Plan & Matrix

| Area | Test Type | Tool | Notes |
|------|-----------|------|------|
| Domain rules | Unit | xUnit | Active invariant, entity behaviors |
| Handlers | Unit/Integration | xUnit | Happy path, edge cases |
| Persistence | Integration | Testcontainers | Postgres CRUD & constraints |
| Contracts | Contract | Pact | `GET /plans` consumer contract |
| Performance | Perf | k6 | Smoke on `activate` |
