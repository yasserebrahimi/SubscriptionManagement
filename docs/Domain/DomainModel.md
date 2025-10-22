# Domain Model



```mermaid
erDiagram
  USER ||--o{ SUBSCRIPTION : has
  PLAN ||--o{ SUBSCRIPTION : selected
  SUBSCRIPTION {
    uuid id
    uuid user_id
    uuid plan_id
    string status
    timestamp created_at_utc
    timestamp deactivated_at_utc
  }
  PLAN {
    uuid id
    string name
    numeric price
  }
  USER {
    uuid id
    string email
  }
```



## Invariants
- **Option A (chosen)**: *User may have at most one Active subscription at a time.*
- DB Enforcement:
```sql
CREATE UNIQUE INDEX IF NOT EXISTS ux_subscriptions_user_active
ON subscriptions (user_id)
WHERE status = 'Active';
```

