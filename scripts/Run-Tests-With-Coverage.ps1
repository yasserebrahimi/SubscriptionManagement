param(
  [switch]$Coverage
)
Write-Host "Running tests..." -ForegroundColor Cyan
if ($Coverage) {
  dotnet test --configuration Release --collect:"XPlat Code Coverage" -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format=opencover
  Write-Host "Coverage reports (OpenCover) generated in TestResults folders." -ForegroundColor Green
} else {
  dotnet test --configuration Release
}
