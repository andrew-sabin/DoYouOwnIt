
using Microsoft.AspNetCore.Mvc;

namespace DoYouOwnIt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles ="Admin")]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _categoryService;

        public ProductCategoryController(IProductCategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<ProductCategoryResponse>>> GetAllCategories()
        {
            return Ok(await _categoryService.GetAllCategories());
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategoryResponse>> GetCategoryById(int id)
        {
            var result = await _categoryService.GetProductCategoryById(id);
            if(result is null)
            {
                return NotFound($"Category with the ID of {id} was not found.");
            }
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("slug/{slug}")]
        public async Task<ActionResult<ProductCategoryResponse>> GetCategoryBySlug(string slug)
        {
            var result = await _categoryService.GetProductCategoryBySlug(slug);
            if(result is null)
            {
                return NotFound($"Category with the slug of {slug} was not found.");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<ProductCategoryResponse>>> CreateCategory(ProductCategoryCreateRequest category)
        {
            return Ok(await _categoryService.CreateCategory(category));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<List<ProductCategoryResponse>>> UpdateCategory(int id, ProductCategoryUpdateRequest category) {
            var result = await _categoryService.UpdateCategory(id, category);
            if(result is null)
            {
                return NotFound("Category Not Found");
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<ProductCategoryResponse>>> DeleteCategory(int id) { 
            var result = await _categoryService.DeleteCategory(id);
            if (result is null)
            {
                return NotFound($"Category with ID of {id} was not found.");
            }
            return Ok(result);
        }
    }
}
