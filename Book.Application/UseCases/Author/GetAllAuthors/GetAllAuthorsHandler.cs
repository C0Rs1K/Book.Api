using AutoMapper;
using Book.Application.Dtos.AuthorDtos;
using Book.Infrastructure.Repositories.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Book.UseCases.UseCases.Author.GetAllAuthors;

public class GetAllAuthorsHandler(IAuthorRepository authorRepository, IMapper mapper) : IRequestHandler<GetAllAuthorsCommand, IEnumerable<AuthorResponseDto>>
{
    public async Task<IEnumerable<AuthorResponseDto>> Handle(GetAllAuthorsCommand request, CancellationToken cancellationToken)
    {
        var authors = authorRepository.GetRange();
        return await mapper.ProjectTo<AuthorResponseDto>(authors).ToListAsync(cancellationToken);
    }
}
