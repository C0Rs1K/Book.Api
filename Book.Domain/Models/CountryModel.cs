using Book.Domain.Models.Abstract;

namespace Book.Domain.Models;

public class CountryModel : BaseModel
{
    public string Name { get; set; }

    public ICollection<AuthorModel> Authors { get; set; }
}