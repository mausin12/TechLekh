using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechLekh.Web.Repositories;

namespace TechLekh.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this._imageRepository = imageRepository;
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            var imageUrl = await _imageRepository.UploadAsync(file);
            if (imageUrl == null)
            {
                return Problem(
                    detail: "Something went wrong",
                    statusCode: StatusCodes.Status500InternalServerError
                    );
            }
            return Ok(new
            {
                link = imageUrl
            });
        }
    }
}
