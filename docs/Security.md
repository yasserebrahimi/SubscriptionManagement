# Security, Policies & Idempotency

## Authentication & Authorization
- JWT/OIDC (Keycloak/Duende). Configure `OIDC__Authority` and `OIDC__Audience` for real environments.
- Policy example: `SubscriptionsWrite` → claim `scope=subscriptions:write`.

## CORS & Security Headers
- CORS is permissive in dev; restrict in prod (origins, headers).
- Middleware sets standard headers: X-Frame-Options, X-Content-Type-Options, X-XSS-Protection, etc.

## Rate Limiting
- Sliding-window per IP, default 200 req/min; expose `X-RateLimit-*` headers; 429 on exceed.

## Correlation ID
- `X-Correlation-ID`: generated if missing, included in responses, added to logging scope.

## Idempotency
- Header: `Idempotency-Key` (ASCII <= 128).
- TTL: default 24h (configurable).
- Behavior: duplicates with same key return the **same response** (status + body). Payload mismatch under same key should return 409.
- Store: Postgres table `idempotency_keys` with expiry index.
