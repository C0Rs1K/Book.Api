using AutoMapper;
using Book.Application.Dtos.BookDtos;
using Book.Infrastructure.Repositories.Interfaces;
using Book.Infrastructure.Shared.Exceptions;
using MediatR;

namespace Book.Application.UseCases.Book.GetBookById;

internal class GetBookByIdHandler(IBookRepository bookRepository, IMapper mapper) : IRequestHandler<GetBookByIdCommand, BookResponseDto>
{
    public async Task<BookResponseDto> Handle(GetBookByIdCommand request, CancellationToken cancellationToken)
    {
        var book = await bookRepository.GetFirstByIdAsync(request.bookId, cancellationToken);
        ResourceNotFoundException.ThrowIfNull(book);
        return mapper.Map<BookResponseDto>(book);
    }
}
