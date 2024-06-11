using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Unimarket.API.Helper;
using Unimarket.API.Helpers;
using Unimarket.API.Services;
using Unimarket.Core.Constants;
using Unimarket.Core.Models;
using Unimarket.Infracstruture.Services;

namespace Unimarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = AppRole.Customer)]
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
        public async Task<IActionResult> GetCartItemsByUserId([FromQuery] DefaultSearch defaultSearch, [FromQuery]string userId)
        {
            //var userId = _currentUserService.GetUserId().ToString();
            //if(userId == null)
            //{
            //    NotFound("Need Login!!!!");
            //}
            var cartItems = await _cartService.GetCartItemsByUserId(userId).Sort(string.IsNullOrEmpty(defaultSearch.sortBy) ? "Id" : defaultSearch.sortBy
                      , defaultSearch.isAscending)
                      .ToPageList(defaultSearch.currentPage, defaultSearch.perPage).AsNoTracking().ToListAsync();

            if (cartItems == null || !cartItems.Any())
            {
                return NotFound("No cart items found for this user.");
            }
            return Ok(new { total = cartItems.Count(), data = cartItems, currenPage = defaultSearch.currentPage });
		}
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddItemDTO addItemDTO)
        {
            var result = await _cartService.AddToCart(addItemDTO);

            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result.Errors);
        }

        [HttpPost("descrease")]
        public async Task<IActionResult> RemoveQuantity([FromBody] AddItemDTO addItemDTO)
        {
            var result = await _cartService.DecreaseQuantity(addItemDTO);

            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result.Errors);
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateItemQuantity([FromBody] UpdateItemQuantityDTO model)
        {
            var userId = _currentUserService.GetUserId().ToString();
            if (userId == null)
            {
                NotFound("Need Login!!!!");
            }
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
            if (model.UserId == null)
            {
                NotFound("Need Login!!!!");
            }
            var result = await _cartService.AddQuantityToCart(model);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result.Errors);
        }
        [HttpDelete("delete-item-in-cart")]
        public async Task<IActionResult> DeleteItemInCart([FromBody] AddItemDTO deleteItem)
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
