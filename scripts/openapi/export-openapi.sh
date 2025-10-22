#!/usr/bin/env bash
set -euo pipefail
PROJECT="${1:-src/Presentation/WebAPI/SubscriptionManagement.WebAPI.csproj}"
URL="${2:-http://0.0.0.0:5080}"
dotnet build -c Release >/dev/null
nohup dotnet run --project "$PROJECT" --urls "$URL" &
PID=$!
sleep 8
mkdir -p openapi
curl -s "$URL/swagger/v1/swagger.json" > openapi/current-v1.json
kill -9 $PID || true
echo "Exported to openapi/current-v1.json"
