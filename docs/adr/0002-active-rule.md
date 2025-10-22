# ADR-0002: Active Subscription Rule
Status: Accepted
Context: product wants to prevent duplicates of active subscriptions.
Decision (this repo): One Active per plan.
DB Constraint: partial unique index = (UserId, PlanId, Status='Active').
Alternative: (UserId, Status='Active').
