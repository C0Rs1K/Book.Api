using Book.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Book.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController(IImageService imageService) : ControllerBase
    {
        [HttpPost]
        public async Task<string> UploadImageAsync([FromBody] string imageBase)
        {
            return await imageService.UploadImageAsync(imageBase);
        }
    }
}
