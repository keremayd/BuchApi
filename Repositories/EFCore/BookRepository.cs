using Entities.Models;
using Repositories.Contracts;

namespace Repositories.EFCore;

public class BookRepository : RepositoryBase<Book>, IBookRepository
{
    public BookRepository(RepositoryContext context) : base(context)
    {
    }

    public IQueryable<Book> GetAllBooks(bool trackChanges) => FindAll(trackChanges);

    public Book? GetBookById(int id, bool trackChanges) =>
        FindByCondition(b => b.Id == id, trackChanges)
            .SingleOrDefault(b => b.Id == id);

    public void CreateBook(Book book) => Create(book);

    public void UpdateBook(Book book) => Update(book);

    public void DeleteBook(Book book) => Delete(book);
}