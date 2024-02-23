using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Services.DataTransferObjects.Response;

namespace WebApi.Utilities.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Soldakinden sağdakine geçişi ifade eder.
        CreateMap<BookDtoForUpdate, Book>();
        CreateMap<Book, BookDtoResponse>().ReverseMap();
        CreateMap<BookDtoForInsertion, Book>();
    }
}