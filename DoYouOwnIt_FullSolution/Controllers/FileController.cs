using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoYouOwnIt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public FileController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost("upload")]
        public async Task<ActionResult> Upload()
        {
            var file = Request.Form.Files[0];
            var uploadFolder = _configuration.GetValue<string>("FileUploadFolder");
            if (string.IsNullOrWhiteSpace(uploadFolder))
            {
                return BadRequest("Upload folder is not configured.");
            }
            var filePath = Path.Combine(uploadFolder, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return Ok(file.FileName);
        }

        [HttpPost("delete")]
        public async Task<ActionResult> DeleteFilePost([FromBody] string fileName)
        {
            Console.WriteLine($"Received fileName: {fileName}");
            var uploadFolder = _configuration.GetValue<string>("FileUploadFolder");
            Console.WriteLine($"Upload folder from config: {uploadFolder}");
            if (string.IsNullOrWhiteSpace(uploadFolder))
            {
                return BadRequest("Upload folder is not configured.");
            }
            var filePath = Path.Combine(uploadFolder, fileName);
            Console.WriteLine($"Full file path: {filePath}");
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found.");
            }
            System.IO.File.Delete(filePath);
            return Ok("File deleted successfully.");
        }

    }
}
