# Glossary

- **Idempotency**: same request + same key ⇒ same response (safe retry).
- **Outbox**: persist events then publish asynchronously; prevents lost messages.
- **CQRS**: separate read/write models for clarity and scaling.
- **SLO**: target for a user-facing metric (e.g., latency p95).
