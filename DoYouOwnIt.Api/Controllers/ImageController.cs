using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;

namespace DoYouOwnIt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        // counter used for generating unique file names
        private string? imageFileName;
        private readonly IWebHostEnvironment hostingEnv;
        private readonly IConfiguration _configuration;

        public ImageController(IWebHostEnvironment env, IConfiguration configuration)
        {
            this.hostingEnv = env;
            _configuration = configuration;
        }

        // Single explicit HTTP method, declare expected content-type for Swagger UI
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

            var containerName = Request.Headers["containerName"];
            var blobDirectory = Request.Headers["blobDirectory"];

            try
            {
                var savedFiles = new List<string>();
                var filePaths = new List<string>();

                foreach (var file in UploadFiles)
                {
                    if (file == null || file.Length == 0)
                        continue;

                    string filename = file.FileName;

                    var container = new BlobContainerClient(storageString, containerName);

                    var createResponse = await container.CreateIfNotExistsAsync();

                    if (createResponse != null && createResponse.GetRawResponse().Status == 201)
                        await container.SetAccessPolicyAsync(PublicAccessType.Blob);

                    string blobPath = string.IsNullOrEmpty(blobDirectory) ?
                        filename : $"{blobDirectory}/{filename}";

                    var blob = container.GetBlobClient(blobPath);

                    await blob.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);

                    using (var fileStream = file.OpenReadStream())
                    {
                        await blob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = file.ContentType });
                    }

                    var imagePath = "https://images.gmntgstrg.com/" + containerName + "/" + blobDirectory + "/" + file.FileName;

                    savedFiles.Add(file.FileName);
                    filePaths.Add(imagePath);
                }

                return Ok(new { files = savedFiles, fileLocations = filePaths });
            }
            catch (Exception ex)
            {
                // return problem details / 500
                return Problem(detail: ex.Message);
            }
        }

        [HttpPost("Rename")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Rename([FromForm] IList<IFormFile> UploadFiles)
        {
            if (UploadFiles == null || UploadFiles.Count == 0)
                return BadRequest("No files uploaded.");

            var storageString = _configuration.GetValue<string>("ConnectionStrings:storageConnection");
            if (storageString == null)
                return (IActionResult)Results.Problem(title: "Invalid connection to media storage",
                    statusCode: 500);

            try
            {
                var targetPath = Path.Combine(hostingEnv.WebRootPath ?? hostingEnv.ContentRootPath, "Images");
                if (!Directory.Exists(targetPath))
                    Directory.CreateDirectory(targetPath);

                var results = new List<string>();

                foreach (var file in UploadFiles)
                {
                    if (file == null || file.Length == 0)
                        continue;

                    var filename = Path.GetFileName(file.FileName);
                    imageFileName = filename;
                    var path = Path.Combine(targetPath, imageFileName);

                    // ensure unique name
                    var counter = 1;
                    while (System.IO.File.Exists(path))
                    {
                        imageFileName = $"rteImage{counter}-{filename}";
                        path = Path.Combine(targetPath, imageFileName);
                        counter++;
                    }

                    await using (var fs = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(fs);
                        await fs.FlushAsync();
                    }

                    results.Add(imageFileName);
                }

                return Ok(new { names = results });
            }
            catch (Exception e)
            {
                return Problem(detail: e.Message);
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

                var containerName = Request.Headers["containerName"];
                var blobDirectory = Request.Headers["blobDirectory"];

                foreach (var file in UploadFiles)
                {
                    var container = new BlobContainerClient(storageString, containerName);

                    var createResponse = await container.CreateIfNotExistsAsync();

                    if (createResponse != null && createResponse.GetRawResponse().Status == 201)
                        await container.SetAccessPolicyAsync(PublicAccessType.Blob);

                    string filename = file.FileName;

                    string blobPath = string.IsNullOrEmpty(blobDirectory) ?
                        filename : $"{blobDirectory}/{filename}";

                    var blob = container.GetBlobClient(blobPath);

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
    }
}
