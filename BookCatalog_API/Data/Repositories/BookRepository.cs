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
        Author? author = await context.Authors.FindAsync(authorId);
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

    public async Task<bool> DeleteBookAsync(int bookId)
    {
        Book? book = await context.Books.FindAsync(bookId);
        if (book == null)
        {
            throw new ArgumentNullException("Book was not found");
        }
        var logs = await context.BookCreateLogs.Where(l => l.BookId == bookId).ToListAsync();
        context.BookCreateLogs.RemoveRange(logs);
        context.Books.Remove(book);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> EditBook(BookEditDto bookDto)
    {
        Book? book = await context.Books.Include(b => b.BookGenres).FirstOrDefaultAsync(b => b.Id == bookDto.Id);
        if (book == null)
        {
            throw new ArgumentNullException("Book was not found");
        }

        book.Description = bookDto.Description;
        book.Title = bookDto.Title;

        var newGenresIds = bookDto.BookGenres.Select(g => g.Id).ToHashSet();
        var currentGenreIds = book.BookGenres.Select(g => g.GenreId).ToHashSet();

        var genresToRemove = book.BookGenres.Where(bg => !newGenresIds.Contains(bg.GenreId)).ToList();
        var genresToAdd = newGenresIds.Except(currentGenreIds)
            .Select(id => new BookGenre { BookId = book.Id, GenreId = id });

        foreach (var bg in genresToRemove)
        {
            book.BookGenres.Remove(bg);
        }

        foreach (var bg in genresToAdd)
        {
            book.BookGenres.Add(bg);
        }

        book.PublicationYear = bookDto.PublicationDate;
        context.Update(book);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<List<BookDto>> GetAllBooksAsync(int userId = -1)
    {
        var mappedList = mapper.Map<List<BookDto>>(await context.Books.ToListAsync());
        if (userId == -1)
        {
            return mappedList;
        }

        var favoriteBookIds = await context.Set<Favourite>()
            .Where(f => f.UserId == userId)
            .Select(f => f.BookId)
            .ToListAsync();

        foreach (var book in mappedList)
        {
            book.IsFavourite = favoriteBookIds.Contains(book.Id);
        }
        return mappedList;
    }

    public async Task<List<BookDto>> GetAllBooksOfAuthorAsync(int authorId, int userId = -1)
    {
        var mappedList = mapper.Map<List<BookDto>>(
            await context.Books
            .Where(a => a.AuthorId == authorId)
            .ToListAsync());
        
        if (userId == -1)
        {
            return mappedList;
        }

        var favouriteBOokIds = await context.Set<Favourite>()
            .Where(f => f.UserId == userId)
            .Select(f => f.BookId)
            .ToListAsync();

        foreach (var book in mappedList)
        {
            book.IsFavourite = favouriteBOokIds.Contains(book.Id);
        }

        return mappedList;
    }

    public async Task<BookDto> GetBookByIdAsync(int bookId, int userId = -1)
    {
        Book? book = await context.Books.Include(b => b.BookGenres).ThenInclude(b => b.Genre)
            .FirstOrDefaultAsync(b => b.Id == bookId);
        if (book == null)
        {
            throw new ArgumentNullException("Book was not found");
        }

        var bookDto = mapper.Map<BookDto>(book);

        if (userId != -1)
        {
            var isFavourite = await context.Set<Favourite>()
                .AnyAsync(f => f.UserId == userId && f.BookId == bookId);
            bookDto.IsFavourite = isFavourite;
        }
        return bookDto;
    }

    public async Task<BookEditDto> GetBookEditDtoByIdAsync(int bookId)
    {
        Book? book = await context.Books.Include(b => b.BookGenres).ThenInclude(bg => bg.Genre).FirstOrDefaultAsync(b => b.Id == bookId);
        if (book == null)
        {
            throw new ArgumentNullException("Book was not found");
        }
        return mapper.Map<BookEditDto>(book);
    }

    public async Task<bool> MakeFavourite( Tuple<int, int> ids) // item1 - user id; item2 - book id
        //returns false -> unfavourite; true -> favourite
    {
        User? user = await context.Users.Include(f => f.Favourites).FirstOrDefaultAsync(u => u.Id == ids.Item1);
        if (user == null) throw new ArgumentNullException(nameof(MakeFavourite), "User was not found");

        Favourite? fav = user.Favourites.FirstOrDefault(f => f.BookId == ids.Item2); 
        if (fav != null)
        {
            user.Favourites.Remove(fav);
            await context.SaveChangesAsync();
            return false;
        }

        Book? book = await context.Books.FindAsync(ids.Item2);
        if (book == null) throw new ArgumentNullException(nameof(MakeFavourite), "Book was not found");

        Favourite favourite = new Favourite
        {
            Book = book,
            BookId = ids.Item2,
            UserId = ids.Item1,
            FavoriteBy = user
        };

        await context.AddAsync(favourite);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<FavouriteDto> GetFavouritesAsync(int userId)
    {
        User? user = await context.Users.Include(a => a.Favourites).ThenInclude(f => f.Book).FirstOrDefaultAsync(a => a.Id == userId);
        if (user == null) throw new ArgumentNullException(nameof(MakeFavourite), "User was not found");

        var bookDtos = user.Favourites
            .Select(f => mapper.Map<BookDto>(f.Book))
            .ToList();

        var favouriteDto = new FavouriteDto
        {
            Books = bookDtos
        };

        return favouriteDto;
    }

    public async Task<List<BookLogsDto>> GetBookLogs()
    {
        var logs = await context.Set<BookCreateLog>().Include(b => b.Author).ToListAsync();
        if (logs != null)
        {
            return mapper.Map<List<BookLogsDto>>(logs);
        }
        return new List<BookLogsDto>();
    }
}
