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
    public async Task<IActionResult> GetAllBooks()
    {
        var books = await _manager.BookService.GetAllBooksAsync(false);
        return Ok(books);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOneBook([FromRoute(Name = "id")] int id)
    {
        var book = await _manager.BookService.GetBookByIdAsync(id, false);

        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] BookDtoForInsertion bookDto)
    {
        if (bookDto is null)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        var book = await _manager.BookService.CreateBookAsync(bookDto);
        return StatusCode(201, book);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateBook([FromRoute(Name = "id")] int id, [FromBody] BookDtoForUpdate book)
    {
        if (book is null)
        {
            throw new BookNotFoundException(id);
        }

        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        await _manager.BookService.UpdateBookAsync(id, book, false);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteBook([FromRoute(Name = "id")] int id)
    {
        await _manager.BookService.DeleteBookAsync(id, false);
        return NoContent();
    }

    // TODO Partially hali yapılacak
    // [HttpPatch("{id:int}")]
    // public IActionResult PartiallyUpdateBook([FromRoute(Name = "id")] int id,
    //     [FromBody] JsonPatchDocument<Book> bookPatch)
    // {
    //     var entity = _manager.BookService.GetBookByIdAsync(id, true);
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