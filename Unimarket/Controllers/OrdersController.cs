using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Unimarket.API.Helper;
using Unimarket.API.Helpers;
using Unimarket.API.Services;
using Unimarket.Core.Models;
using Unimarket.Infracstruture.Services;

namespace Unimarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ICurrentUserService _currentUserService;

        public OrderController(IOrderService orderService, ICurrentUserService currentUserService)
        {
            _orderService = orderService;
            _currentUserService = currentUserService;
        }
        [HttpGet("user")]
        [Authorize] // This attribute is optional, use it if you want to restrict access to authenticated users
        public async Task<IActionResult> GetOrderByUserId([FromQuery] DefaultSearch defaultSearch)
        {
            var userId = _currentUserService.GetUserId().ToString();
            if (userId == null)
            {
                NotFound("Need Login!!!!");
            }
            var orderVM = await _orderService.GetOrdersByUserId(userId).Sort(string.IsNullOrEmpty(defaultSearch.sortBy) ? "" : defaultSearch.sortBy
                      , defaultSearch.isAscending)
                      .ToPageList(defaultSearch.currentPage, defaultSearch.perPage).AsNoTracking().ToListAsync();
            if (orderVM == null)
            {
                return NotFound();
            }

            return Ok(orderVM);
        }
        [HttpPost("checkout")]
        public async Task<IActionResult> CheckOut([FromBody] CheckOutDTO checkOutDTO)
        {
            var userId = _currentUserService.GetUserId().ToString();
            if (userId == null)
            {
                NotFound("Need Login!!!!");
            }
            var paymentUrl = await _orderService.CheckOut(userId, checkOutDTO);

            if (!string.IsNullOrEmpty(paymentUrl))
            {
                return Ok(new { success = true, paymentUrl });
            }

            return Ok(new { success = true });
        }

        [HttpGet("vnpay-return")]
        public async Task<IActionResult> ConfirmVnPayPayment()
        {
            var result = await _orderService.ConfirmVnPayPayment(Request.Query);
            if (result.Succeeded)
            {
                return Ok(new { success = true, message = "Payment successful" });
            }
            return BadRequest(new { success = false, errors = result.Errors });
        }
    }

}
