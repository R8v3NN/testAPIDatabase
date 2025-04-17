using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using testAPIDatabase.Dtos;
using testAPIDatabase.Entities;

namespace testAPIDatabase.Controllers;

using testAPIDatabase.Context;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly AppDbContext dbContext;
    public BookController(AppDbContext dbContext) 
    {
        this.dbContext = dbContext;
    }
    [HttpGet]
    public IActionResult GetAllBooks()
    {
        var allBooks = dbContext.Books.ToList();
        return Ok(allBooks);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public IActionResult GetBookById(Guid id)
    {
        var book = dbContext.Books.Find(id);
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

        dbContext.Books.Add(bookEntity);
        dbContext.SaveChanges();

        return Ok(bookEntity);
    }

    [HttpPut]
    public IActionResult UpdateBook(Guid id, BookInDto updateBookInDto)
    {
        var book = dbContext.Books.Find(id);
        if (book is null)
        {
            return NotFound();
        }

        book.Author = updateBookInDto.Author;
        book.Title = updateBookInDto.Title;
        book.PublishedDate = updateBookInDto.PublishedDate;
        dbContext.SaveChanges();

        return Ok(book);
    }

    [HttpDelete]
    public IActionResult DeleteBook(Guid id)
    {
        var book = dbContext.Books.Find(id);
        if (book is null)
        {
            return NotFound();
        }
        dbContext.Books.Remove(book);
        dbContext.SaveChanges();

        return Ok(book);
    }
}
