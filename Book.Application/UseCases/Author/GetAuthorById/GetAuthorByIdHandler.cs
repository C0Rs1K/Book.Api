using AutoMapper;
using Book.Application.Dtos.AuthorDtos;
using Book.Infrastructure.Repositories;
using Book.Infrastructure.Repositories.Interfaces;
using Book.Infrastructure.Shared.Exceptions;
using MediatR;

namespace Book.UseCases.UseCases.Author.GetAuthorById;

public class GetAuthorByIdHandler(IAuthorRepository authorRepository, IMapper mapper) : IRequestHandler<GetAuthorByIdCommand, AuthorResponseDto>
{
    public async Task<AuthorResponseDto> Handle(GetAuthorByIdCommand request, CancellationToken cancellationToken)
    {
        var author = await authorRepository.GetFirstByIdAsync(request.authorId, cancellationToken);
        ResourceNotFoundException.ThrowIfNull(author);
        return mapper.Map<AuthorResponseDto>(author);
    }
}
