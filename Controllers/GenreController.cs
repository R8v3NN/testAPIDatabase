namespace testAPIDatabase.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using testAPIDatabase.Context;
using testAPIDatabase.Dtos;
using testAPIDatabase.Entities;

[ApiController]
[Route("api/[controller]")]
public class GenreController : ControllerBase
{
    private readonly AppDbContext dbContext;
    public GenreController(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult GetAllGenres()
    {
        var allGenres = dbContext.Genres.ToList();
        return Ok(allGenres);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public IActionResult GetGenreById(Guid id)
    {
        var genre = dbContext.Genres.Find(id);
        if (genre is null)
        {
            return NotFound();
        }
        return Ok(genre);
    }

    [HttpPost]
    public IActionResult AddGenre(GenreInDto addGenreDto)
    {
        if (string.IsNullOrWhiteSpace(addGenreDto?.Name))
        {
            return BadRequest("Genre name cannot be empty.");
        }

        var existingGenre = dbContext.Genres.FirstOrDefault(g => g.Name == addGenreDto.Name);

        if (existingGenre is not null)
        {
            return Conflict("Genre already exists.");
        }

        var genreEntity = new Genre { Name = addGenreDto.Name };

        dbContext.Genres.Add(genreEntity);
        dbContext.SaveChanges();

        return Ok(genreEntity);
    }

    [HttpPut]
    public IActionResult UpdateGenre(Guid id, GenreInDto updateGenreInDto)
    {
        if (string.IsNullOrWhiteSpace(updateGenreInDto?.Name))
        {
            return BadRequest("Genre name cannot be empty.");
        }

        var genre = dbContext.Genres.Find(id);
        if (genre is null)
        {
            return NotFound();
        }

        var existingGenre = dbContext.Genres.FirstOrDefault(g => g.Name == updateGenreInDto.Name);
        if (existingGenre is not null)
        {
            return Conflict("Genre already exists.");
        }
        genre.Name = updateGenreInDto.Name;
        dbContext.SaveChanges();

        return Ok(genre);
    }

    [HttpDelete]
    public IActionResult DeleteGenre(Guid id)
    {
        var genre = dbContext.Genres.Find(id);
        if (genre is null)
        {
            return NotFound();
        }
        dbContext.Genres.Remove(genre);
        dbContext.SaveChanges();

        return Ok(genre);
    }
}
