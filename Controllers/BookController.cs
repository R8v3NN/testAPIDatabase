
namespace testAPIDatabase.Controllers;

using testAPIDatabase.Context;
using Microsoft.AspNetCore.Mvc;
using testAPIDatabase.Dtos;
using testAPIDatabase.Entities;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/v2/[controller]/[action]")]
public class BookController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    public BookController(AppDbContext dbContext) 
    {
        _dbContext = dbContext;
    }
    [HttpGet]
    public IActionResult GetAllBooks()
    {
        var allBooks = _dbContext.Books.ToList();
        return Ok(allBooks);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public IActionResult GetBookById(Guid id)
    {
        var book = _dbContext.Books.Find(id);
        if (book is null)
        {
            return NotFound();
        }
        return Ok(book);
    }

    [HttpPost]
    public IActionResult AddBook(BookInDto addBookDto)
    {
        var bookEntity = new Book()
        { 
            Author = addBookDto.Author,
            Title = addBookDto.Title,
            PublishedDate = addBookDto.PublishedDate
        };

        _dbContext.Books.Add(bookEntity);
        _dbContext.SaveChanges();

        return Ok(bookEntity);
    }

    [HttpPut]
    public IActionResult UpdateBook(Guid id, BookInDto updateBookInDto)
    {
        var book = _dbContext.Books.Find(id);
        if (book is null)
        {
            return NotFound();
        }

        book.Author = updateBookInDto.Author;
        book.Title = updateBookInDto.Title;
        book.PublishedDate = updateBookInDto.PublishedDate;
        _dbContext.SaveChanges();

        return Ok(book);
    }

    [HttpDelete]
    public IActionResult DeleteBook(Guid id)
    {
        var book = _dbContext.Books.Find(id);
        if (book is null)
        {
            return NotFound();
        }
        _dbContext.Books.Remove(book);
        _dbContext.SaveChanges();

        return Ok(book);
    }
}
