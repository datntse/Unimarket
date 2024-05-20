using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Unimarket.Core.Models;
using Unimarket.Infracstruture.Services;

namespace Unimarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(AddItemDTO model)
        {
            var result = await _cartService.AddToCart(model);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result.Errors);
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateItemQuantity(UpdateItemQuantityDTO model)
        {
            var result = await _cartService.UpdateItemQuantity(model);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result.Errors);
        }
    }
}
