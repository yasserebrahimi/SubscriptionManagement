
using System.ComponentModel.DataAnnotations;

namespace SubscriptionManagement.Domain
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; protected set; }
        // Removed [Timestamp] RowVersion for PostgreSQL. We'll use xmin concurrency token in DbContext.
    }

    public enum SubscriptionStatus { Pending = 1, Active = 2, Suspended = 3, Cancelled = 4, Expired = 5 }

    public class SubscriptionPlan : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public int DurationInDays { get; private set; }
        public bool IsActive { get; private set; } = true;

        public SubscriptionPlan() { }
        public SubscriptionPlan(string name, string description, decimal price, int duration)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Description required");
            if (price < 0) throw new ArgumentException("Price cannot be negative");
            if (duration <= 0) throw new ArgumentException("Duration must be positive");
            Name = name; Description = description; Price = price; DurationInDays = duration;
        }
        public void Deactivate() => IsActive = false;
        public void Activate() => IsActive = true;
    }

    public class Subscription : BaseEntity
    {
        public Guid UserId { get; private set; }
        public Guid PlanId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public SubscriptionStatus Status { get; private set; } = SubscriptionStatus.Active;
        public SubscriptionPlan? Plan { get; private set; }

        public static Subscription Create(Guid userId, Guid planId, int durationDays)
        {
            if (durationDays <= 0) throw new ArgumentException("Duration must be positive");
            return new Subscription
            {
                UserId = userId,
                PlanId = planId,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(durationDays),
                Status = SubscriptionStatus.Active
            };
        }

        public void Deactivate(string reason = "User requested")
        {
            if (Status != SubscriptionStatus.Active) throw new InvalidOperationException("Only Active can be deactivated");
            Status = SubscriptionStatus.Cancelled;
        }
    }
}
