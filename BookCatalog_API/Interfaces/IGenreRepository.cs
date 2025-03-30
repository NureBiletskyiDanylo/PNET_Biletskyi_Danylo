using BookCatalog_API.DTOs;

namespace BookCatalog_API.Interfaces;

public interface IGenreRepository
{
    Task<GenreDto> AddGenreAsync(GenreCreateDto genreDto);
    Task<List<GenreDto>> GetGenresAsync();
    Task<bool> DeleteGenreAsync(int id);
    Task<bool> EditGenreAsync(GenreDto genre);
    Task<GenreDto> GetGenreByIdAsync(int id);
    Task<GenreDto> GetGenreByNameAsync(string name);
}
