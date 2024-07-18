using Book.Domain.Models.Abstract;

namespace Book.Domain;

public static class ModelExtensions
{
    public static T AsCreated<T>(this T entity) where T : BaseModel
    {
        var createdDate = DateTimeOffset.UtcNow;

        entity.CreatedDate = createdDate;
        entity.ModifiedDate = createdDate;
        entity.IsDeleted = false;

        return entity;
    }

    public static T AsModified<T>(this T entity) where T : BaseModel
    {
        var modifiedDate = DateTimeOffset.UtcNow;

        entity.ModifiedDate = modifiedDate;

        return entity;
    }

    public static T AsDeleted<T>(this T entity) where T : BaseModel
    {
        var deletedDate = DateTimeOffset.UtcNow;

        entity.ModifiedDate = deletedDate;
        entity.IsDeleted = true;

        return entity;
    }
}