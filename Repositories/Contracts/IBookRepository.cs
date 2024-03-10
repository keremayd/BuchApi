using Entities.Models;

namespace Repositories.Contracts;

public interface IBookRepository: IRepositoryBase<Book>
{
    Task<IEnumerable<Book>> GetAllBooksAsync(bool trackChanges);
    Task<Book> GetBookByIdAsync(int id,bool trackChanges);
    void CreateBook(Book book);
    void UpdateBook(Book book);
    void DeleteBook(Book book);
    void DetachEntity<T>(T entity) where T : class;
}