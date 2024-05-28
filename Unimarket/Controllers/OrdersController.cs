using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Unimarket.Core.Models;
using Unimarket.Infracstruture.Services;

namespace Unimarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> CheckOut([FromBody] CheckOutDTO checkOutDTO)
        {
            var paymentUrl = await _orderService.CheckOut(checkOutDTO);

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
