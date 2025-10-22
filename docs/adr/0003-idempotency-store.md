# ADR-0003: Idempotency Store
Status: Accepted
Context: clients may retry POST /activate; we need safe, idempotent execution.
Decision: Middleware with `Idempotency-Key` and 10-minute TTL. Demo uses MemoryCache; production should use DB/Redis with TTL and scope (path+method).
Consequences: better UX under retries; small memory overhead; key collision prevented by namespace scheme.
