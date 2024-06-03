using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Unimarket.API.Helper;
using Unimarket.API.Helpers;
using Unimarket.API.Services;
using Unimarket.Core.Models;
using Unimarket.Infracstruture.Services;

namespace Unimarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ICurrentUserService _currentUserService;
        public CartController(ICartService cartService,ICurrentUserService currentUserService)
        {
            _cartService = cartService;
            _currentUserService = currentUserService;
        }
        [HttpGet("get/usercart")]
        public async Task<IActionResult> GetCartItemsByUserId([FromQuery] DefaultSearch defaultSearch)
        {
            var userId = _currentUserService.GetUserId().ToString();
            if(userId == null)
            {
                NotFound("Need Login!!!!");
            }
            var cartItems = await _cartService.GetCartItemsByUserId(userId).Sort(string.IsNullOrEmpty(defaultSearch.sortBy) ? "Id" : defaultSearch.sortBy
                      , defaultSearch.isAscending)
                      .ToPageList(defaultSearch.currentPage, defaultSearch.perPage).AsNoTracking().ToListAsync();

            if (cartItems == null || !cartItems.Any())
            {
                return NotFound("No cart items found for this user.");
            }
            return Ok(cartItems);
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(AddItemDTO model)
        {
            var userId = _currentUserService.GetUserId().ToString();
            var result = await _cartService.AddToCart(userId, model);

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
            var userId = _currentUserService.GetUserId().ToString();
            if (userId == null)
            {
                NotFound("Need Login!!!!");
            }
            var result = await _cartService.UpdateItemQuantity(userId,model);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("add-quantity")]
        public async Task<IActionResult> AddQuantityToCart(UpdateItemQuantityDTO model)
        {
            var userId = _currentUserService.GetUserId().ToString();
            if (userId == null)
            {
                NotFound("Need Login!!!!");
            }
            var result = await _cartService.AddQuantityToCart(userId,model);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result.Errors);
        }
        [HttpDelete("delete-item-in-cart")]
        public async Task<IActionResult> DeleteItemInCart(AddItemDTO deleteItem)
        {
            var userId = _currentUserService.GetUserId().ToString();
            var result = await _cartService.DeleteItemInCart(userId,deleteItem);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result.Errors);
        }
    }
}
