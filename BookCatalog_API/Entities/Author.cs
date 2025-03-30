namespace BookCatalog_API.Entities;

public class Author
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? AuthorInfo { get; set; }
    public List<Book> Books { get; set; } = [];
}
