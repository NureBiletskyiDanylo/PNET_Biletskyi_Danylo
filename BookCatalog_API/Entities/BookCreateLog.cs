namespace BookCatalog_API.Entities;

public class BookCreateLog
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public required Book Book { get; set; }
    public required string Title { get; set; }
    public int AuthorId { get; set; }
    public required Author Author { get; set; }
    public DateTime InsertedAt { get; set; }
}
