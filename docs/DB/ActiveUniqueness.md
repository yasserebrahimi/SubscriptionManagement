# Database – Active Uniqueness

**Option A (chosen):** one Active per user.

```sql
CREATE UNIQUE INDEX IF NOT EXISTS ux_subscriptions_user_active
ON subscriptions (user_id)
WHERE status = 'Active';
```
**Option B (alternative):** one Active per (user, plan).

```sql
CREATE UNIQUE INDEX IF NOT EXISTS ux_subscriptions_user_plan_active
ON subscriptions (user_id, plan_id)
WHERE status = 'Active';
```
