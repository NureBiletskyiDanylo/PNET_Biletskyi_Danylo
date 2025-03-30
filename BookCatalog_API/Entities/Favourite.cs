namespace BookCatalog_API.Entities;

public class Favourite
{
    public User FavoriteBy { get; set; } = null!;
    public int UserId { get; set; }
    public Book Book { get; set; } = null!;
    public int BookId { get; set; }
}
