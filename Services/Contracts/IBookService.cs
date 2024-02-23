using Entities.DataTransferObjects;
using Entities.Models;
using Services.DataTransferObjects.Response;

namespace Services.Contracts;

public interface IBookService
{
    IEnumerable<BookDtoResponse> GetAllBooks(bool trackChanges);
    BookDtoResponse GetBookById(int id, bool trackChanges);
    BookDtoResponse CreateBook(BookDtoForInsertion book);
    void UpdateBook(int id, BookDtoForUpdate bookDto, bool trackChanges);
    void DeleteBook(int id, bool trackChanges);
}