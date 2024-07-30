using Book.Application.UseCases.Image.UploadImage;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Book.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController(ISender sender) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType<string>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UploadImageAsync([FromBody] string imageBase, CancellationToken cancellationToken)
        {
            return CreatedAtAction(nameof(UploadImageAsync), await sender.Send(new UploadImageCommand(imageBase), cancellationToken));
        }
    }
}
