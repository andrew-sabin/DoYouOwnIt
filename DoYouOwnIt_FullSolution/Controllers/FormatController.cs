using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoYouOwnIt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles ="Admin, Moderator")]
    public class FormatController : ControllerBase
    {
        private readonly IFormatService _formatService;
        public FormatController(IFormatService formatService)
        {
            _formatService = formatService;
        }
        [HttpGet]
        public async Task<ActionResult<List<FormatResponse>>> GetAllFormatsAsync()
        {
            return Ok(await _formatService.GetAllFormatsAsync());
        }
        [AllowAnonymous]
        [HttpGet("product/{productId}")]
        public async Task<ActionResult<List<FormatResponse>>> GetFormatsByProductIdAsync(int productId)
        {
            var result = await _formatService.GetFormatsByProductIdAsync(productId);
            if (result is null || !result.Any())
            {
                return NotFound($"No formats found for product with ID {productId}.");
            }
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<FormatResponse>> GetFormatByIdAsync(int id)
        {

            var result = await _formatService.GetFormatByIdAsync(id);
            if (result is null)
            {
                return NotFound($"Format with the ID of {id} was not found.");
            }
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("product/{prodSlug}/slug/{slug}")]
        public async Task<ActionResult<StoreResponse>?> GetFormatBySlugAsync(string prodSlug, string slug)
        {
            var result = await _formatService.GetFormatBySlugAsync(prodSlug, slug);
            if (result == null)
            {
                return NotFound($"Format with this slug {slug} was not found in {prodSlug}");
            }
            return Ok(result);
        }
        [Authorize(Roles ="Admin, Moderator, AlphaTester")]
        [HttpPost]
        public async Task<ActionResult<List<FormatResponse>>> CreateFormat(FormatCreateRequest format)
        {
            return Ok(await _formatService.CreateFormatAsync(format));
        }
        [Authorize(Roles = "Admin, Moderator, AlphaTester")]
        [HttpPut("{id}")]
        public async Task<ActionResult<List<FormatResponse>>> UpdateFormatAsync(int id, FormatUpdateRequest format)
        {
            var result = await _formatService.UpdateFormatAsync(id, format);
            if (result is null)
            {
                return NotFound($"Format with ID {id} Not Found");
            }
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<FormatResponse>>> DeleteFormatAsync(int id)
        {
            var result = await _formatService.DeleteFormatAsync(id);
            if (result is null)
            {
                return NotFound($"Format with ID of {id} was not found.");
            }
            return Ok(result);
        }
    }
}
