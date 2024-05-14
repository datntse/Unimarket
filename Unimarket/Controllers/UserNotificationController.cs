using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Unimarket.Core.Models;
using Unimarket.Infracstruture.Services;

namespace Unimarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserNotificationController : ControllerBase
    {
        public readonly IUserNotificationService _userNotificationService;
        public UserNotificationController(IUserNotificationService userNotificationService)
        {
            _userNotificationService = userNotificationService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUserNoti(UserNotiCM model)
        {
            var result = await _userNotificationService.CreateUserNoti(model);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            return BadRequest(result.Errors);
        }
    }
}
