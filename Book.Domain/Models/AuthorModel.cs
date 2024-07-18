using Book.Domain.Models.Abstract;
using System.Diagnostics.Metrics;
using System.Security.AccessControl;

namespace Book.Domain.Models;

public class AuthorModel : BaseModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public CountryModel Country { get; set; }
    public int CountryId { get; set; }

    public ICollection<BookModel> Books { get; set; }
}