
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace SubscriptionManagement.Application.Behaviors
{
    public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<PerformanceBehavior<TRequest, TResponse>> _logger;
        public PerformanceBehavior(ILogger<PerformanceBehavior<TRequest, TResponse>> logger) => _logger = logger;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var sw = Stopwatch.StartNew();
            var response = await next();
            sw.Stop();
            _logger.LogInformation("Request {RequestName} took {Elapsed} ms", typeof(TRequest).Name, sw.ElapsedMilliseconds);
            return response;
        }
    }
}
