namespace Book.Application.Dtos.BookDtos;

public class BookResponseDto
{
    public int Id { get; set; }
    public string ISBN { get; set; }
    public string Name { get; set; }
    public string Genre { get; set; }
    public string? Description { get; set; }
    public string Author { get; set; }
    public DateTime TakeTime { get; set; }
    public DateTime ReturnTime { get; set; }
    public string BookOwner { get; set; }
}