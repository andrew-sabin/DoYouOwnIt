using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoYouOwnIt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles ="Admin, Moderator")]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;
        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<StoreResponse>>> GetAllStoresAsync()
        {
            return Ok(await _storeService.GetAllStoresAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StoreResponse>?> GetStoreByIdAsync(int id)
        {
            var result = await _storeService.GetStoreByIdAsync(id);
            if (result == null)
            {
                return NotFound($"Store or service with this ID {id} was not found");
            }
            return Ok(result);

        }
        [HttpGet("slug/{slug}")]
        public async Task<ActionResult<StoreResponse>?> GetStoreBySlugAsync(string slug)
        {
            var result = await _storeService.GetStoreBySlugAsync(slug);
            if (result == null)
            {
                return NotFound($"Store with this slug {slug} was not found");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<StoreResponse>>> CreateStoreAsync(StoreCreateRequest store)
        {
            return Ok(await _storeService.CreateStoreAsync(store));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<StoreResponse>> UpdateStoreAsync(int id, StoreUpdateRequest store)
        {
            var result = await _storeService.UpdateStoreAsync(id, store);
            if (result is null)
                return NotFound($"Store or service with this ID {id} was not found");
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<StoreResponse>>> DeleteStoreAsync(int id)
        {
            var result = await _storeService.DeleteStoreAsync(id);
            if (result is null)
                return NotFound($"Store or service with this ID {id} was not found");

            return Ok(result);
        }
    }
}
