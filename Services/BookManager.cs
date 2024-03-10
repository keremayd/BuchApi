using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using Services.DataTransferObjects.Response;

namespace Services;

public class BookManager : IBookService
{
    private readonly IRepositoryManager _manager;
    private readonly ILoggerService _logger;
    private readonly IMapper _mapper;

    public BookManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
    {
        _manager = manager;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BookDtoResponse>> GetAllBooksAsync(bool trackChanges)
    {
        var books = await _manager.Book.GetAllBooksAsync(trackChanges);
        return _mapper.Map<IEnumerable<BookDtoResponse>>(books);
    }

    public async Task<BookDtoResponse> GetBookByIdAsync(int id, bool trackChanges)
    {
        var book = await _manager.Book.GetBookByIdAsync(id, trackChanges);
        if (book is null)
        {
            throw new BookNotFoundException(id);
        }

        return _mapper.Map<BookDtoResponse>(book);
    }

    public async Task<BookDtoResponse> CreateBookAsync(BookDtoForInsertion bookDto)
    {
        var entity = _mapper.Map<Book>(bookDto);
        _manager.Book.CreateBook(entity);
        await _manager.SaveAsync();
        return _mapper.Map<BookDtoResponse>(entity);
    }

    public async Task UpdateBookAsync(int id, BookDtoForUpdate request, bool trackChanges)
    {
        var entity = await _manager.Book.GetBookByIdAsync(id, trackChanges);
        _manager.Book.DetachEntity(entity);
        if (entity is null)
        {
            throw new BookNotFoundException(id);
        }

        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }
        
        // Mapping
        entity = _mapper.Map<Book>(request);
        
        //_manager.Book.Update(entity);
        _manager.Book.UpdateBook(entity);
        await _manager.SaveAsync();
    }

    public async Task DeleteBookAsync(int id, bool trackChanges)
    {
        var entity = await _manager.Book.GetBookByIdAsync(id, trackChanges);
        if (entity is null)
        {
            throw new BookNotFoundException(id);
        }
        _manager.Book.DeleteBook(entity);
        await _manager.SaveAsync();
    }
}