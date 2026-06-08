using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace DoYouOwnIt.Api.Controllers
{
    [Route("api/Product")]
    [ApiController]
    [Authorize (Roles ="Admin, Moderator, AlphaTester")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _productService.GetAllProducts());
        }
        [AllowAnonymous]
        [HttpGet("users/{creatorId}")]
        public async Task<IActionResult> GetProductsByCreatorID(string creatorId)
        {
            var products = await _productService.GetProductsByCreatorId(creatorId);
            if (products == null || !products.Any())
            {
                return NotFound($"Not products found with creator id: {creatorId}");
            }
            return Ok(products);
        }
        [AllowAnonymous]
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
        [Authorize(Roles ="Admin, Moderator")]
        [HttpPatch("lock/{id}")]
        public async Task<ActionResult> LockProduct(int id, ProductLockRequest product)
        {
            var lockedProduct = await _productService.LockProduct(id, product);
            if (lockedProduct== null)
            {
                return NotFound($"Product with ID {id} was not found.");
            }
            return Ok(lockedProduct);
        }
        [Authorize(Roles = "Admin, Moderator")]
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

namespace DoYouOwnIt.OData.Controllers
{
    public class ProductController : ODataController
    {
        private readonly DataContext _dbContext;

        public ProductController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Filter | AllowedQueryOptions.Top | AllowedQueryOptions.Count 
            | AllowedQueryOptions.OrderBy | AllowedQueryOptions.Expand | AllowedQueryOptions.Select)]
        public IActionResult Get()
        {
            return Ok(_dbContext.Products);
        }
    }
}
