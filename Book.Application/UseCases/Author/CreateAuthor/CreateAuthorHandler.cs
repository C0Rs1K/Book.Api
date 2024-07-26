using AutoMapper;
using Book.Domain.Models;
using Book.Infrastructure.Repositories.Interfaces;
using Book.Infrastructure.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Book.UseCases.UseCases.Author.CreateAuthor;

public class CreateAuthorHandler(IAuthorRepository authorRepository, ICountryRepository countryRepository, IMapper mapper) : IRequestHandler<CreateAuthorCommand, int> 
{
    public async Task<int> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var authorDto = request.createAuthorDto;
        var author = mapper.Map<AuthorModel>(authorDto);

        author.CountryId = await CreateCountryAsync(authorDto.Country, cancellationToken);

        authorRepository.Create(author);
        await authorRepository.SaveChangesAsync(cancellationToken);
        return author.Id;
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
