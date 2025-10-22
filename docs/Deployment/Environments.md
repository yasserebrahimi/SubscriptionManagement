# Environments

- **Dev**: permissive CORS; OIDC optional; lower rate limits.
- **Stage**: mirrors prod configuration; synthetic tests.
- **Prod**: locked-down CORS; policies active; secrets via vault; strict SLO alerts.
