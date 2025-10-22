# DDD & Bounded Contexts

Contexts:
- **Plan**: plan definitions, pricing
- **Subscription**: activation, deactivation, active invariant
- **Billing**: charges, invoices, refunds
- **Notification**: user comms

Context maps:
- Subscription consumes Plan (query/read)
- Billing listens to Subscription events (outbox → bus)
