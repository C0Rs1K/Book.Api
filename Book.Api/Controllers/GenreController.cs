using Book.Application.Dtos.BookDtos;
using Book.Application.SearchParameters;
using Book.Application.Services;
using Book.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Book.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenreController(IGenreService genreService) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<string>> GetAllBooksAsync(CancellationToken cancellationToken)
    {
        return await genreService.GetAllGenresAsync(cancellationToken);
    }
}
