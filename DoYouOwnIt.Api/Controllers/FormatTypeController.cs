using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DoYouOwnIt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, Moderator, AlphaTester")]
    public class FormatTypeController : ControllerBase
    {
        readonly IFormatTypeService _formatTypeService;
        public FormatTypeController(IFormatTypeService formatTypeService)
        {
            _formatTypeService = formatTypeService;
        }
        [HttpGet]
        public async Task<ActionResult<List<FormatTypeResponse>>> GetAllFormatTypes()
        {
            return Ok(await _formatTypeService.GetAllFormatTypesAsync());
        }

        [Authorize(Roles ="Admin, Moderator, AlphaTester")]
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<List<FormatTypeResponse>>> GetFormatTypesByCategoryId(int categoryId) 
        { 
            return Ok(await _formatTypeService.GetFormatTypesByCategoryId(categoryId));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<FormatTypeResponse>> GetFormatTypeById(int id)
        {
            var result = await _formatTypeService.GetFormatTypeByIdAsync(id);
            if (result is null)
            {
                return NotFound($"Format Type with ID {id} was not found.");
            }
            return Ok(await _formatTypeService.GetFormatTypeByIdAsync(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateFormatType(FormatTypeCreateRequest formatType)
        {
            return Ok(await _formatTypeService.CreateFormatTypeAsync(formatType));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<FormatTypeResponse>> UpdateFormatType(FormatTypeUpdateRequest formatType, int id)
        {
            var result = await _formatTypeService.UpdateFormatTypeAsync(formatType, id);
            if (result is null)
            {
                return NotFound($"Format Type with ID {id} was not found.");
            }
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<FormatTypeResponse>> DeleteFormatType(int id)
        {
            var result = await _formatTypeService.DeleteFormatTypeByIdAsync(id);
            if (result is null)
            {
                return NotFound($"Format Type with ID {id} was not found.");
            }
            return Ok(result);
        }
    }
}
