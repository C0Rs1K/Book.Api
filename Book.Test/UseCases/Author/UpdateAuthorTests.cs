using AutoMapper;
using Book.Application.Dtos.AuthorDtos;
using Book.Domain.Models;
using Book.Infrastructure.Repositories.Interfaces;
using Book.Infrastructure.Shared.Exceptions;
using Book.UseCases.UseCases.Author.UpdateAuthor;
using MockQueryable.Moq;
using Moq;

namespace Book.Test.UseCases.Author;

public class UpdateAuthorTests
{
    private readonly Mock<IAuthorRepository> _mockAuthorRepository;
    private readonly Mock<ICountryRepository> _mockCountryRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly UpdateAuthorHandler _handler;

    public UpdateAuthorTests()
    {
        _mockAuthorRepository = new();
        _mockCountryRepository = new();
        _mockMapper = new();
        _handler = new(
            _mockAuthorRepository.Object,
            _mockCountryRepository.Object,
            _mockMapper.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldUpdateAuthor_WhenAuthorExists()
    {
        // Arrange
        var authorId = 1;
        var authorDto = new AuthorRequestDto();
        var author = new AuthorModel { Id = authorId };

        _mockAuthorRepository.Setup(x => x.GetFirstByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(author);
        _mockCountryRepository.Setup(x => x.GetRange())
            .Returns(new List<CountryModel>().AsQueryable().BuildMock());
        _mockMapper.Setup(x => x.Map(It.IsAny<AuthorRequestDto>(), It.IsAny<AuthorModel>()));

        // Act
        var command = new UpdateAuthorCommand(authorId, authorDto);
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockAuthorRepository.Verify(x => x.Update(author), Times.Once);
        _mockAuthorRepository.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowResourceNotFoundException_WhenAuthorDoesNotExist()
    {
        // Arrange
        var authorId = 1;
        var authorDto = new AuthorRequestDto();

        _mockAuthorRepository.Setup(x => x.GetFirstByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((AuthorModel)null);

        // Act
        var command = new UpdateAuthorCommand(authorId, authorDto);

        // Assert
        await Assert.ThrowsAsync<ResourceNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ShouldThrowBadRequestException_WhenCountryAlreadyExists()
    {
        // Arrange
        var authorId = 1;
        var authorDto = new AuthorRequestDto { Country = "ExistingCountry" };
        var author = new AuthorModel { Id = authorId };
        var existingCountry = new CountryModel { Name = "ExistingCountry" };

        _mockAuthorRepository.Setup(x => x.GetFirstByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(author);
        _mockCountryRepository.Setup(x => x.GetRange())
            .Returns(new List<CountryModel> { existingCountry }.AsQueryable().BuildMock());

        // Act
        var command = new UpdateAuthorCommand(authorId, authorDto);

        // Assert
        await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, CancellationToken.None));
    }
}
