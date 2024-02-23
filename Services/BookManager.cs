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

    public IEnumerable<BookDtoResponse> GetAllBooks(bool trackChanges)
    {
        var books = _manager.Book.GetAllBooks(trackChanges);
        return _mapper.Map<IEnumerable<BookDtoResponse>>(books);
    }

    public BookDtoResponse GetBookById(int id, bool trackChanges)
    {
        var book = _manager.Book.GetBookById(id, trackChanges);
        if (book is null)
        {
            throw new BookNotFoundException(id);
        }

        return _mapper.Map<BookDtoResponse>(book);
    }

    public BookDtoResponse CreateBook(BookDtoForInsertion bookDto)
    {
        var entity = _mapper.Map<Book>(bookDto);
        _manager.Book.CreateBook(entity);
        _manager.Save();
        return _mapper.Map<BookDtoResponse>(entity);
    }

    public void UpdateBook(int id, BookDtoForUpdate request, bool trackChanges)
    {
        var entity = _manager.Book.GetBookById(id, trackChanges);
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
        _manager.Save();
    }

    public void DeleteBook(int id, bool trackChanges)
    {
        var entity = _manager.Book.GetBookById(id, trackChanges);
        if (entity is null)
        {
            throw new BookNotFoundException(id);
        }
        _manager.Book.DeleteBook(entity);
    }
}