namespace SubscriptionManagement.Infrastructure.Persistence
{
    public class OutboxProcessor : BackgroundService
    {
        private readonly IServiceProvider _sp;
        private readonly ILogger<OutboxProcessor> _logger;

        public OutboxProcessor(IServiceProvider sp, ILogger<OutboxProcessor> logger)
        {
            _sp = sp; _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _sp.CreateScope();
                    var outbox = scope.ServiceProvider.GetRequiredService<IOutbox>();
                    var batch = await outbox.TakeBatchAsync(50, stoppingToken);

                    foreach (var msg in batch)
                    {
                        try
                        {
                            // TODO: publish to your message bus here (MassTransit/Kafka) if available
                            _logger.LogInformation("Publishing outbox message type={Type}", msg.Type);
                            await outbox.MarkProcessedAsync(msg.Id, stoppingToken);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Outbox publish failed");
                            await outbox.MarkFailedAsync(msg.Id, ex.Message, stoppingToken);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Outbox loop error");
                }

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }
    }
}
