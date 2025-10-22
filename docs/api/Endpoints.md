# API Endpoints (v1)

Base URL: `http://localhost:5080`

| Method | Path                                   | Description                   | Auth/Policy               |
|-------:|----------------------------------------|-------------------------------|---------------------------|
| GET    | /api/v1/plans                          | List plans                    | (optional) `plans:read`   |
| POST   | /api/v1/subscriptions/activate         | Activate a subscription       | `subscriptions:write` + `Idempotency-Key` |
| POST   | /api/v1/subscriptions/deactivate       | Deactivate (by id/user)       | `subscriptions:write`     |
| GET    | /api/v1/subscriptions/{{id}}           | Get subscription by id        | `subscriptions:read`      |

## Errors (RFC7807)
- `400` Validation – `application/problem+json` with `errors`
- `401` Unauthorized – missing/invalid token
- `404` Not Found
- `409` Conflict – active invariant violation
- `429` Too Many Requests – rate limit exceeded
