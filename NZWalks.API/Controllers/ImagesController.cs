using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/images")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        // Create region
        // POST: https://localhost:7139/api/images/upload
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imageUploadRequestDto)
        {
            ValidateFileUpload(imageUploadRequestDto);

            if (ModelState.IsValid)
            {

            }

            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDto imageUploadRequestDto)
        {
            var allowedExtentions = new string[] { ".jpg", ".jpeg", ".png" };

            // If the file extention does NOT match the allowedExtentions values
            if (!allowedExtentions.Contains(Path.GetExtension(imageUploadRequestDto.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extention.");
            }

            // 10MB = 10485760 bytes
            if (imageUploadRequestDto.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10MB, please upload a smaller file size.");
            }
        }
    }
}
