namespace BookCatalog_API.Entities;

public class BookGenre
{
    public Book Book { get; set; } = null!;
    public int BookId { get; set; }
    public Genre Genre { get; set; } = null!;
    public int GenreId { get; set; }
}
