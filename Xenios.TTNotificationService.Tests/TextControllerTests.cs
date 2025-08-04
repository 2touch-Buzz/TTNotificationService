using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xenios.TTNotificationService.Controllers;
using Xenios.TTNotificationService.Models;

namespace Xenios.TTNotificationService.Tests
{
    public class TextControllerTests
    {
        private readonly Mock<ILogger<TextController>> _mockLogger;
        private readonly TextController _controller;

        public TextControllerTests()
        {
            _mockLogger = new Mock<ILogger<TextController>>();
            _controller = new TextController(_mockLogger.Object);
        }

        [Fact]
        public async Task SendTextAsync_ReturnsNotImplemented()
        {
            // Arrange
            SendTextMessageRequest request = new SendTextMessageRequest
            {
                PhoneNumber = "+1234567890",
                Message = "Test Message"
            };

            // Act
            ActionResult<ApiResponse<object>> result = await _controller.SendTextAsync(request);

            // Assert
            ObjectResult statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(501, statusCodeResult.StatusCode);
            
            ApiResponse<object> response = Assert.IsType<ApiResponse<object>>(statusCodeResult.Value);
            Assert.False(response.Success);
            Assert.Equal("Not Implemented", response.Message);
            Assert.Null(response.Data);
        }

        [Fact]
        public async Task SendTextAsync_LogsRequest()
        {
            // Arrange
            SendTextMessageRequest request = new SendTextMessageRequest
            {
                PhoneNumber = "+1234567890",
                Message = "Test Message"
            };

            // Act
            await _controller.SendTextAsync(request);

            // Assert
            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("SendTextAsync called with request")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }
    }
}