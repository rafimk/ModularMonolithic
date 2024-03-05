using Application.Data;
using Application.Messaging;
using Modules.Training.Domain.Invitations;
using Shared.Results;

namespace Modules.Training.Application.Invitations.GetInvitationById;

internal sealed class GetInvitationByIdQueryHandler : IQueryHandler<GetInvitationByIdQuery, InvitationResponse>
{
    private readonly ISqlQueryExecutor _sqlQueryExecutor;

    public GetInvitationByIdQueryHandler(ISqlQueryExecutor sqlQueryExecutor) => _sqlQueryExecutor = sqlQueryExecutor;

    public async Task<Result<InvitationResponse>> Handle(GetInvitationByIdQuery request, CancellationToken cancellationToken) =>
        await Result.Success(request)
            .Bind(async query => Result.Create(await GetInvitationByIdAsync(query)))
            .MapFailure(() => InvitationErrors.NotFound(new InvitationId(request.InvitationId)));

    private async Task<InvitationResponse?> GetInvitationByIdAsync(GetInvitationByIdQuery query) =>
        await _sqlQueryExecutor.FirstOrDefaultAsync<InvitationResponse>(
            @"SELECT id, email, sender_first_name, sender_last_name, status
              FROM training.invitations
              WHERE id = @InvitationId",
            new { query.InvitationId });
}