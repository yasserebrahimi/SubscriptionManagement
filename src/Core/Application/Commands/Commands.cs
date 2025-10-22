
using MediatR;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using SubscriptionManagement.Application.Common.Models;
using SubscriptionManagement.Domain;
using SubscriptionManagement.Infrastructure.Persistence;

namespace SubscriptionManagement.Application.Commands
{
    public record CreateActivateCommand(Guid UserId, Guid PlanId) : IRequest<Result<SubscriptionDto>>;
    public record DeactivateCommand(Guid SubscriptionId, string? Reason) : IRequest<Result<bool>>;

    public record SubscriptionDto(Guid SubscriptionId, Guid UserId, Guid PlanId, string Status, DateTime StartDate, DateTime? EndDate);
    public record SubscriptionPlanDto(Guid Id, string Name, string Description, decimal Price, int DurationInDays);

    public class CreateActivateCommandValidator : AbstractValidator<CreateActivateCommand>
    {
        public CreateActivateCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.PlanId).NotEmpty();
        }
    }

    public class CreateActivateHandler : IRequestHandler<CreateActivateCommand, Result<SubscriptionDto>>
    {
        private readonly ApplicationDbContext _db;
        public CreateActivateHandler(ApplicationDbContext db) => _db = db;

        public async Task<Result<SubscriptionDto>> Handle(CreateActivateCommand request, CancellationToken ct)
        {
            await using var tx = await _db.Database.BeginTransactionAsync(ct);
            try
            {
                var plan = await _db.SubscriptionPlans.FirstOrDefaultAsync(p => p.Id == request.PlanId && p.IsActive, ct);
                if (plan is null) return Result<SubscriptionDto>.Failure("NotFound");

                var hasActive = await _db.Subscriptions.AnyAsync(s => s.UserId == request.UserId && s.PlanId == request.PlanId && s.Status == SubscriptionStatus.Active, ct);
                if (hasActive) return Result<SubscriptionDto>.Failure("Conflict");

                var sub = Subscription.Create(request.UserId, request.PlanId, plan.DurationInDays);
                _db.Subscriptions.Add(sub);
                await _db.SaveChangesAsync(ct);
                await tx.CommitAsync(ct);

                return Result<SubscriptionDto>.Success(new SubscriptionDto(sub.Id, sub.UserId, sub.PlanId, sub.Status.ToString(), sub.StartDate, sub.EndDate));
            }
            catch (DbUpdateException)
            {
                await tx.RollbackAsync(ct);
                return Result<SubscriptionDto>.Failure("Conflict");
            }
        }
    }

    public class DeactivateHandler : IRequestHandler<DeactivateCommand, Result<bool>>
    {
        private readonly ApplicationDbContext _db;
        public DeactivateHandler(ApplicationDbContext db) => _db = db;

        public async Task<Result<bool>> Handle(DeactivateCommand request, CancellationToken ct)
        {
            var sub = await _db.Subscriptions.FirstOrDefaultAsync(s => s.Id == request.SubscriptionId, ct);
            if (sub is null) return Result<bool>.Failure("NotFound");
            sub.Deactivate(request.Reason ?? "User requested");
            await _db.SaveChangesAsync(ct);
            return Result<bool>.Success(true);
        }
    }
}
