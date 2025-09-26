using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoYouOwnIt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin, Moderator")]
    public class AvailabilityController : ControllerBase
    {
        private readonly IAvailabilityService _availabilityService;

        public AvailabilityController(IAvailabilityService availabilityService)
        {
            _availabilityService = availabilityService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AvailabilityResponse>>> GetAvailability()
        {
            return Ok(await _availabilityService.GetAllAvailabilitiesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AvailabilityResponse?>> GetAvailabilityById(int id)
        {
            var availability = await _availabilityService.GetAvailabilityByIdAsync(id);
            if (availability == null)
            {
                return NotFound($"Availability with ID {id} was not found.");
            }
            return Ok(availability);
        }
        [AllowAnonymous]
        [HttpGet("format/{formatId}")]
        public async Task<ActionResult<List<AvailabilityResponse>?>> GetAvailabilitiesByFormatId(int formatId)
        {
            var availabilities = await _availabilityService.GetAvailabilitiesByFormatIdAsync(formatId);
            if (availabilities == null || !availabilities.Any())
            {
                return NotFound($"No availabilities found for format ID {formatId}.");
            }
            return Ok(availabilities);
        }
        [AllowAnonymous]
        [HttpGet("store/{storeId}")]
        public async Task<ActionResult<List<AvailabilityResponse>?>> GetAvailabilitiesByStoreId(int storeId)
        {
            var availabilities = await _availabilityService.GetAvailabilitiesByStoreIdAsync(storeId);
            if (availabilities == null || !availabilities.Any())
            {
                return NotFound($"No availabilities found for store ID {storeId}.");
            }
            return Ok(availabilities);
        }

        [HttpPost]
        public async Task<ActionResult<List<AvailabilityResponse>>> CreateAvailability(AvailabilityCreateRequest availability)
        {
            return Ok(await _availabilityService.CreateAvailabilityAsync(availability));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AvailabilityResponse?>> UpdateAvailability(int id, AvailabilityUpdateRequest availability)
        {
            var updatedAvailability = await _availabilityService.UpdateAvailabilityAsync(id, availability);
            if (updatedAvailability == null)
            {
                return NotFound($"Availability with ID {id} was not found.");
            }
            return Ok(updatedAvailability);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<AvailabilityResponse>?>> DeleteAvailability(int id)
        {
            var deletedAvailabilities = await _availabilityService.DeleteAvailabilityAsync(id);
            if (deletedAvailabilities == null)
            {
                return NotFound($"Availability with ID {id} was not found.");
            }
            return Ok(deletedAvailabilities);
        }
    }
}
