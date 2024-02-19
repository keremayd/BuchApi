using Entities.Models;

namespace Repositories.Contracts;

public interface IBookRepository: IRepositoryBase<Book>
{
    IQueryable<Book> GetAllBooks(bool trackChanges);
    Book GetBookById(int id,bool trackChanges);
    void CreateBook(Book book);
    void UpdateBook(Book book);
    void DeleteBook(Book book);
    void DetachEntity<T>(T entity) where T : class;
}