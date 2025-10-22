
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Http;

namespace SubscriptionManagement.WebAPI.Middleware
{
    public class IdempotencyStore
    {
        private readonly IMemoryCache _cache;
        public IdempotencyStore(IMemoryCache cache) => _cache = cache;
        public bool TryGet(string key, out object? value) => _cache.TryGetValue(key, out value);
        public void Set(string key, object value) => _cache.Set(key, value, TimeSpan.FromMinutes(10));
    }

    public class IdempotencyMiddleware : IMiddleware
    {
        private readonly IdempotencyStore _store;
        public IdempotencyMiddleware(IdempotencyStore store) => _store = store;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var isActivatePost = context.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase)
                                 && context.Request.Path.Value?.Contains("/subscriptions/activate") == true;
            if (!isActivatePost)
            {
                await next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue("Idempotency-Key", out var key) || string.IsNullOrWhiteSpace(key))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new { error = "Missing Idempotency-Key header" });
                return;
            }

            var scopedKey = $"{context.Request.Method}:{context.Request.Path}:{key}";

            if (_store.TryGet(scopedKey!, out var cached) && cached is string cachedBody)
            {
                context.Response.StatusCode = StatusCodes.Status200OK;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(cachedBody);
                return;
            }

            var originalBody = context.Response.Body;
            using var mem = new MemoryStream();
            context.Response.Body = mem;

            await next(context);

            mem.Position = 0;
            using var reader = new StreamReader(mem);
            string body = await reader.ReadToEndAsync();
            _store.Set(scopedKey!, body);

            mem.Position = 0;
            await mem.CopyToAsync(originalBody);
            context.Response.Body = originalBody;
        }
    }
}
