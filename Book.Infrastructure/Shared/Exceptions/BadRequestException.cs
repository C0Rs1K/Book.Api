namespace Book.Infrastructure.Shared.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException()
    {
    }

    public BadRequestException(string exceptionString, Exception? inner = null)
        : base("Bad request: " + exceptionString, inner)
    {
    }
}