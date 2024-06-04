using Microsoft.AspNetCore.Mvc;
using Unimarket.Core.Entities;
using Unimarket.Core.Models;
using Unimarket.Infracstruture.Services;

namespace Unimarket.API.Controllers
{
    [ApiController]
    [Route("api/category")]
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

        [HttpGet("id")]
        public async Task<IActionResult> GetCateById(string id)
        {
            try
            {
                var result = await _categoryService.FindAsync(Guid.Parse(id));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(String categoryName)
        {
            try
            {
                Category cate = new Category
                {
                    Id = Guid.NewGuid(),
                    Name = categoryName,
                    CreateAt = DateTime.Now,
                    Type = 1,
                };
                await _categoryService.AddAsync(cate);
                await _categoryService.SaveChangeAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryUM categoryUM)
        {
            try
            {
                var result = await _categoryService.FindAsync(categoryUM.Id);
                result.Name = categoryUM.Name;
                 _categoryService.Update(result);
                await _categoryService.SaveChangeAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
