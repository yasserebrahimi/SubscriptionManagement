using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace SubscriptionManagement.WebAPI.Middleware
{
    /// <summary>
    /// Adds a minimal but solid set of security headers.
    /// CSP enabled only in Production to avoid breaking dev tools.
    /// </summary>
    public sealed class SecurityHeadersMiddleware : IMiddleware
    {
        private readonly IHostEnvironment _env;
        public SecurityHeadersMiddleware(IHostEnvironment env) => _env = env;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var headers = context.Response.Headers;

            // Clickjacking / MIME sniffing / Referrer / XSS
            headers["X-Frame-Options"] = "DENY";
            headers["X-Content-Type-Options"] = "nosniff";
            headers["Referrer-Policy"] = "no-referrer";
            headers["X-XSS-Protection"] = "0";

            // HSTS (only when served over HTTPS)
            if (string.Equals(context.Request.Scheme, "https", StringComparison.OrdinalIgnoreCase))
            {
                headers["Strict-Transport-Security"] = "max-age=31536000; includeSubDomains; preload";
            }

            // Content Security Policy (Production only)
            if (_env.IsProduction())
            {
                // Swagger UI uses inline styles; allow 'unsafe-inline' for style only.
                // If you host any external assets, add them to the directives accordingly.
                var csp = string.Join("; ",
                    "default-src 'none'",
                    "base-uri 'self'",
                    "frame-ancestors 'none'",
                    "img-src 'self' data:",
                    "script-src 'self'",
                    "style-src 'self' 'unsafe-inline'",
                    "connect-src 'self'"
                );
                headers["Content-Security-Policy"] = csp;
                headers["X-Content-Security-Policy"] = csp; // legacy
            }

            await next(context);
        }
    }
}
