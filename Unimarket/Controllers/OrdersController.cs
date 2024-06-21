using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
    //[Authorize(Roles = AppRole.Customer)]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ICurrentUserService _currentUserService;

        public OrderController(IOrderService orderService, ICurrentUserService currentUserService)
        {
            _orderService = orderService;
            _currentUserService = currentUserService;
        }
        //[Authorize(Roles = AppRole.Staff)]
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll([FromQuery] DefaultSearch defaultSearch)
        {
            var orderVM = await _orderService.GetAll().Sort(string.IsNullOrEmpty(defaultSearch.sortBy) ? "Id" : defaultSearch.sortBy
                      , defaultSearch.isAscending)
                      .ToPageList(defaultSearch.currentPage, defaultSearch.perPage).AsNoTracking().ToListAsync();
            if (orderVM == null)
            {
                return NotFound();
            }

            return Ok(new { total = orderVM.Count(), data = orderVM, currenPage = defaultSearch.currentPage });
        }

        //[Authorize(Roles = AppRole.Customer)]
        [HttpGet("user")]
        public async Task<IActionResult> GetOrderByUserId([FromQuery] DefaultSearch defaultSearch, [FromQuery] string userId)
        {
            if (userId == null)
            {
                NotFound("Need Login!!!!");
            }
            var orderVM = await _orderService.GetOrdersByUserId(userId).Sort(string.IsNullOrEmpty(defaultSearch.sortBy) ? "Id" : defaultSearch.sortBy
                      , defaultSearch.isAscending)
                      .ToPageList(defaultSearch.currentPage, defaultSearch.perPage).AsNoTracking().ToListAsync();
            if (orderVM == null)
            {
                return NotFound();
            }

            return Ok(new { total = orderVM.Count(), data = orderVM, currenPage = defaultSearch.currentPage });
        }

        //[Authorize(Roles = AppRole.Customer)]
        [HttpPut("update/order")]
        public async Task<IActionResult> UpdateStatus(UpdateOrderUM upOrder)
        {
            var status = await _orderService.UpdateOrder(upOrder);
			if (status.Succeeded)
			{
				return Ok(new { success = true, message = "Payment successful" });
			}
			return BadRequest(new { success = false, errors = status.Errors });
		}
        //[Authorize(Roles = AppRole.Customer)]
        [HttpPost("checkout")]
        public async Task<IActionResult> CheckOut([FromBody] CheckOutDTO checkOutDTO)
        {
            //var userId = _currentUserService.GetUserId().ToString();
            //if (userId == null)
            //{
            //    NotFound("Need Login!!!!");
            //}
            var paymentUrl = await _orderService.CheckOut(checkOutDTO);

            if (!string.IsNullOrEmpty(paymentUrl))
            {
                return Ok(new { success = true, paymentUrl });
            }

            return Ok(new { success = true });
        }
        [Authorize(Roles = AppRole.Customer)]
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
