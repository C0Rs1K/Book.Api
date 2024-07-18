using Book.Domain.Models.Abstract;

namespace Book.Domain.Models;

public class GenreModel : BaseModel
{
    public string Name { get; set; }

    public ICollection<BookModel> Books { get; set; }
}