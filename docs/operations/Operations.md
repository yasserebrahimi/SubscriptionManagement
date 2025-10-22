# Runbooks

## Elevated latency
1. Check `/metrics` for p95/p99; find hot endpoints.
2. Verify DB health and slow queries.
3. Inspect rate limiting/bulkhead rejections.
4. Roll back recent deployments if necessary.

## High 5xx
1. Tail logs using correlation IDs.
2. Check dependency timeouts (Polly) and circuit breakers.
3. Validate config/secrets and DB connectivity.
