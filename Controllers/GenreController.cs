
namespace testAPIDatabase.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using testAPIDatabase.Context;
using testAPIDatabase.Dtos;
using testAPIDatabase.Handlers.Commands;

[ApiController]
[Route("api/v2/[controller]/[action]")]
public class GenreController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    private readonly IMediator _mediator;

    public GenreController(AppDbContext dbContext, IMediator mediator)
    {
        _dbContext = dbContext;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllGenres()
    {
        try
        {
            var result = await _mediator.Send(new GetAllGenresCommand());
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetGenreById(Guid id)
    {
        try
        {
            var result = await _mediator.Send(new GetByIdGenreCommand { Id = id });
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddGenre(GenreInDto addGenreDto)
    {
        try
        {
            var result = await _mediator.Send(new AddGenreCommand
            {
                Name = addGenreDto.Name
            });
            return Ok(result);
        }
        catch (ConflictException ex)
        {
            return Conflict(ex.Message); // 409
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateGenre(Guid id, GenreInDto updateGenreInDto)
    {
        try
        {
            var result = await _mediator.Send(new UpdateGenreCommand
            {
                Id = id,
                Name = updateGenreInDto.Name
            });

            return Ok(result);
        }
        catch (ConflictException ex)
        {
            return Conflict(ex.Message); // 409
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message); // zwraca 404
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteGenre(Guid id)
    {
        try
        {
            var result = await _mediator.Send(new DeleteGenreCommand
            {
                Id = id
            });

            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message); // zwraca 404
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        } 
    }
}
