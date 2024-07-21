using Book.Domain.Models;
using Book.Infrastructure.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Book.Application.Dtos.AuthorDtos;
using Book.Application.Services.Interfaces;
using Book.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Book.Application.Services;

public class AuthorService(
    IAuthorRepository authorRepository,
    ICountryRepository countryRepository,
    IMapper mapper)
    : IAuthorService
{
    public async Task<AuthorResponseDto> GetAuthorByIdAsync(int authorId, CancellationToken cancellationToken)
    {
        var author = await GetAuthorModelByIdAsync(authorId, cancellationToken);
        return mapper.Map<AuthorResponseDto>(author);
    }

    public async Task<IEnumerable<AuthorResponseDto>> GetAllAuthorsAsync(CancellationToken cancellationToken)
    {
        var authors = authorRepository.GetRange();
        return await mapper.ProjectTo<AuthorResponseDto>(authors).ToListAsync(cancellationToken);
    }

    public async Task<int> CreateAuthorAsync(AuthorRequestDto createAuthorDto,CancellationToken cancellationToken)
    {
        var author = mapper.Map<AuthorModel>(createAuthorDto);

        author.CountryId = await CreateCountryAsync(createAuthorDto.Country, cancellationToken);

        authorRepository.Create(author);
        await authorRepository.SaveChangesAsync(cancellationToken);
        return author.Id;
    }

    public async Task UpdateAuthorAsync(int authorId, AuthorRequestDto updateAuthorDto, CancellationToken cancellationToken)
    {
        var author = await GetAuthorModelByIdAsync(authorId, cancellationToken);

        author.CountryId = await CreateCountryAsync(updateAuthorDto.Country, cancellationToken);

        mapper.Map(updateAuthorDto, author);

        authorRepository.Update(author);
        await authorRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAuthorAsync(int authorId, CancellationToken cancellationToken)
    {
        var author = await GetAuthorModelByIdAsync(authorId, cancellationToken);

        authorRepository.Delete(author);
        await authorRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task<AuthorModel> GetAuthorModelByIdAsync(int authorId, CancellationToken cancellationToken)
    {
        var author = await authorRepository.GetFirstByIdAsync(authorId, cancellationToken);

        if (author == null)
        {
            throw new ResourceNotFoundException(nameof(author));
        }

        return author;
    }

    private async Task<int> CreateCountryAsync(string countryName, CancellationToken cancellationToken)
    {
        var country = await countryRepository.GetRange()
            .FirstOrDefaultAsync(x => x.Name == countryName, cancellationToken);

        if (country == null)
        {
            country = new CountryModel
            {
                Name = countryName,
            };

            countryRepository.Create(country);

            await countryRepository.SaveChangesAsync(cancellationToken);
        }

        return country.Id;
    }
}