# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src
COPY ["SubscriptionManagement.csproj", "./"]
RUN dotnet restore "SubscriptionManagement.csproj" --runtime linux-musl-x64
COPY . .
RUN dotnet build "SubscriptionManagement.csproj" -c Release -o /app/build --no-restore --runtime linux-musl-x64

# Publish Stage
FROM build AS publish
RUN dotnet publish "SubscriptionManagement.csproj" -c Release -o /app/publish --no-restore --runtime linux-musl-x64 --self-contained false /p:PublishTrimmed=false

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
RUN addgroup -g 1000 appgroup && adduser -u 1000 -G appgroup -s /bin/sh -D appuser
WORKDIR /app
RUN apk add --no-cache curl
COPY --from=publish --chown=appuser:appgroup /app/publish .
ENV ASPNETCORE_URLS=http://+:8080 DOTNET_RUNNING_IN_CONTAINER=true DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
USER appuser
EXPOSE 8080
HEALTHCHECK --interval=30s --timeout=10s --start-period=40s --retries=3 CMD curl -f http://localhost:8080/health || exit 1
ENTRYPOINT ["dotnet", "SubscriptionManagement.dll"]
