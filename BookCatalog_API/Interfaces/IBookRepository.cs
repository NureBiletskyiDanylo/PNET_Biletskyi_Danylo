using BookCatalog_API.DTOs;
using BookCatalog_API.Entities;

namespace BookCatalog_API.Interfaces;

public interface IBookRepository
{
    Task<bool> AddBookAsync(int authorId, BookCreateDto bookDto);
    Task<bool> DeleteBookAsync(int bookId);
    Task<BookDto> GetBookByIdAsync(int bookId, int userId = 0);
    Task<List<BookDto>> GetAllBooksOfAuthorAsync(int authorId, int userId = -1);
    Task<List<BookDto>> GetAllBooksAsync(int userId = -1);
    Task<bool> EditBook(BookEditDto bookDto);
    Task<bool> MakeFavourite(Tuple<int, int> ids);
    Task<FavouriteDto> GetFavouritesAsync(int userId);
    Task<BookEditDto> GetBookEditDtoByIdAsync(int bookId);
}
