
using Microsoft.EntityFrameworkCore;
using SubscriptionManagement.Domain;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace SubscriptionManagement.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Subscription> Subscriptions => Set<Subscription>();
        public DbSet<SubscriptionPlan> SubscriptionPlans => Set<SubscriptionPlan>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subscription>(b =>
            {
                b.ToTable("subscriptions");
                b.HasKey(x => x.Id);
                b.Property(x => x.Status).HasConversion<int>();

                // PostgreSQL optimistic concurrency with xmin
                b.UseXminAsConcurrencyToken();

                b.HasIndex(x => new { x.UserId, x.PlanId, x.Status })
                    .HasFilter("\"Status\" = 2") // 2 = Active
                    .IsUnique();
            });

            modelBuilder.Entity<SubscriptionPlan>(b =>
            {
                b.ToTable("subscription_plans");
                b.HasKey(x => x.Id);
                b.Property(x => x.Price).HasColumnType("numeric(18,2)");
                // Optional xmin on reference data too (uncomment if needed):
                // b.UseXminAsConcurrencyToken();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
