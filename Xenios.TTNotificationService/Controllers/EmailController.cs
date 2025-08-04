using Microsoft.AspNetCore.Mvc;
using Xenios.TTNotificationService.Models;

namespace Xenios.TTNotificationService.Controllers
{
    [ApiController]
    [Route("Email")]
    public class EmailController : ControllerBase
    {
        private readonly ILogger<EmailController> _logger;

        public EmailController(ILogger<EmailController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<object>>> SendEmailAsync([FromBody] SendEmailRequest request)
        {
            _logger.LogInformation("SendEmailAsync called with request: {@Request}", request);

            await Task.CompletedTask; // Simulate async operation

            ApiResponse<object> response = new ApiResponse<object>
            {
                Success = false,
                Message = "Not Implemented",
                Data = null
            };

            return StatusCode(501, response);
        }
    }
}