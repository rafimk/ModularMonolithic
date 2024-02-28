using Application.Time;
using Domain.Primitives;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Interceptors;

public sealed class UpdateAuditableEntitiesInterceptor : SaveChangesInterceptor
{
    private readonly ISystemTime _systemTime;

    public UpdateAuditableEntitiesInterceptor(ISystemTime systemTime) => _systemTime = systemTime;

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        DateTime utcNow = _systemTime.UtcNow;

        foreach (EntityEntry<IAuditable> auditable in GetAuditableEntities(eventData.Context))
        {
            if (auditable.State == EntityState.Added)
            {
                auditable.Property(nameof(IAuditable.CreatedOnUtc)).CurrentValue = utcNow;
            }

            if (auditable.State == EntityState.Modified)
            {
                auditable.Property(nameof(IAuditable.ModifiedOnUtc)).CurrentValue = utcNow;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static IEnumerable<EntityEntry<IAuditable>> GetAuditableEntities(DbContext dbContext) => dbContext.ChangeTracker.Entries<IAuditable>();
}