# Migrations

- Use EF Core migrations for schema changes.
- Ensure **partial unique indexes** for Active invariants (Option A/B).
- Apply migrations before deploying a new version (CI/CD step).

Example:
```bash
dotnet ef migrations add AddUniqueActive --project src/Infrastructure/Infrastructure.csproj
dotnet ef database update
```
