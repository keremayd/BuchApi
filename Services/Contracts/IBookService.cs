using Entities.Models;

namespace Services.Contracts;

public interface IBookService
{
    IEnumerable<Book> GetAllBooks(bool trackChanges);
    Book GetBookById(int id, bool trackChanges);
    Book CreateBook(Book book);
    void UpdateBook(int id, Book book, bool trackChanges);
    void DeleteBook(int id, bool trackChanges);
}