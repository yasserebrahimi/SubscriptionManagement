# Microservices Plan

## Services
- **PlanService**: catalog, pricing, plan rules.
- **SubscriptionService**: lifecycle, invariants (Active uniqueness).
- **BillingService**: payments, invoices, reconciliation.
- **NotificationService**: user-facing communications (email/SMS).

## Contracts
- Events (examples):
  - `SubscriptionActivated {{ subscriptionId, userId, planId, atUtc }}`
  - `SubscriptionDeactivated {{ subscriptionId, userId, atUtc }}`
  - `PaymentCaptured {{ invoiceId, userId, amount, atUtc}}`

## Splitting Strategy
1. Extract read models behind interfaces.
2. Move write-side to dedicated service with its own DB.
3. Replace in-proc calls with HTTP/async contracts.
4. Leverage **Outbox** to publish integration events reliably.
