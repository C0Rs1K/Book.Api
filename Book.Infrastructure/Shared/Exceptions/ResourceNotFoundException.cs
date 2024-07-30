namespace Book.Infrastructure.Shared.Exceptions;

public class ResourceNotFoundException : Exception
{
    public ResourceNotFoundException()
    {
    }

    public ResourceNotFoundException(string resourceName, Exception? inner = null)
        : base("Resource not found: " + resourceName, inner)
    {
    }

    public static void ThrowIfNull(object? item)
    {
        if (item == null)
        {
            throw new ResourceNotFoundException("Item not found: " + item);
        }
    }
}