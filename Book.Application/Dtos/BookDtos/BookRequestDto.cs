namespace Book.Application.Dtos.BookDtos;

public class BookRequestDto
{
    public string ISBN { get; set; }
    public string Name { get; set; }
    public string Genre { get; set; }
    public string? Description { get; set; }
    public int AuthorId { get; set; }
    public DateTime TakeTime { get; set; }
    public DateTime ReturnTime { get; set; }
    public string BookOwner { get; set; }
    public string ImagePath { get; set; }
}