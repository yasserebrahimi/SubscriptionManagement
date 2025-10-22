# Operations

## Endpoints
- **/health** – liveness
- **/metrics** – Prometheus scraping (OTEL exporter)
- **/swagger** – OpenAPI

## Metrics of interest (examples)
- `http_server_request_duration_seconds_bucket/count/sum`
- `process_*`, `runtime_*`
- Custom business metrics can be added later (e.g., active subscriptions)

## SLI/SLO Examples
- **Availability:** 99.9%
- **Latency:** p95 < 300ms; p99 < 800ms
- **Error rate:** < 0.1% over 30d (error budget)

## Alerts (suggested)
- p95 latency > 300ms (5m)
- 5xx rate > 1% (5m)
- Rate-limit rejections surge

## Runbooks
1. Inspect `/health` and recent deploys.
2. Jump to Prometheus/Grafana; filter by `X-Correlation-ID` if available.
3. Verify Postgres/RabbitMQ status.
