
param(
  [string]$InfraProj = "src/Infrastructure/SubscriptionManagement.Infrastructure.csproj",
  [string]$StartupProj = "src/Presentation/WebAPI/SubscriptionManagement.WebAPI.csproj",
  [string]$Name = "Initial"
)
$ErrorActionPreference = "Stop"
# Install dotnet-ef if missing
if (-not (Get-Command dotnet-ef -ErrorAction SilentlyContinue)) {
  dotnet tool install --global dotnet-ef | Out-Null
}
$env:PATH += ";" + "$env:USERPROFILE\.dotnet\tools"

# Check if any migration exists
$hasMig = Test-Path "src/Infrastructure/Migrations"
if (-not $hasMig) {
  Write-Host "Adding EF migration '$Name'..." -ForegroundColor Cyan
  dotnet ef migrations add $Name -p $InfraProj -s $StartupProj
} else {
  Write-Host "Migrations already exist." -ForegroundColor Yellow
}
Write-Host "Updating database..." -ForegroundColor Cyan
dotnet ef database update -p $InfraProj -s $StartupProj
