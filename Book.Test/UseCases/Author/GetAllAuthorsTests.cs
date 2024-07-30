using AutoMapper;
using Book.Application.Dtos.AuthorDtos;
using Book.Domain.Models;
using Book.Infrastructure.Repositories.Interfaces;
using Book.Test.Extentions;
using Book.UseCases.UseCases.Author.GetAllAuthors;
using MockQueryable.Moq;
using Moq;
using System.Linq.Expressions;

namespace Book.Test.UseCases.Author;

public class GetAllAuthorsTests
{
    private readonly Mock<IAuthorRepository> _mockAuthorRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly GetAllAuthorsHandler _handler;

    public GetAllAuthorsTests()
    {
        _mockAuthorRepository = new();
        _mockMapper = new();
        _handler = new(
            _mockAuthorRepository.Object,
            _mockMapper.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldReturnAllAuthors_WhenAuthorsExist()
    {
        // Arrange
        var authors = new List<AuthorModel>
        {
            new AuthorModel { Id = 1 },
            new AuthorModel { Id = 2 }
        }.AsQueryable().BuildMock();

        var authorDtos = new List<AuthorResponseDto>
        {
            new AuthorResponseDto { Id = 1 },
            new AuthorResponseDto { Id = 2 }
        };

        var mockAuthorsDbSet = authors.BuildMockDbSet();

        _mockAuthorRepository.Setup(x => x.GetRange())
            .Returns(mockAuthorsDbSet.Object);

        _mockMapper.Setup(m => m.ProjectTo(It.IsAny<IQueryable<AuthorModel>>()
            , It.IsAny<object>()
            , It.IsAny<Expression<Func<AuthorResponseDto, object>>[]>()))
        .Returns(authorDtos.AsQueryable().BuildMock());

        // Act
        var command = new GetAllAuthorsCommand();
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal(authorDtos[0].Id, result.ElementAt(0).Id);
        Assert.Equal(authorDtos[1].Id, result.ElementAt(1).Id);
    }
}
