# Dashboards (PromQL)

- **RPS**:
```
sum(rate(http_server_request_duration_seconds_count[5m]))
```

- **Latency p95**:
```
histogram_quantile(0.95, sum(rate(http_server_request_duration_seconds_bucket[5m])) by (le))
```

- **Error rate**:
```
sum(rate(http_server_request_duration_seconds_count{status=~"5.."}[5m]))
/
sum(rate(http_server_request_duration_seconds_count[5m]))
```

- **429 rate**:
```
sum(rate(http_server_request_duration_seconds_count{status="429"}[5m]))
```
