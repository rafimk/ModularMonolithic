using Application.ServiceLifetimes;
using Microsoft.EntityFrameworkCore;
using Modules.Training.Domain.Invitations;
using Shared.Results;

namespace Modules.Training.Persistence.Repositories;

internal sealed class InvitationRepository : IInvitationRepository, IScoped
{
    private readonly TrainingDbContext _dbContext;

    public InvitationRepository(TrainingDbContext dbContext) => _dbContext = dbContext;

    public async Task<Result<Invitation>> GetByIdAsync(InvitationId id, CancellationToken cancellationToken = default) =>
        Result.Create(await _dbContext.Set<Invitation>().FirstOrDefaultAsync(invitation => invitation.Id == id, cancellationToken));

    public async Task<Result> CheckNonePendingForEmailAsync(string email, CancellationToken cancellationToken = default) =>
        Result.Create(
            !await _dbContext.Set<Invitation>()
                .AnyAsync(
                    invitation => invitation.Email == email && invitation.Status == InvitationStatus.Pending,
                    cancellationToken));

    public void Add(Invitation invitation) => _dbContext.Set<Invitation>().Add(invitation);
}