# Error Examples (RFC7807)

## 400 Validation
```json
{
  "type": "https://httpstatuses.io/400",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "traceId": "|abc123",
  "errors": {
    "planId": ["The planId format is invalid."]
  }
}
```

## 401 Unauthorized
```json
{
  "type": "https://httpstatuses.io/401",
  "title": "Unauthorized",
  "status": 401,
  "detail": "Bearer token is missing or invalid."
}
```

## 409 Conflict
```json
{
  "type": "https://httpstatuses.io/409",
  "title": "Conflict",
  "status": 409,
  "detail": "User already has an Active subscription."
}
```

## 429 Too Many Requests
```json
{
  "type": "https://httpstatuses.io/429",
  "title": "Too Many Requests",
  "status": 429,
  "detail": "Rate limit exceeded. Retry later.",
  "limit": 200,
  "remaining": 0
}
```
