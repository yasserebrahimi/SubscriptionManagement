
param(
  [string]$BaseUrl = "http://localhost:5000",
  [string]$UserId = "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
  [string]$Key = "test-key-123"
)
Write-Host "Health..." -ForegroundColor Cyan
Invoke-RestMethod "$BaseUrl/health" -Method GET | Out-Host

Write-Host "Plans..." -ForegroundColor Cyan
$plans = Invoke-RestMethod "$BaseUrl/api/v1/plans" -Method GET
$planId = $plans.data[0].id
Write-Host "Using planId: $planId"

Write-Host "Activate..." -ForegroundColor Cyan
$body = @{ userId = $UserId; planId = $planId } | ConvertTo-Json
Invoke-RestMethod "$BaseUrl/api/v1/subscriptions/activate" -Method POST -Body $body -ContentType "application/json" -Headers @{ "Idempotency-Key" = $Key } | Out-Host
