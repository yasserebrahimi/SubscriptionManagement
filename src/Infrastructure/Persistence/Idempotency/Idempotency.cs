using System.ComponentModel.DataAnnotations;

namespace SubscriptionManagement.Infrastructure.Persistence
{
    public class IdempotencyKey
    {
        [Key] public string Key { get; set; } = default!;
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAtUtc { get; set; } = DateTime.UtcNow.AddHours(24);
        public string? ResponseJson { get; set; }
    }

    public interface IIdempotencyStore
    {
        Task<(bool exists, string? response)> TryGetAsync(string key, CancellationToken ct);
        Task PutAsync(string key, string responseJson, TimeSpan ttl, CancellationToken ct);
    }

    public class EfIdempotencyStore : IIdempotencyStore
    {
        private readonly OperationalDbContext _db;
        public EfIdempotencyStore(OperationalDbContext db) { _db = db; }

        public async Task<(bool, string?)> TryGetAsync(string key, CancellationToken ct)
        {
            var e = await _db.IdempotencyKeys.FindAsync(new object[] { key }, ct);
            if (e is null) return (false, null);
            if (e.ExpiresAtUtc < DateTime.UtcNow)
            {
                _db.IdempotencyKeys.Remove(e);
                await _db.SaveChangesAsync(ct);
                return (false, null);
            }
            return (true, e.ResponseJson);
        }

        public async Task PutAsync(string key, string responseJson, TimeSpan ttl, CancellationToken ct)
        {
            var e = await _db.IdempotencyKeys.FindAsync(new object[] { key }, ct);
            if (e is null)
            {
                e = new IdempotencyKey { Key = key };
                _db.IdempotencyKeys.Add(e);
            }
            e.ResponseJson = responseJson;
            e.ExpiresAtUtc = DateTime.UtcNow.Add(ttl);
            await _db.SaveChangesAsync(ct);
        }
    }
}
