# Serilog Notes

- Enriched with: Service, EnvironmentName, ProcessId, ThreadId, CorrelationId
- Masked fields: Authorization/token/email/password
- Request logging enabled with `UseSerilogRequestLogging`
- Configure sinks via `appsettings*.json` or environment variables
