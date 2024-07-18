namespace Book.Domain.Models.Abstract;

public abstract class BaseModel
{
    public int Id { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset? ModifiedDate { get; set; }
    public bool IsDeleted { get; set; } 
}