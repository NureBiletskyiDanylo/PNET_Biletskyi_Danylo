using BookCatalog_API.Entities;
using System.Text.Json.Serialization;

namespace BookCatalog_API.DTOs;

public class BookCreateDto
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public DateOnly PublicationYear { get; set; }
    public ICollection<GenreDto> BookGenres { get; set; } = [];
}
