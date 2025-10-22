# How-To: Release

- Cut a tag (e.g., `v1.0.0`).
- GitHub Action builds & pushes image to GHCR.
- Helm values → set image tag to the new release.
- Release drafter composes notes automatically.
