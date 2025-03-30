using BookCatalog_API.DTOs;
using BookCatalog_API.Entities;

namespace BookCatalog_API.Interfaces;

public interface IAuthorRepository
{
    Task<bool> AddAuthorAsync(AuthorCreateDto authorDto);
    Task<bool> EditAuthorAsync(AuthorDto author);
    Task<AuthorDto?> GetAuthorByIdAsync(int id);
    Task<List<AuthorDto>> GetAuthorsByNameAsync(string name);
    Task<List<AuthorDto>> GetAllAuthorsAsync();
    Task<bool> DeleteAuthorAsync(int id);
}
