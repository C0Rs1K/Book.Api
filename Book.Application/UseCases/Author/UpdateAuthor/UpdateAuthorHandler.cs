using AutoMapper;
using Book.Domain.Models;
using Book.Infrastructure.Repositories.Interfaces;
using Book.Infrastructure.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Book.UseCases.UseCases.Author.UpdateAuthor;

public class UpdateAuthorHandler(IAuthorRepository authorRepository, ICountryRepository countryRepository, IMapper mapper) : IRequestHandler<UpdateAuthorCommand>
{
    public async Task Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await authorRepository.GetFirstByIdAsync(request.authorId, cancellationToken);
        ResourceNotFoundException.ThrowIfNull(author);

        var authorDto = request.updateAuthorDto;

        author.CountryId = await CreateCountryAsync(authorDto.Country, cancellationToken);

        mapper.Map(authorDto, author);

        authorRepository.Update(author);
        await authorRepository.SaveChangesAsync(cancellationToken);
    }

    private async Task<int> CreateCountryAsync(string countryName, CancellationToken cancellationToken)
    {
        var country = await countryRepository.GetRange()
            .FirstOrDefaultAsync(x => x.Name == countryName, cancellationToken);

        BadRequestException.ThrowIfNotNull(country);

        country = new CountryModel
        {
            Name = countryName,
        };

        countryRepository.Create(country);

        await countryRepository.SaveChangesAsync(cancellationToken);

        return country.Id;
    }
}
