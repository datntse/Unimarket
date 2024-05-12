using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Unimarket.API.Services;
using Unimarket.Core.Models;
using Unimarket.Infracstruture.Services;

namespace Unimarket.API.Controllers
{
    [ApiController]
    [Route("Auth")]
    public class AuthenticateController : Controller
    {
        private readonly IUserService _userService;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthenticateController(IJwtTokenService jwtTokenService, IUserService userService)
        {
            _jwtTokenService = jwtTokenService;
            _userService = userService;


        }
        [HttpPost("SigUp")]
        public async Task<IActionResult> SignUp(UserDTO signUpModel)
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

        [HttpPost("SigIn")]
        public async Task<IActionResult> SignIn(UserSignIn signIn)
        {
            var user = await _userService.SignInAsync(signIn);
            if (user == null || !(user.Status != 0))
            {
                return Unauthorized();
            }
            var userRoles = await _userService.GetRolesAsync(user);
            var accessToken = _jwtTokenService.CreateToken(user, userRoles);
            var refreshToken = _jwtTokenService.CreateRefeshToken();
            user.RefreshToken = refreshToken;
            user.DateExpireRefreshToken = DateTime.Now.AddDays(7);
            _userService.Update(user);
            var result = await _userService.SaveChangeAsync();
            if (result)
            {
                return Ok(new { token = accessToken, refreshToken });

            }
            return BadRequest("Failed to update user's token");
        }
    }
}

