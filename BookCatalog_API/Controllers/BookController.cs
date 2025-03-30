using BookCatalog_API.DTOs;
using BookCatalog_API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog_API.Controllers;

public class BookController(IBookRepository repository) : BaseApiController
{
    [HttpGet("books")]
    public async Task<ActionResult> GetAllBooks()
    {
        var books = await repository.GetAllBooksAsync();
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
}
