using Microsoft.AspNetCore.Mvc;

namespace DoYouOwnIt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles ="Admin, Moderator")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _productService.GetAllProducts());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound($"Product with {id} was not found.");
            }
            return Ok(product);
        }
        [AllowAnonymous]
        [HttpGet("slug/{slug}")]
        public async Task<ActionResult<StoreResponse>?> GetProductBySlugAsync(string slug)
        {
            var result = await _productService.GetProductBySlug(slug);
            if (result == null)
            {
                return NotFound($"Store with this slug {slug} was not found");
            }
            return Ok(result);
        }
        [Authorize(Roles = "Admin, Moderator, AlphaTester")]
        [HttpPost]
        public async Task<ActionResult<List<ProductResponse>>> CreateProduct(ProductCreateRequest product)
        {
            return Ok(await _productService.CreateProduct(product));
        }
        [Authorize(Roles = "Admin, Moderator, AlphaTester")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, ProductUpdateRequest product)
        {
            var updatedProduct = await _productService.UpdateProduct(id, product);
            if (updatedProduct == null)
            {
                return NotFound($"Product with {id} was not found.");
            }
            return Ok(updatedProduct);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deletedProduct = await _productService.DeleteProduct(id);
            if (deletedProduct == null)
            {
                return NotFound($"Product with {id} was not found.");
            }
            return Ok(deletedProduct);
        }
        [AllowAnonymous]
        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategoryId(int categoryId)
        {
            var products = await _productService.GetProductsByCategoryId(categoryId);
            if (products == null || !products.Any())
            {
                return NotFound($"No products found for category with ID {categoryId}.");
            }
            return Ok(products);
        }
        [AllowAnonymous]
        [HttpGet("Search/{categoryId}/{searchText}")]
        public async Task<IActionResult> SearchProducts(string searchText, int? categoryId, int pageNumber, int pageSize)
        {
            var products = await _productService.SearchProducts(searchText, categoryId, pageNumber, pageSize);
            return Ok(products);
        }
    }
}
