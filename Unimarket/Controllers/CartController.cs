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
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCartItemsByUserId(string userId)
        {
            var cartItems = await _cartService.GetCartItemsByUserId(userId);
            if (cartItems == null || !cartItems.Any())
            {
                return NotFound("No cart items found for this user.");
            }
            return Ok(cartItems);
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

        [HttpPost("add-quantity")]
        public async Task<IActionResult> AddQuantityToCart(UpdateItemQuantityDTO model)
        {
            var result = await _cartService.AddQuantityToCart(model);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result.Errors);
        }
        [HttpDelete("delete-item-in-cart")]
        public async Task<IActionResult> DeleteItemInCart(AddItemDTO deleteItem)
        {
            var result = await _cartService.DeleteItemInCart(deleteItem);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result.Errors);
        }
    }
}
