using AutoMapper;
using Book.Application.Dtos.AuthorDtos;
using Book.Domain.Models;

namespace Book.Application.MapperProfiles;

public class AuthorMapperProfile : Profile
{
    public AuthorMapperProfile()
    {
        CreateMap<AuthorModel, AuthorResponseDto>()
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country.Name));

        CreateMap<AuthorResponseDto, AuthorModel>()
            .ForMember(dest => dest.Country, opt => opt.Ignore());

        CreateMap<AuthorModel, AuthorRequestDto>()
            .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country.Name))
            .ReverseMap();

        CreateMap<AuthorRequestDto, AuthorModel>()
            .ForMember(dest => dest.Country, opt => opt.Ignore());
    }
}