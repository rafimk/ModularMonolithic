namespace Authorization.Contracts;

public interface IUserPermissionsResponse
{
    HashSet<string> Permissions { get; }
}