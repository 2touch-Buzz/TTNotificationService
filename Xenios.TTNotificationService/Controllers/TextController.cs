using Microsoft.AspNetCore.Mvc;
using Xenios.TTNotificationService.Models;

namespace Xenios.TTNotificationService.Controllers
{
    [ApiController]
    [Route("Text")]
    public class TextController : ControllerBase
    {
        private readonly ILogger<TextController> _logger;

        public TextController(ILogger<TextController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<object>>> SendTextAsync([FromBody] SendTextMessageRequest request)
        {
            _logger.LogInformation("SendTextAsync called with request: {@Request}", request);

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