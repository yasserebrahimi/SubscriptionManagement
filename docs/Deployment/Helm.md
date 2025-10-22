# Deployment – Helm (optional)

- Chart path (example): `deploy/helm/subscription-webapi/`
- Set image repository to `ghcr.io/OWNER/REPO/webapi` and tag to a release.

```bash
helm upgrade --install subscription-webapi deploy/helm/subscription-webapi -n subscription --create-namespace   --set image.repository=ghcr.io/OWNER/REPO/webapi   --set image.tag=v1.0.0
```
