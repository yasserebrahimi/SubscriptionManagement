CREATE UNIQUE INDEX IF NOT EXISTS ux_subscriptions_user_active
ON subscriptions (user_id)
WHERE status = 'Active';

CREATE INDEX IF NOT EXISTS ix_subscriptions_user_status_created
ON subscriptions (user_id, status, created_at_utc);
