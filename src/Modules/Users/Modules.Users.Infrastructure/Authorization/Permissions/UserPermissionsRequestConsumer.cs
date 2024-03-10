﻿using Application.Data;
using Authorization.Contracts;
using MassTransit;

namespace Modules.Users.Infrastructure.Authorization.Permissions;

internal sealed class UserPermissionsRequestConsumer : IConsumer<IUserPermissionsRequest>
{
    private readonly ISqlQueryExecutor _sqlQueryExecutor;

    public UserPermissionsRequestConsumer(ISqlQueryExecutor sqlQueryExecutor) => _sqlQueryExecutor = sqlQueryExecutor;

    public async Task Consume(ConsumeContext<IUserPermissionsRequest> context)
    {
        const string sql = @"
            SELECT DISTINCT p.name
            FROM users.users u
            JOIN users.user_roles ur ON u.id = ur.user_id
            JOIN users.roles r ON r.id = ur.role_id
            JOIN users.role_permissions rp ON r.id = rp.role_id
            JOIN users.permissions p ON p.id = rp.permission_id
            WHERE u.identity_provider_id = @UserIdentityProviderId";

        IEnumerable<string> permissions = await _sqlQueryExecutor.QueryAsync<string>(sql, context.Message);

        var response = new UserPermissionsResponse
        {
            Permissions = permissions.ToHashSet()
        };

        await context.RespondAsync<IUserPermissionsResponse>(response);
    }

    private sealed class UserPermissionsResponse : IUserPermissionsResponse
    {
        public HashSet<string> Permissions { get; init; } = new();
    }
}