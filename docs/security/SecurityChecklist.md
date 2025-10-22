# Security Checklist

- [x] No secrets in repo; use env vars or secret store
- [x] Dependency scanning: Dependabot + Dependency Review on PRs
- [x] Static analysis: CodeQL (GitHub Actions)
- [x] Input validation: FluentValidation + 422
- [x] Error handling: ProblemDetails; no stack traces leaked
- [x] Logging: structured; avoid PII
- [x] Transport: HTTPS recommended in hosting (TLS terminator/load balancer)
- [x] DB principle of least privilege; limit service account
- [x] Idempotency: scoped keys, TTL
- [x] Rate limiting/throttling (future): gateway or ASP.NET rate limiter

— End —
