namespace BookCatalog_API.DTOs
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? AuthorInfo { get; set; }
        public ICollection<BookDto> Books { get; set; } = [];
    }
}
