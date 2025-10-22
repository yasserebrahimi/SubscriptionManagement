using Microsoft.EntityFrameworkCore;

namespace SubscriptionManagement.Infrastructure.Persistence
{
    /// <summary>
    /// EF Core context for operational tables (idempotency/outbox).
    /// Uses the same Postgres connection string as your main DB.
    /// </summary>
    public class OperationalDbContext : DbContext
    {
        public OperationalDbContext(DbContextOptions<OperationalDbContext> options) : base(options) { }

        public DbSet<IdempotencyKey> IdempotencyKeys => Set<IdempotencyKey>();
        public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

        protected override void OnModelCreating(ModelBuilder b)
        {
            b.Entity<IdempotencyKey>(cfg =>
            {
                cfg.ToTable("idempotency_keys");
                cfg.HasKey(x => x.Key);
                cfg.Property(x => x.ResponseJson);
                cfg.HasIndex(x => x.ExpiresAtUtc);
            });

            b.Entity<OutboxMessage>(cfg =>
            {
                cfg.ToTable("outbox_messages");
                cfg.HasKey(x => x.Id);
                cfg.Property(x => x.Type).HasMaxLength(255);
                cfg.HasIndex(x => new { x.ProcessedAtUtc, x.CreatedAtUtc });
            });
        }
    }
}
