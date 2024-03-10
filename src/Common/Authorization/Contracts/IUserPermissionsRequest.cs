namespace Authorization.Contracts;

public interface IUserPermissionsRequest
{
    string UserIdentityProviderId { get; }
}