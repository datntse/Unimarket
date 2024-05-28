using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Unimarket.Infracstruture.Services;

namespace Unimarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("create-payment")]
        public IActionResult CreatePayment(float amount,string orderDescription, string locale)
        {
            string paymentUrl = _paymentService.CreatePaymentUrl(amount, orderDescription, locale);
            return Ok(new { PaymentUrl = paymentUrl });
        }
    }
}
