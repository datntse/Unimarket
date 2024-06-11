using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Unimarket.Core.Models;
using Unimarket.Infracstruture.Services;

namespace Unimarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost("CreateNoti")]
        public async Task<IActionResult> CreateNoti([FromBody] NotiDTO noti)
        {
            try
            {
                // Validate the input NotiDTO (you can use DataAnnotations or custom validation logic)
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Create a new notification based on the provided NotiDTO
                await _notificationService.CreateNoti(noti);

                return Ok("Notification created successfully!");
            }
            catch (Exception ex)
            {
                // Log the exception or perform any necessary error handling
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating notification: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var result = _notificationService.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotificationById(Guid id)
        {
            var notification = await _notificationService.FindAsync(id);

            if (notification == null)
            {
                return NotFound();
            }

            return Ok(notification);
        }
    }
}
