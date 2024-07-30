using AutoMapper;
using Book.Application.Dtos.AuthorDtos;
using Book.Domain.Models;
using Book.Infrastructure.Repositories.Interfaces;
using Book.Infrastructure.Shared.Exceptions;
using Book.UseCases.UseCases.Author.CreateAuthor;
using MockQueryable.Moq;
using Moq;

namespace Book.Test.UseCases.Author;

public class CreateAuthorTests
{
    private readonly Mock<IAuthorRepository> _authorRepositoryMock;
    private readonly Mock<ICountryRepository> _countryRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateAuthorHandler _handler;

    public CreateAuthorTests()
    {
        _authorRepositoryMock = new();
        _countryRepositoryMock = new();
        _mapperMock = new();
        _handler = new(_authorRepositoryMock.Object, _countryRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_WhenAuthorDoesNotExist_ShouldCreateNewAuthor()
    {
        // Arrange
        var authorRequestDto = new AuthorRequestDto
        {
            FirstName = "John",
            LastName = "Doe",
            Country = "USA"
        };

        var author = new AuthorModel { Id = 1 };
        _mapperMock.Setup(x => x.Map<AuthorModel>(It.IsAny<AuthorRequestDto>()))
            .Returns(author);

        _countryRepositoryMock.Setup(x => x.GetRange())
            .Returns(new[] { new CountryModel { Name = "Canada" } }.AsQueryable().BuildMock());

        _authorRepositoryMock.Setup(x => x.Create(It.IsAny<AuthorModel>()))
            .Verifiable();
        _authorRepositoryMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Verifiable();

        var command = new CreateAuthorCommand(authorRequestDto);

        // Act
        var authorId = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(author.Id, authorId);
        _authorRepositoryMock.Verify(x => x.Create(author), Times.Once);
        _authorRepositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenCountryNameExist_ShouldThrowBadRequestException()
    {
        // Arrange
        var authorRequestDto = new AuthorRequestDto
        {
            FirstName = "John",
            LastName = "Doe",
            Country = "Canada"
        };

        _mapperMock.Setup(x => x.Map<AuthorModel>(It.IsAny<AuthorRequestDto>()))
            .Returns(new AuthorModel { Id = 1 });

        _countryRepositoryMock.Setup(x => x.GetRange())
            .Returns(new[] { new CountryModel { Name = "Canada" } }.AsQueryable().BuildMock());

        var command = new CreateAuthorCommand(authorRequestDto);

        // Act and Assert
        await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, CancellationToken.None));
    }
}
