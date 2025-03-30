using AutoMapper;
using BookCatalog_API.DTOs;
using BookCatalog_API.Entities;
using BookCatalog_API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog_API.Data.Repositories;

public class BookRepository(DataContext context, IMapper mapper) : IBookRepository
{
    public async Task<bool> AddBookAsync(int authorId, BookCreateDto bookDto)
    {
        Author? author = await context.Authors.FindAsync(mapper);
        if (author == null)
        {
            throw new ArgumentNullException("Author was not found");
        }

        Book book = mapper.Map<Book>(bookDto);
        book.AuthorId = authorId;
        var genres = await context.Genres
            .Where(g => bookDto.BookGenres.Select(a => a.Id).Contains(g.Id))
            .ToListAsync();

        book.BookGenres = genres.Select(g => new BookGenre { GenreId = g.Id }).ToList();

        context.Add(book);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteBookAsync(BookDto bookDto)
    {
        Book? book = await context.Books.FindAsync(bookDto.Id);
        if (book == null)
        {
            throw new ArgumentNullException("Book was not found");
        }
        context.Books.Remove(book);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> EditBook(BookDto bookDto)
    {
        Book? book = await context.Books.FindAsync(bookDto.Id);
        if (book == null)
        {
            throw new ArgumentNullException("Book was not found");
        }

        book.Description = bookDto.Description;
        book.Title = bookDto.Title;
        var genres = await context.Genres
            .Where(g => bookDto.BookGenres.Select(a => a.Id).Contains(g.Id))
            .ToListAsync();

        book.BookGenres = genres.Select(g => new BookGenre { GenreId = g.Id }).ToList();

        context.Update(book);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<List<BookDto>> GetAllBooksAsync()
    {
        List<Book> books = await context.Books.ToListAsync();
        return mapper.Map<List<BookDto>>(books);
    }

    public async Task<List<BookDto>> GetAllBooksOfAuthorAsync(int authorId)
    {
        List<Book> books = await context.Books
            .Where(a => a.AuthorId == authorId)
            .ToListAsync();
        return mapper.Map<List<BookDto>>(books);
    }

    public async Task<BookDto> GetBookByIdAsync(int bookId)
    {
        Book? book = await context.Books.FindAsync(bookId);
        if (book == null)
        {
            throw new ArgumentNullException("Book was not found");
        }
        return mapper.Map<BookDto>(book);
    }
}
