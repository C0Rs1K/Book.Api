using Book.Application.UseCases.Genre.GetAllGenres;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Book.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GenreController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<string>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllBooksAsync(CancellationToken cancellationToken)
    {
        return Ok(await sender.Send(new GetAllGenresCommand(),cancellationToken));
    }
}
