using BookCatalog_API.DTOs;
using BookCatalog_API.Entities;

namespace BookCatalog_API.Interfaces;

public interface IBookRepository
{
    Task<bool> AddBookAsync(int authorId, BookCreateDto bookDto);
    Task<bool> DeleteBookAsync(int bookId);
    Task<BookDto> GetBookByIdAsync(int bookId);
    Task<List<BookDto>> GetAllBooksOfAuthorAsync(int authorId);
    Task<List<BookDto>> GetAllBooksAsync(int userId = -1);
    Task<bool> EditBook(BookDto bookDto);
    Task<bool> MakeFavourite(Tuple<int, int> ids);
    Task<List<Favourite>> GetFavouritesAsync(int userId);
}
