# Idempotency

- Header: `Idempotency-Key` (ASCII <= 128). Clients must reuse the same key on retries.
- TTL: default 24h (configurable).
- Behavior: duplicate request → **same response** (status/body). Mismatched payload for the same key → **409**.
- Storage: Postgres table `idempotency_keys` with expiry index.
