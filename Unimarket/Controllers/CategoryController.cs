using Microsoft.AspNetCore.Mvc;
using Unimarket.Infracstruture.Services;

namespace Unimarket.API.Controllers
{
    [ApiController]
    [Route("Category")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var result = _categoryService.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }  
        }
    }
}
