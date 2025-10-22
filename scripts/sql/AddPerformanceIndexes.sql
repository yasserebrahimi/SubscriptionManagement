-- Performance indexes for common subscription queries
CREATE INDEX IF NOT EXISTS ix_subscriptions_user_status_expiry
  ON subscriptions(user_id, status, created_at_utc);

CREATE INDEX IF NOT EXISTS ix_subscriptions_user_active
  ON subscriptions(user_id)
  WHERE status = 'Active';

CREATE INDEX IF NOT EXISTS ix_subscriptions_active_created
  ON subscriptions(status, created_at_utc)
  WHERE status = 'Active';

CREATE INDEX IF NOT EXISTS ix_subscriptions_plan_status_created
  ON subscriptions(plan_id, status, created_at_utc);
