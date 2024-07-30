using Book.Domain.Models;
using Book.Infrastructure.Repositories.Interfaces;
using Book.Infrastructure.Shared.Exceptions;
using Book.UseCases.UseCases.Author.DeleteAuthor;
using Moq;

namespace Book.Test.UseCases.Author;

public class DeleteAuthorTests
{
    private readonly Mock<IAuthorRepository> _authorRepositoryMock;
    private readonly DeleteAuthorHandler _handler;

    public DeleteAuthorTests()
    {
        _authorRepositoryMock = new();
        _handler = new(_authorRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WhenAuthorIsNotFound_ShouldThrowResourceNotFoundException()
    {
        // Arrange
        _authorRepositoryMock.Setup(x => x.GetFirstByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((AuthorModel)null);

        var command = new DeleteAuthorCommand(1);

        // Act and Assert
        await Assert.ThrowsAsync<ResourceNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_WhenAuthorIsFound_ShouldDeleteAuthorAndSaveChanges()
    {
        // Arrange
        var author = new AuthorModel { Id = 1 };
        _authorRepositoryMock.Setup(x => x.GetFirstByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(author);
        _authorRepositoryMock.Setup(x => x.Delete(It.IsAny<AuthorModel>()))
            .Verifiable();
        _authorRepositoryMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Verifiable();

        var command = new DeleteAuthorCommand(1);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _authorRepositoryMock.Verify(x => x.Delete(author), Times.Once);
        _authorRepositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
