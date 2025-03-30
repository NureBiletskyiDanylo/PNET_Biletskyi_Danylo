namespace BookCatalog_API.Entities;

public class Genre
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<BookGenre> BooksWithGenre { get; set; } = [];
}
