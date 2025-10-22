-- Operational tables
create table if not exists idempotency_keys(
  key text primary key,
  created_at_utc timestamp not null,
  expires_at_utc timestamp not null,
  response_json text
);
create index if not exists ix_idem_expiry on idempotency_keys(expires_at_utc);

create table if not exists outbox_messages(
  id uuid primary key,
  type text not null,
  payload text not null,
  created_at_utc timestamp not null,
  processed_at_utc timestamp,
  error text,
  retry_count int not null default 0
);
create index if not exists ix_outbox_processed_created on outbox_messages(processed_at_utc, created_at_utc);
