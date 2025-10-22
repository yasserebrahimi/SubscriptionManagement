# API Guide (v1)

All endpoints return RFC7807 `application/problem+json` on errors.

## Plans
`GET /api/v1/plans` → 200 OK
```json
[ { "id": "basic", "name": "Basic", "price": 9.99 } ]
```

## Activate Subscription
`POST /api/v1/subscriptions/activate`
- **Headers:** `Idempotency-Key: <token>` (required), `Authorization: Bearer ...` (if policies enabled)
- **Body:**
```json
{ "userId": "00000000-0000-0000-0000-000000000001", "planId": "basic" }
```
- **Responses:**
  - `200 OK` – activated
  - `409 Conflict` – already has Active (Option A)
  - `429 Too Many Requests` – rate limited

## Deactivate Subscription
`POST /api/v1/subscriptions/deactivate`
- Body: `{ "userId": "...", "subscriptionId": "..." }`
- Returns: 200 OK

## Get Subscription
`GET /api/v1/subscriptions/{id}` → 200 OK / 404 NotFound
