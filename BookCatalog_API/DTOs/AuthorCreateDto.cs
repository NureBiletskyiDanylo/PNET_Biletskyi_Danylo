namespace BookCatalog_API.DTOs;

public class AuthorCreateDto
{
    public required string Name { get; set; }
    public string? AuthorInfo { get; set; }
}
