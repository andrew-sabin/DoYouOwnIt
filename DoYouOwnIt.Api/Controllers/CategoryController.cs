
using Microsoft.AspNetCore.Mvc;

namespace DoYouOwnIt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles ="Admin")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<CategoryResponse>>> GetAllCategories()
        {
            return Ok(await _categoryService.GetAllCategories());
        }
        [AllowAnonymous]
        [HttpGet("unlocked")]
        public async Task<ActionResult<List<CategoryResponse>>> GetAllUnlockedCategories()
        {
            return Ok(await _categoryService.GetAllUnlockedCategories());
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResponse>> GetCategoryById(int id)
        {
            var result = await _categoryService.GetCategoryById(id);
            if(result is null)
            {
                return NotFound($"Category with the ID of {id} was not found.");
            }
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("slug/{slug}")]
        public async Task<ActionResult<CategoryResponse>> GetCategoryBySlug(string slug)
        {
            var result = await _categoryService.GetCategoryBySlug(slug);
            if(result is null)
            {
                return NotFound($"Category with the slug of {slug} was not found.");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<CategoryResponse>>> CreateCategory(CategoryCreateRequest category)
        {
            return Ok(await _categoryService.CreateCategory(category));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<List<CategoryResponse>>> UpdateCategory(int id, CategoryUpdateRequest category) {
            var result = await _categoryService.UpdateCategory(id, category);
            if(result is null)
            {
                return NotFound("Category Not Found");
            }
            return Ok(result);
        }
        [HttpPatch("{id}/lock")]
        public async Task<ActionResult<List<CategoryResponse>>> LockCategory(int id, CategoryLockRequest request) {
            var result = await _categoryService.LockCategory(id, request);
            if (result is null)
            {
                return NotFound($"Category with ID of {id} was not found.");
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<CategoryResponse>>> DeleteCategory(int id) { 
            var result = await _categoryService.DeleteCategory(id);
            if (result is null)
            {
                return NotFound($"Category with ID of {id} was not found.");
            }
            return Ok(result);
        }
    }
}
