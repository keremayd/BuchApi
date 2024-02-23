using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Services.Contracts;

namespace Presentation.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly IServiceManager _manager;

    public BooksController(IServiceManager manager)
    {
        _manager = manager;
    }

    [HttpGet]
    public IActionResult GetAllBooks()
    {
        var books = _manager.BookService.GetAllBooks(false);
        return Ok(books);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetOneBook([FromRoute(Name = "id")] int id)
    {
        var book = _manager.BookService.GetBookById(id, false);

        return Ok(book);
    }

    [HttpPost]
    public IActionResult CreateBook([FromBody] BookDtoForInsertion bookDto)
    {
        if (bookDto is null)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        var book = _manager.BookService.CreateBook(bookDto);
        return StatusCode(201, book);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateBook([FromRoute(Name = "id")] int id, [FromBody] BookDtoForUpdate book)
    {
        if (book is null)
        {
            throw new BookNotFoundException(id);
        }

        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        _manager.BookService.UpdateBook(id, book, false);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteBook([FromRoute(Name = "id")] int id)
    {
        _manager.BookService.DeleteBook(id, false);
        return NoContent();
    }

    // TODO Partially hali yapılacak
    // [HttpPatch("{id:int}")]
    // public IActionResult PartiallyUpdateBook([FromRoute(Name = "id")] int id,
    //     [FromBody] JsonPatchDocument<Book> bookPatch)
    // {
    //     var entity = _manager.BookService.GetBookById(id, true);
    //
    //     if (entity is null)
    //     {
    //         return NotFound();
    //     }
    //
    //     bookPatch.ApplyTo(entity);
    //     _manager.BookService.UpdateBook(id, new BookDtoForUpdate(entity.Id, entity.Title, entity.Price), true);
    //     return NoContent();
    // }
}