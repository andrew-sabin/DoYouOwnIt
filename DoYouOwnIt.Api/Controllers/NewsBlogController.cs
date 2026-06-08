using DoYouOwnIt_Shared.Models.NewsUpdate;
using DoYouOwnIt.Api.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DoYouOwnIt_Shared.Models.NewsBlog;

namespace DoYouOwnIt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles="Admin")]
    public class NewsBlogController : ControllerBase
    {
        private readonly INewsBlogService _newsBlogService;
        
        public NewsBlogController(INewsBlogService newsBlogService)
        {
            _newsBlogService = newsBlogService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<NewsBlogResponse>>> GetAllNewsBlogs()
        {
            return Ok(await _newsBlogService.GetAllNewsBlogs());
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<NewsBlogResponse>> GetNewsBlogByIdAsync(int id)
        {
            var result = await _newsBlogService.GetNewsBlogByIdAsync(id);
            if(result == null)
                return NotFound();
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("slug/{slug}")]
        public async Task<ActionResult<NewsBlogResponse>> GetNewsBlogBySlugAsync(string slug) 
        {
            var result = await _newsBlogService.GetNewsBlogBySlugAsync(slug);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("type/{type}")]
        public async Task<ActionResult<NewsBlogResponse?>> GetFirstBlogByType(string type)
        {
            var result = await _newsBlogService.GetFirstBlogByType(type);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("latest/{type}")]
        public async Task<ActionResult<List<NewsBlogResponse>>> GetLastestBlogsByType(string type, int amnt, bool sticky) { 
            var result = await _newsBlogService.GetLastestBlogsByType(type, amnt, sticky);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("sticky")]
        public async Task<ActionResult<List<NewsBlogResponse>>> GetLastestStickyBlogs(int amnt, bool sticky)
        {
            var result = await _newsBlogService.GetLatestStickyBlogs(amnt, sticky);
            return Ok(result);
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<ActionResult<NewsBlogResponse>> CreateNewsBlogAsync(NewsBlogCreateRequest request)
        {
            return Ok(await _newsBlogService.CreateNewsBlogAsync(request));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<NewsBlogResponse>> UpdateNewsBlogAsync(int id, NewsBlogUpdateRequest request)
        {
            var result = await _newsBlogService.UpdateNewsBlogAsync(id, request);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<NewsBlogResponse>>> DeleteNewsBlogAsync(int id) 
        { 
            var result = await _newsBlogService.DeleteFormatAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

    }
}
