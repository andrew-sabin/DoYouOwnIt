using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoYouOwnIt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles ="Admin, Moderator, AlphaTester")]
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
        [HttpGet("recent")]
        public async Task<ActionResult<List<FormatResponse>>> GetRecentFormatsAsync(int amount)
        {
            var result = await _formatService.GetRecentFormatsAsync(amount);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("recent/updated")]
        public async Task<ActionResult<List<FormatResponse>>> GetRecentlyUpdatedFormatsAsync(int amount) 
        {
            var result = await _formatService.GetRecentlyUpdatedFormatsAsync(amount);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin/product/{productId}")]
        public async Task<ActionResult<List<FormatResponse>>> GetFormatsByProductIdAdminAsync(int productId)
        {
            var result = await _formatService.GetFormatsByProductIdAdminAsync(productId);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("product/{productId}")]
        public async Task<ActionResult<List<FormatResponse>>> GetFormatsByProductIdAsync(int productId)
        {
            var result = await _formatService.GetFormatsByProductIdAsync(productId);
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

        [Authorize(Roles = "Admin")]
        [HttpGet("admin/{id}")]
        public async Task<ActionResult<FormatResponse>> GetFormatByIdAdminAsync(int id)
        {

            var result = await _formatService.GetFormatByIdAdminAsync(id);
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

        [Authorize(Roles = "Admin")]
        [HttpGet("admin/product/{prodSlug}/slug/{slug}")]
        public async Task<ActionResult<StoreResponse>?> GetFormatBySlugAdminAsync(string prodSlug, string slug)
        {
            var result = await _formatService.GetFormatBySlugAdminAsync(prodSlug, slug);
            if (result == null)
            {
                return NotFound($"Format with this slug {slug} was not found in {prodSlug}");
            }
            return Ok(result);
        }

        [Authorize(Roles ="Admin, Moderator, AlphaTester")]
        [HttpPost]
        public async Task<ActionResult<FormatResponse>> CreateFormat(FormatCreateRequest format)
        {
            var response = await _formatService.CreateFormatAsync(format);
            return Ok(response);
        }
        [Authorize(Roles = "Admin, Moderator, AlphaTester")]
        [HttpPut("{id}")]
        public async Task<ActionResult<FormatResponse>> UpdateFormatAsync(int id, FormatUpdateRequest format)
        {
            var result = await _formatService.UpdateFormatAsync(id, format);
            if (result is null)
            {
                return NotFound($"Format with ID {id} Not Found");
            }
            return Ok(result);
        }
        [Authorize(Roles = "Admin, Moderator")]
        [HttpPatch("lock/{id}")]
        public async Task<ActionResult<FormatResponse>> LockFormatAsync(int id, FormatLockRequest format)
        {
            var result = await _formatService.LockFormatAsync(id, format);
            if (result is null)
            {
                return NotFound($"Format with ID {id} Not Found.");
            }
            return Ok(result);
        }
        [Authorize(Roles = "Admin, Moderator")]
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
