using AutoMapper;
using Book.Application.Dtos.BookDtos;
using Book.Domain.Models;

namespace Book.Application.MapperProfiles;

public class BookMapperProfile : Profile
{
    public BookMapperProfile()
    {
        CreateMap<BookModel, BookResponseDto>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))   
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => $"{src.Author.FirstName} {src.Author.LastName}"))
            .ForMember(dest => dest.BookOwner, opt => opt.MapFrom(src => src.BookOwner.UserName));

        CreateMap<BookResponseDto, BookModel>()
            .ForMember(dest => dest.Genre, opt => opt.Ignore())
            .ForMember(dest => dest.Author, opt => opt.Ignore())
            .ForMember(dest => dest.BookOwner, opt => opt.Ignore());

        CreateMap<BookModel, BookRequestDto>()
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
            .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => $"{src.Author.FirstName} {src.Author.LastName}"))
            .ForMember(dest => dest.BookOwner, opt => opt.MapFrom(src => src.BookOwner.UserName));

        CreateMap<BookRequestDto, BookModel>()
            .ForMember(dest => dest.Genre, opt => opt.Ignore())
            .ForMember(dest => dest.Author, opt => opt.Ignore())
            .ForMember(dest => dest.BookOwner, opt => opt.Ignore());
    }
}