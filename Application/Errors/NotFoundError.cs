namespace Application.Errors;

public class NotFoundError : Exception
{
    public NotFoundError(string? message = null)
        : base(message ?? "Not Found") { }
}
