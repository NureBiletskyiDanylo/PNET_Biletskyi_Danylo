using BookCatalog_API.DTOs;
using BookCatalog_API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog_API.Controllers;

public class AuthorController(IAuthorRepository authorRepository, IBookRepository bookRepository) : BaseApiController
{
    #region Author
    [HttpPost("author/create")]
    public async Task<ActionResult> CreateAuthor(AuthorCreateDto dto)
    {
        if (dto != null && !string.IsNullOrEmpty(dto.Name))
        {
            bool result = await authorRepository.AddAuthorAsync(dto);
            return result ? Ok() : BadRequest("Error during storing the author"); 
        }
        return BadRequest("Error during author creation");
    }

    [HttpGet("author/{id:int}")]
    public async Task<ActionResult<AuthorDto>> GetAuthor(int id)
    {
        var author = await authorRepository.GetAuthorByIdAsync(id);
        if (author == null) return BadRequest("Author was not found");
        return Ok(author);
    }

    [HttpGet("author")]
    public async Task<ActionResult<List<AuthorDto>>> GetAuthors()
    {
        return await authorRepository.GetAllAuthorsAsync();
    }

    [HttpDelete("author/{id:int}")]
    public async Task<ActionResult> DeleteAuthor(int id)
    {
        bool result = false;
        try
        {
            result = await authorRepository.DeleteAuthorAsync(id);
        }
        catch (ArgumentNullException exc)
        {
            return BadRequest(exc.Message);
        }
        if (!result)
        {
            return BadRequest("Error: Deletion was not successful");
        }
        return Ok();
    }

    [HttpPut("author/{id:int}")]
    public async Task<ActionResult<AuthorDto>> EditAuthor(AuthorDto author)
    {
        if (author == null) 
            return BadRequest("Error: Received data was corrupted");

        bool result = await authorRepository.EditAuthorAsync(author);
        return result ? Ok() : BadRequest("Error: Updating was not successful"); 
    }
    #endregion

    #region Book
    [HttpPost("author/{authorId:int}/add-book")]
    public async Task<ActionResult> AddBook(int authorId, BookCreateDto bookDto)
    {
        if (bookDto == null)
            return BadRequest("Failed receiving data");

        bool result = await bookRepository.AddBookAsync(authorId, bookDto);
        if (!result) return BadRequest("Error: Adding was not successful");
        return Ok();
    }
    
    #endregion
}
