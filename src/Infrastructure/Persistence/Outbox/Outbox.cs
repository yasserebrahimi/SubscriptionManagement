using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SubscriptionManagement.Infrastructure.Persistence
{
    public class OutboxMessage
    {
        [Key] public Guid Id { get; set; } = Guid.NewGuid();
        public string Type { get; set; } = string.Empty;
        public string Payload { get; set; } = string.Empty;
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? ProcessedAtUtc { get; set; }
        public string? Error { get; set; }
        public int RetryCount { get; set; }
    }

    public interface IOutbox
    {
        Task AddAsync(string type, string payload, CancellationToken ct);
        Task<List<OutboxMessage>> TakeBatchAsync(int take, CancellationToken ct);
        Task MarkProcessedAsync(Guid id, CancellationToken ct);
        Task MarkFailedAsync(Guid id, string error, CancellationToken ct);
    }

    public class EfOutbox : IOutbox
    {
        private readonly OperationalDbContext _db;
        public EfOutbox(OperationalDbContext db) { _db = db; }

        public async Task AddAsync(string type, string payload, CancellationToken ct)
        {
            _db.OutboxMessages.Add(new OutboxMessage { Type = type, Payload = payload });
            await _db.SaveChangesAsync(ct);
        }

        public async Task<List<OutboxMessage>> TakeBatchAsync(int take, CancellationToken ct)
        {
            return await _db.OutboxMessages
                .Where(x => x.ProcessedAtUtc == null)
                .OrderBy(x => x.CreatedAtUtc)
                .Take(take)
                .ToListAsync(ct);
        }

        public async Task MarkProcessedAsync(Guid id, CancellationToken ct)
        {
            var m = await _db.OutboxMessages.FindAsync(new object[] { id }, ct);
            if (m is null) return;
            m.ProcessedAtUtc = DateTime.UtcNow;
            await _db.SaveChangesAsync(ct);
        }

        public async Task MarkFailedAsync(Guid id, string error, CancellationToken ct)
        {
            var m = await _db.OutboxMessages.FindAsync(new object[] { id }, ct);
            if (m is null) return;
            m.Error = error;
            m.RetryCount++;
            await _db.SaveChangesAsync(ct);
        }
    }
}
