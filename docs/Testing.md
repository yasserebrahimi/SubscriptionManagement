# Testing

## Unit Tests
`tests/UnitTests/*` – pure unit coverage for domain and application layers.

## Integration Tests
- `tests/IntegrationTests/*` – runtime integration against real DB (configured via test settings).
- `tests/IntegrationTests.Containerized/*` – uses Testcontainers to spin up Postgres in Docker on the fly.

To run:
```bash
dotnet test -c Release
```

## Contract Tests (Pact)
`tests/ContractTests.Pact/*` – consumer contracts; produces Pact files under `pacts/` folder. Integrate with your provider pipeline for verification.

## Coverage
Codecov workflow uploads coverage from CI. Add `CODECOV_TOKEN` in GitHub → Settings → Secrets → Actions.
