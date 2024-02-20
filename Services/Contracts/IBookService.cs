using Entities.DataTransferObjects;
using Entities.Models;
using Services.DataTransferObjects.Response;

namespace Services.Contracts;

public interface IBookService
{
    IEnumerable<BookDtoResponse> GetAllBooks(bool trackChanges);
    Book GetBookById(int id, bool trackChanges);
    Book CreateBook(Book book);
    void UpdateBook(int id, BookDtoForUpdate bookDto, bool trackChanges);
    void DeleteBook(int id, bool trackChanges);
}