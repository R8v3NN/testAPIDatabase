using Humanizer.Localisation;
using Microsoft.AspNetCore.Mvc;
using testAPIDatabase.Context;
using testAPIDatabase.Dtos;
using testAPIDatabase.Entities;

namespace testAPIDatabase.Controllers;

[ApiController]
[Route("api/v2/[controller]/[action]")]
public class BookGenreController: ControllerBase
{
    private readonly AppDbContext _dbContext;

    public BookGenreController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult GetAllBookGenres()
    {
        var allBookGenres = _dbContext.BookGenres.ToList();
        return Ok(allBookGenres);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public IActionResult GetBookGenreById(Guid id)
    {
        var BookGenre = _dbContext.BookGenres.Find(id);
        if (BookGenre is null)
        {
            return NotFound();
        }
        return Ok(BookGenre);
    }

    [HttpGet]
    [Route("{bookId:guid}")]
    public IActionResult GetBookGenreByBookId(Guid bookId)
    {
        var book = _dbContext.BookGenres.Where(bg => bg.BookId == bookId).Select(bg => bg.Genre).ToList();
        if (book is null)
        {
            return NotFound();
        }
        return Ok(book);
    }

    [HttpPost]
    public IActionResult AddBookGenre(BookGenreInDto addBookGenreInDto)
    {
        var book = _dbContext.Books.Find(addBookGenreInDto.BookId);
        if (book is null)
        {
            return NotFound();
        }

        var genre = _dbContext.Genres.Find(addBookGenreInDto.GenreId);
        if (genre is null)
        {
            return NotFound();
        }

        var genreAlreadyExists = _dbContext.BookGenres.FirstOrDefault(g => g.GenreId == genre.Id);
        if (genreAlreadyExists is not null)
        {
            return Conflict("Genre already assign to this book.");
        }

        var bookGenreEntity = new BookGenre()
        {
            Book = book,
            Genre = genre,
        };

        _dbContext.BookGenres.Add(bookGenreEntity);
        _dbContext.SaveChanges();

        return Ok(bookGenreEntity);
    }

    [HttpPut]
    public IActionResult UpdateBookGenre(BookGenreInDto updateBookGenreInDto, Guid id)
    {
        var book = _dbContext.Books.Find(updateBookGenreInDto.BookId);
        if (book is null)
        {
            return NotFound();
        }

        var genre = _dbContext.Genres.Find(updateBookGenreInDto.GenreId);
        if (genre is null)
        {
            return NotFound();
        }

        var bookGenreId = _dbContext.BookGenres.Find(id);
        if (bookGenreId is null)
        {
            return NotFound();
        }

        var genreAlreadyAssign = _dbContext.BookGenres.FirstOrDefault(g => g.GenreId == genre.Id);
        if (genreAlreadyAssign is not null)
        {
            return Conflict("Genre already assign to this book.");
        }

        bookGenreId.BookId = book.Id;
        bookGenreId.GenreId = genre.Id;
        
        _dbContext.SaveChanges();

        return Ok(bookGenreId);
    }

    [HttpDelete]
    public IActionResult DeleteBookGenre(Guid id)
    {
        var bookGenre = _dbContext.BookGenres.Find(id);
        if (bookGenre is null)
        {
            return NotFound();
        }
        _dbContext.BookGenres.Remove(bookGenre);
        _dbContext.SaveChanges();

        return Ok(bookGenre);
    }
}
