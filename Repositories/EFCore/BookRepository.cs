using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore;

public class BookRepository : RepositoryBase<Book>, IBookRepository
{
    public BookRepository(RepositoryContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync(bool trackChanges) => await FindAll(trackChanges).ToListAsync();
    
    public void DetachEntity<T>(T entity) where T : class
    {
        var entry = _context.Entry(entity);
        if (entry.State != EntityState.Detached)
        {
            entry.State = EntityState.Detached;
        }
    }

    public async Task<Book?> GetBookByIdAsync(int id, bool trackChanges) => 
        await FindByCondition(b => b.Id == id, trackChanges)
            .SingleOrDefaultAsync(b => b.Id == id);

    public void CreateBook(Book book) => Create(book);

    public void UpdateBook(Book book) => Update(book);

    public void DeleteBook(Book book) => Delete(book);
}