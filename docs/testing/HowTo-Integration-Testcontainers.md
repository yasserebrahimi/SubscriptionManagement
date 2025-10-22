# How-To: Testcontainers Integration

1. Install `DotNet.Testcontainers` and `Npgsql`.
2. Start a Postgres container in test fixture; get mapped port.
3. Run migrations/seed; execute integration tests against it.
