using AutoMapper;
using Book.Application.Dtos.AuthorDtos;
using Book.Domain.Models;
using Book.Infrastructure.Repositories.Interfaces;
using Book.Infrastructure.Shared.Exceptions;
using Book.UseCases.UseCases.Author.GetAuthorById;
using Moq;

namespace Book.Test.UseCases.Author;

public class GetAuthorByIdTests
{
    private readonly Mock<IAuthorRepository> _mockAuthorRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly GetAuthorByIdHandler _handler;

    public GetAuthorByIdTests()
    {
        _mockAuthorRepository = new();
        _mockMapper = new();
        _handler = new(
            _mockAuthorRepository.Object,
            _mockMapper.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldReturnAuthor_WhenAuthorExists()
    {
        // Arrange
        var authorId = 1;
        var author = new AuthorModel { Id = authorId };
        var authorDto = new AuthorResponseDto { Id = authorId };

        _mockAuthorRepository.Setup(x => x.GetFirstByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(author);
        _mockMapper.Setup(x => x.Map<AuthorResponseDto>(author))
            .Returns(authorDto);

        // Act
        var command = new GetAuthorByIdCommand(authorId);
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(authorDto.Id, result.Id);
    }

    [Fact]
    public async Task Handle_ShouldThrowResourceNotFoundException_WhenAuthorDoesNotExist()
    {
        // Arrange
        var authorId = 1;

        _mockAuthorRepository.Setup(x => x.GetFirstByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((AuthorModel)null);

        // Act
        var command = new GetAuthorByIdCommand(authorId);

        // Assert
        await Assert.ThrowsAsync<ResourceNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }
}
