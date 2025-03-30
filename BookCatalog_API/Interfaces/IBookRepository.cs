using BookCatalog_API.DTOs;

namespace BookCatalog_API.Interfaces;

public interface IBookRepository
{
    Task<bool> AddBookAsync(int authorId, BookCreateDto bookDto);
    Task<bool> DeleteBookAsync(BookDto bookDto);
    Task<BookDto> GetBookByIdAsync(int bookId);
    Task<List<BookDto>> GetAllBooksOfAuthorAsync(int authorId);
    Task<List<BookDto>> GetAllBooksAsync();
    Task<bool> EditBook(BookDto bookDto);
}
