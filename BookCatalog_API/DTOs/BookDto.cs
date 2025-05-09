﻿namespace BookCatalog_API.DTOs;

public class BookDto
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public DateOnly PublicationDate { get; set; }
    public int AuthorId { get; set; }
    public string? CoverUrl { get; set; }
    public ICollection<GenreDto> BookGenres { get; set; } = [];
    public bool IsFavourite { get; set; }
}
