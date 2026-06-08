using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.Blazor.Inputs;
using static System.Net.WebRequestMethods;

namespace DoYouOwnIt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment hostingEnv;
        private readonly IUserContextService _userContextService;
        public FileController(IConfiguration configuration, IWebHostEnvironment hostingEnv, IUserContextService userContext)
        {
            _configuration = configuration;
            this.hostingEnv = hostingEnv;
            _userContextService = userContext;
        }

        [HttpPost("Save")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Save([FromForm] IList<IFormFile> UploadFiles)
        {
            if (UploadFiles == null || UploadFiles.Count == 0)
                return BadRequest("No files uploaded.");

            var storageString = _configuration.GetValue<string>("ConnectionStrings:storageConnection");
            if (storageString == null)
                return (IActionResult)Results.Problem(title: "Invalid connection to media storage",
                    statusCode: 500);

            try
            {

                var savedFiles = new List<string>();

                foreach (var file in UploadFiles)
                {
                    var container = new BlobContainerClient(storageString, "uploadfolder");

                    var createResponse = await container.CreateIfNotExistsAsync();

                    if (createResponse != null && createResponse.GetRawResponse().Status == 201)
                        await container.SetAccessPolicyAsync(PublicAccessType.Blob);

                    var blob = container.GetBlobClient(file.FileName);

                    await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);

                    using (var fileStream = file.OpenReadStream())
                    {
                        await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = file.ContentType });
                    }

                    savedFiles.Add(file.FileName);
                }
                var fileUploads = new { files = savedFiles};
                return new JsonResult(fileUploads);
            }
            catch (Exception ex)
            {
                // return problem details / 500
                return Problem(detail: ex.Message);
            }
        }

        [HttpPost("DeleteFile")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> DeleteFile([FromForm] IList<IFormFile> UploadFiles)
        {
            try
            {
                if (UploadFiles == null || UploadFiles.Count == 0)
                    return BadRequest("No files provided to delete.");

                var storageString = _configuration.GetValue<string>("ConnectionStrings:storageConnection");
                if (storageString == null)
                    return (IActionResult)Results.Problem(title: "Invalid connection to media storage",
                        statusCode: 500);

                foreach (var file in UploadFiles)
                {
                    var container = new BlobContainerClient(storageString, "uploadfolder");

                    var createResponse = await container.CreateIfNotExistsAsync();

                    if (createResponse != null && createResponse.GetRawResponse().Status == 201)
                        await container.SetAccessPolicyAsync(PublicAccessType.Blob);

                    var blob = container.GetBlobClient(file.FileName);

                    var exists = await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);

                    if (exists)
                        return Ok($"File '{file.FileName}' has been deleted.");

                    else
                    {
                        return NotFound($"File '{file.FileName}' not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
            return StatusCode(500, $"No file processed.");
        }

        [HttpPost("RTFSave")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> SaveRichTextFile([FromForm] IList<IFormFile> UploadFiles)
        {
            if (UploadFiles == null || UploadFiles.Count == 0)
                return BadRequest("No files uploaded.");

            try
            {
                var targetPath = Path.Combine(hostingEnv.ContentRootPath, "wwwroot", "RichTextFiles");
                if (!Directory.Exists(targetPath))
                {
                    Directory.CreateDirectory(targetPath);
                }

                var savedFiles = new List<string>();
                var filePaths = new List<string>();

                foreach (var file in UploadFiles)
                {
                    if (file == null || file.Length == 0)
                        continue;

                    var originalFileName = Path.GetFileName(file.FileName);
                    var candidateName = originalFileName;
                    var fullPath = Path.Combine(targetPath, candidateName);
                    var counter = 1;

                    // generate a unique file name if one already exists
                    while (System.IO.File.Exists(fullPath))
                    {
                        var nameOnly = Path.GetFileNameWithoutExtension(originalFileName);
                        var ext = Path.GetExtension(originalFileName);
                        candidateName = $"{nameOnly}_{counter}{ext}";
                        fullPath = Path.Combine(targetPath, candidateName);
                        counter++;
                    }

                    await using (var fs = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(fs);
                        await fs.FlushAsync();
                    }
                    var savefileDir = Path.Combine("RichTextFiles", candidateName);
                    filePaths.Add(savefileDir);

                    savedFiles.Add(candidateName);
                }
                var fileUploads = new { files = savedFiles, fileLocations = filePaths };
                return new JsonResult(fileUploads);
            }
            catch (Exception ex)
            {
                // return problem details / 500
                return Problem(detail: ex.Message);
            }
        }

        [HttpPost("RTFDeleteFile")]
        [Consumes("multipart/form-data")]
        public IActionResult DeleteRichTextFile([FromForm] IList<IFormFile> UploadFiles)
        {
            try
            {
                if (UploadFiles == null || UploadFiles.Count == 0)
                    return BadRequest("No files provided to delete.");

                foreach (var uploadFile in UploadFiles)
                {
                    var fileName = Path.GetFileName(uploadFile.FileName);
                    var filePath = Path.Combine(hostingEnv.WebRootPath ?? hostingEnv.ContentRootPath, "RichTextFiles", fileName);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                        return Ok($"File '{fileName}' has been deleted.");
                    }
                    else
                    {
                        return NotFound($"File '{fileName}' not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
            return StatusCode(500, $"No file processed.");
        }

    }
}
