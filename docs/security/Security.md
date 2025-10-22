# SECURITY

If you discover a security vulnerability within this repository, please report it responsibly.

- Do **not** file a public issue for sensitive vulnerabilities.
- Email: security@example.com with steps to reproduce and expected impact.
- We will acknowledge within 72 hours and provide a remediation plan.

Best practices followed in code:
- OIDC/JWT validation (issuer/audience)
- ProblemDetails without stack traces in production
- Security headers (no-sniff, XSS, frame options)
- Rate limiting and timeouts/bulkheads for external calls
