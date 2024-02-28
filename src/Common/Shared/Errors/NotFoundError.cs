namespace Shared.Errors;

public sealed class NotFoundError : Error
{
    public NotFoundError(string code, string message)
        : base(code, message)
    {
    }
}
