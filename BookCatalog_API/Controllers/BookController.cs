using AutoMapper;
using BookCatalog_API.DTOs;
using BookCatalog_API.Extensions;
using BookCatalog_API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog_API.Controllers;

public class BookController(IBookRepository repository) : BaseApiController
{
    [HttpGet("books")]
    public async Task<ActionResult> GetAllBooks()
    {
        int userId = -1;
        try
        {
            userId = User.GetUserId();

        }
        catch (Exception)
        {
            return Ok(await repository.GetAllBooksAsync()); 
        }
        var books = await repository.GetAllBooksAsync(userId);
        return Ok(books);
    }


    [HttpPost("book/create/{id:int}")]
    public async Task<ActionResult> AddBook(int id, BookCreateDto bookDto)
    {
        if (bookDto == null) return BadRequest("Error: Received data is corrupted");

        bool result = await repository.AddBookAsync(id, bookDto);
        if (!result) return BadRequest("Error: Adding a book was not successful");
        return Ok();

    }

    [HttpGet("books/{id:int}")]
    public async Task<ActionResult> GetAuthorBooks(int id)
    {
        int userId = -1;
        try
        {
            userId = User.GetUserId();

        }
        catch (Exception)
        {
            return Ok(await repository.GetAllBooksOfAuthorAsync(id));
        }
        return Ok(await repository.GetAllBooksOfAuthorAsync(id, userId));
    }

    [HttpDelete("books/{id:int}")]
    public async Task<ActionResult> DeleteBook(int id)
    {
        bool result = false;
        try
        {
            result = await repository.DeleteBookAsync(id);
        }
        catch (Exception exc)
        {
            return BadRequest(exc.Message);
        }

        if (!result)
        {
            return BadRequest("Error: Book was not deleted");
        }
        return Ok();
    }

    [HttpPost("books/favourite/{id:int}")]
    public async Task<ActionResult> DoFavourite(int id)
    {
        int userId = User.GetUserId();
        bool result = await repository.MakeFavourite(new Tuple<int, int>(userId, id));
        return Ok(result);
    }

    [Authorize]
    [HttpGet("favourites")]
    public async Task<ActionResult> GetFavourites()
    {
        int userId = User.GetUserId();
        var favourites = await repository.GetFavouritesAsync(userId);
        // DO with DTO
        return Ok(favourites);
    }

    [HttpPut("books/{id:int}")]
    public async Task<ActionResult> EditBook([FromRoute]int id, [FromBody]BookEditDto book)
    {
        bool success = await repository.EditBook(book);
        if (!success)
        {
            return BadRequest("Book was not edited because of internal error");
        }
        return Ok();
    }

    [HttpGet("books/book/{id:int}")]
    public async Task<ActionResult> GetBookForEditById(int id)
    {
        var book = await repository.GetBookEditDtoByIdAsync(id);

        return Ok(book);
    }

    [HttpGet("books/book/view/{id:int}")]
    public async Task<ActionResult> GetBookForViewById(int id)
    {
        int userId = -1;
        try
        {
            userId = User.GetUserId();

        }
        catch (Exception)
        {
            return Ok(await repository.GetBookByIdAsync(id));
        }
        return Ok(await repository.GetBookByIdAsync(id, userId));
    }
}
