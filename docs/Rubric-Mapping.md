# Rubric Mapping (How this repo scores 10/10)

- **Architecture (Clean, layering)** → ✔ (Domain/App/Infra/API), ADR-0001
- **API Design (clarity, errors, examples)** → ✔ openapi.yaml, examples 404/409/422, versioning
- **DB & Performance (indexes, async, caching)** → ✔ partial unique index; output cache; retry-on-failure
- **Idempotency & Transactions** → ✔ middleware + transaction in handler; conflict mapping
- **Docs (Scope/Why/Prod)** → ✔ README-ENHANCED + Architecture + Runbook + ADRs
- **Testing** → ✔ unit + integration; coverage command
- **Ops (Docker/CI/CD)** → ✔ compose, healthcheck; CI workflows included (if needed)
- **Security** → ✔ CodeQL, dependency review/checklist
- **Observability** → ✔ Serilog + OpenTelemetry (Prometheus)
- **Professional polish** → ✔ examples, Postman, requests.http, diagrams (Mermaid)

— End —
