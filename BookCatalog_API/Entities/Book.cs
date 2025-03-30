namespace BookCatalog_API.Entities;

public class Book
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public DateOnly PublicationYear { get; set; }
    public int AuthorId { get; set; }
    public Author Author { get; set; } = null!;
    public string? CoverUrl { get; set; }
    public ICollection<BookGenre> BookGenres { get; set; } = [];
    public List<Favourite> BeingFavourite { get; set; } = [];
}
