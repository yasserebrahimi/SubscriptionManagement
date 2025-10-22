
using Microsoft.EntityFrameworkCore;
using SubscriptionManagement.Domain;

namespace SubscriptionManagement.Infrastructure.Persistence
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(ApplicationDbContext db)
        {
            if (!await db.SubscriptionPlans.AnyAsync())
            {
                db.SubscriptionPlans.AddRange(
                    new SubscriptionPlan("Basic", "Basic access", 9.99m, 30),
                    new SubscriptionPlan("Pro", "Pro features", 19.99m, 30),
                    new SubscriptionPlan("Annual", "Annual plan", 199.99m, 365)
                );
                await db.SaveChangesAsync();
            }
        }
    }
}
