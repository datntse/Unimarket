using Microsoft.AspNetCore.Mvc;
using Unimarket.Core.Models;
using Unimarket.Infracstruture.Services;

namespace Unimarket.API.Controllers
{
    [ApiController]
    [Route("Auth")]
    public class AuthenticateController : Controller
    {
        private readonly IUserService _userService;
        public AuthenticateController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("Sigin")]
        public async Task<IActionResult> SignUp(UserSignUp signUpModel)
        {
            var result = await _userService.SignUpAsync(signUpModel);
            if (result == null)
            {
                return BadRequest("Email is existed");
            }
            if (result.Succeeded)
            {
                return Ok(result.Succeeded);
            }

            return StatusCode(500);
        }
    }
}

