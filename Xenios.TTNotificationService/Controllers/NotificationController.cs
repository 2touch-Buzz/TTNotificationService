using Microsoft.AspNetCore.Mvc;
using Xenios.TTNotificationService.Models;

namespace Xenios.TTNotificationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly ILogger<NotificationController> _logger;

        public NotificationController(ILogger<NotificationController> logger)
        {
            _logger = logger;
        }

        [HttpPost("email")]
        public async Task<ActionResult<ApiResponse<object>>> SendEmailAsync([FromBody] SendEmailRequest request)
        {
            _logger.LogInformation("SendEmailAsync called with request: {@Request}", request);

            await Task.CompletedTask; // Simulate async operation

            var response = new ApiResponse<object>
            {
                Success = false,
                Message = "Not Implemented",
                Data = null
            };

            return StatusCode(501, response);
        }

        [HttpPost("text")]
        public async Task<ActionResult<ApiResponse<object>>> SendTextMessageAsync([FromBody] SendTextMessageRequest request)
        {
            _logger.LogInformation("SendTextMessageAsync called with request: {@Request}", request);

            await Task.CompletedTask; // Simulate async operation

            var response = new ApiResponse<object>
            {
                Success = false,
                Message = "Not Implemented",
                Data = null
            };

            return StatusCode(501, response);
        }
    }
}