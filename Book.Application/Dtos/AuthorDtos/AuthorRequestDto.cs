namespace Book.Application.Dtos.AuthorDtos;

public class AuthorRequestDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Country { get; set; }
}