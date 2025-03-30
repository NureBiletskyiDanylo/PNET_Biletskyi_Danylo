using AutoMapper;
using BookCatalog_API.DTOs;
using BookCatalog_API.Entities;
using BookCatalog_API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog_API.Data.Repositories;

public class GenreRepository(DataContext context, IMapper mapper) 
    : IGenreRepository
{
    public async Task<GenreDto> AddGenreAsync(GenreCreateDto genreDto)
    {
        genreDto.Name = genreDto.Name.ToLower();
        if (await context.Genres.FirstOrDefaultAsync(a => a.Name == genreDto.Name) != null)
        {
            throw new ArgumentException("Genre with this name already exists");
        }
        var genre = mapper.Map<Genre>(genreDto);
        context.Genres.Add(genre);
        bool result = await context.SaveChangesAsync() > 0;
        if (result)
        {
            return mapper.Map<GenreDto>(genre);
        }
        throw new ArgumentNullException("Adding genre was not successful");
    }

    public async Task<bool> DeleteGenreAsync(int id)
    {
        Genre? genre = await context.Genres.FindAsync(id);
        if (genre == null) throw new ArgumentNullException("Genre was not found");
        context.Remove(genre);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> EditGenreAsync(GenreDto genre)
    {
        Genre? oldGenre = await context.Genres.FindAsync(genre.Id);
        if (oldGenre == null) throw new ArgumentNullException("Genre was not found");
        oldGenre.Name = genre.Name.ToLower();
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<GenreDto> GetGenreByIdAsync(int id)
    {
        return mapper.Map<GenreDto>(await context.Genres.FindAsync(id));
    }

    public async Task<GenreDto> GetGenreByNameAsync(string name)
    {
        Genre? genre = await context.Genres
            .FirstOrDefaultAsync(g =>
            string.Equals(name.ToLower(), g.Name));
        if (genre == null) throw new ArgumentNullException("Genre was not found");
        return mapper.Map<GenreDto>(genre);
    }

    public async Task<List<GenreDto>> GetGenresAsync()
    {
        return mapper.Map<List<GenreDto>>(await context.Genres.ToListAsync());
    }
}
