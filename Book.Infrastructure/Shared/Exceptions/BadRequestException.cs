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

    public static void ThrowIfNotNull(object? item)
    {
        if (item != null)
        {
            throw new BadRequestException("Item already exist" + item);
        }
    }
}