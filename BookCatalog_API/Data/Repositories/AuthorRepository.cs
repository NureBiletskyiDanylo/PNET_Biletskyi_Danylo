using AutoMapper;
using BookCatalog_API.DTOs;
using BookCatalog_API.Entities;
using BookCatalog_API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog_API.Data.Repositories;

public class AuthorRepository(DataContext context, IMapper mapper) : IAuthorRepository
{
    public async Task<bool> AddAuthorAsync(AuthorCreateDto authorDto)
    {
        context.Authors.Add(mapper.Map<Author>(authorDto));
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAuthorAsync(int id)
    {
        var author = await context.Authors.FindAsync(id);
        if (author == null)
            throw new ArgumentNullException("Author was not found");
        context.Authors.Remove(author);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> EditAuthorAsync(AuthorDto author)
    {
        context.Authors.Update(mapper.Map<Author>(author));
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<AuthorDto?> GetAuthorByIdAsync(int id)
    {
        return mapper.Map<AuthorDto>(await context.Authors.Include(b => b.Books).FirstOrDefaultAsync(a => a.Id == id));
    }

    public async Task<List<AuthorDto>> GetAuthorsByNameAsync(string name)
    {
        return mapper.Map<List<AuthorDto>>(await context.Authors.Where(a => a.Name == name).ToListAsync())
            ?? new List<AuthorDto>();
    }

    public async Task<List<AuthorDto>> GetAllAuthorsAsync()
    {
        return mapper.Map<List<AuthorDto>>(await context.Authors.ToListAsync());
    }
}
