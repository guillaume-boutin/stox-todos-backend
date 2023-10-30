namespace Application.Errors;

public class StatusError : Exception
{
    public int StatusCode;

    public StatusError(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}
