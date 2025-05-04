using BookCatalog_API.Entities;

namespace BookCatalog_API.DTOs;

public class BookLogsDto
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public required string Title { get; set; }
    public int AuthorId { get; set; }
    public required AuthorDto Author { get; set; }
    public DateTime InsertedAt { get; set; }
}
