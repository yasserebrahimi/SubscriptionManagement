# CI/CD

GitHub Actions workflows include (examples):
- `ci.yml` – restore/build/test
- `codecov.yml` – coverage upload to Codecov (needs `CODECOV_TOKEN` secret)
- `codeql.yml` – static analysis
- `dependency-review.yml` – vulnerable dependency detection on PRs
- `docker-publish.yml` – build & push Docker image to GHCR
- `sonar.yml` – SonarCloud analysis
- `trivy.yml` – container vulnerability scan
- `release-drafter.yml` – automated release notes
