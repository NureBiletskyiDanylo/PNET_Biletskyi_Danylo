using BookCatalog_API.DTOs;
using BookCatalog_API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog_API.Controllers;

public class GenreController(IGenreRepository repository) : BaseApiController
{
    [HttpPost("genre/create")]
    public async Task<ActionResult> AddGenre(GenreCreateDto genreDto)
    {
        GenreDto? genre = null;
        try
        {
            genre = await repository.AddGenreAsync(genreDto);
        }
        catch (Exception exc)
        {
            return BadRequest(exc.Message);
        }
        if (genre == null) return BadRequest("Error: Genre was not added");
        return Ok(genre);
    }

    [HttpDelete("genre/{id:int}")]
    public async Task<ActionResult> RemoveGenre(int id)
    {
        bool result = false;
        try
        {
            result = await repository.DeleteGenreAsync(id);
        }
        catch (ArgumentNullException exc)
        {
            return BadRequest(exc.Message);
        }
        if (!result) return BadRequest("Error: Genre was not removed");
        return Ok();
    }

    [HttpPut("genre/edit")]
    public async Task<ActionResult> EditGenre(GenreDto genreDto)
    {
        if (genreDto == null) return BadRequest("Received data was corrupted");

        bool result = false;
        try
        {
            result = await repository.EditGenreAsync(genreDto);
        }
        catch (ArgumentNullException exc)
        {
            return BadRequest(exc.Message);
        }
        if (!result) return BadRequest("Error: Genre was not edited");
        return Ok();
    }

    [HttpGet("genre/{id:int}")]
    public async Task<ActionResult> GetGenreById(int id)
    {
        GenreDto? genre = await repository.GetGenreByIdAsync(id);
        if (genre == null)
            return BadRequest("Genre was not found");
        return Ok(genre);
    }

    [HttpGet("genre/")]
    public async Task<ActionResult> GetGenreByName(string name)
    {
        if (string.IsNullOrEmpty(name))
            return BadRequest("Received data was corrupted");
        GenreDto? genre;
        try
        {
            genre = await repository.GetGenreByNameAsync(name);
        }
        catch (ArgumentNullException exc)
        {
            return BadRequest(exc.Message);
        }

        return Ok(genre);
    }

    [HttpGet("genre/all")]
    public async Task<ActionResult<List<GenreDto>>> GetGenres()
    {
        return Ok(await repository.GetGenresAsync());
    }
}
