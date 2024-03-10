using Entities.DataTransferObjects;
using Entities.Models;
using Services.DataTransferObjects.Response;

namespace Services.Contracts;

public interface IBookService
{
    Task<IEnumerable<BookDtoResponse>> GetAllBooksAsync(bool trackChanges);
    Task<BookDtoResponse> GetBookByIdAsync(int id, bool trackChanges);
    Task<BookDtoResponse> CreateBookAsync(BookDtoForInsertion book);
    Task UpdateBookAsync(int id, BookDtoForUpdate bookDto, bool trackChanges);
    Task DeleteBookAsync(int id, bool trackChanges);
}