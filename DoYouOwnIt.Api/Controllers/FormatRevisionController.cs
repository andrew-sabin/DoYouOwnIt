using DoYouOwnIt.Shared.Models.FormatRevision;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoYouOwnIt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormatRevisionController : ControllerBase
    {
        private readonly IFormatRevisionService _formatRevisionService;
        public FormatRevisionController(IFormatRevisionService formatRevisionService)
        {
            _formatRevisionService = formatRevisionService;
        }

        [HttpGet]
        public async Task<ActionResult<List<FormatRevisionResponse>>> GetFormatRevisionsAsync()
        {
            var result = await _formatRevisionService.GetFormatRevisionsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FormatRevisionResponse>> GetFormatRevisionByIdAsync(int id)
        {
            var result = await _formatRevisionService.GetFormatRevisionByIdAsync(id);
            if (result == null)
                return BadRequest($"No FormatRevision with {id} found");

            return Ok(result);
        }

        [HttpGet("format/{formatId}")]
        public async Task<ActionResult<List<FormatRevisionResponse>>> GetFormatRevisionsByFormatId(int formatId)
        {
            var result = await _formatRevisionService.GetFormatRevisionsByFormatId(formatId);
            return Ok(result);
        }

        [HttpGet("format/recent/{formatId}")]
        public async Task<ActionResult<FormatRevisionResponse>> GetMostRecentRevisionResponseByFormatId(int formatId)
        {
            var result = await _formatRevisionService.GetLatestFormatRevisionByFormatId(formatId);
            if (result == null)
                return BadRequest("Format did not have any revisions");

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Moderator, AlphaTester")]
        public async Task<ActionResult<FormatRevisionResponse>> CreateNewFormatRevisionAsync(FormatRevisionCreateRequest request)
        {
            return Ok(await _formatRevisionService.CreateNewFormatRevisionAsync(request));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<ActionResult<List<FormatRevisionResponse>>> DeleteFormatRevisionAsync(int id) 
        { 
            var result = await _formatRevisionService.DeleteFormatRevisionAsync(id);
            if (result == null)
            {
                return NotFound("FormatRevision not found");
            }
            return Ok(result);
        }
    }
}
