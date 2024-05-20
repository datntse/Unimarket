using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Unimarket.Core.Models;
using Unimarket.Infracstruture.Services;

namespace Unimarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CheckOut(CheckOutDTO checkOutDTO)
        {
            try
            {
                var result = await _orderService.CheckOut(checkOutDTO);
                return Ok(result); // Assuming the result is an IdentityResult or similar
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
