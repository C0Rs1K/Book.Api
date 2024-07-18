using Book.Domain.Models.Abstract;
using Microsoft.AspNetCore.Identity;

namespace Book.Domain.Models;

public class BookModel : BaseModel
{
    public string ISBN { get; set; }
    public string Name { get; set; }
    public GenreModel Genre { get; set; }
    public int GenreId { get; set; }
    public string? Description { get; set; }
    public AuthorModel Author { get; set; }
    public int AuthorId { get; set; }
    public DateTime TakeTime { get; set; }
    public DateTime ReturnTime { get; set; }
    public IdentityUser<Guid>? BookOwner { get; set; }
    public Guid BookOwnerId { get; set; }
    public string ImagePath { get; set; }
}