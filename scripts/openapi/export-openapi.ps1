param(
  [string]$Project = "src/Presentation/WebAPI/SubscriptionManagement.WebAPI.csproj",
  [string]$Url = "http://0.0.0.0:5080"
)
$ErrorActionPreference = "Stop"
Write-Host "Building..."
dotnet build -c Release | Out-Null
Write-Host "Starting API..."
$proc = Start-Process dotnet -ArgumentList "run --project $Project --urls $Url" -PassThru
Start-Sleep -Seconds 8
try {
  New-Item -ItemType Directory -Force -Path openapi | Out-Null
  Invoke-WebRequest "$Url/swagger/v1/swagger.json" -OutFile "openapi/current-v1.json"
  Write-Host "OpenAPI exported to openapi/current-v1.json"
} finally {
  if ($proc) { Stop-Process -Id $proc.Id -Force }
}
