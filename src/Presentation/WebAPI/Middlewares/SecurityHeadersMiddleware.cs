using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SubscriptionManagement.WebAPI.Middlewares;

public class SecurityHeadersMiddleware
{
    private readonly RequestDelegate _next;
    public SecurityHeadersMiddleware(RequestDelegate next) => _next = next;

    public Task Invoke(HttpContext ctx)
    {
        var h = ctx.Response.Headers;
        h["X-Content-Type-Options"] = "nosniff";
        h["X-Frame-Options"] = "DENY";
        h["X-XSS-Protection"] = "0";
        h["Referrer-Policy"] = "no-referrer";
        h["Permissions-Policy"] = "geolocation=(), microphone=(), camera=()";
        h["Content-Security-Policy"] = "default-src 'self'; img-src 'self' data:; object-src 'none'; frame-ancestors 'none'; base-uri 'self';";
        h["Cross-Origin-Opener-Policy"] = "same-origin";
        h["Cross-Origin-Embedder-Policy"] = "require-corp";
        return _next(ctx);
    }
}
