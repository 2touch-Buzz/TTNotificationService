using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xenios.TTNotificationService.Controllers;
using Xenios.TTNotificationService.Models;

namespace Xenios.TTNotificationService.Tests
{
    public class EmailControllerTests
    {
        private readonly Mock<ILogger<EmailController>> _mockLogger;
        private readonly EmailController _controller;

        public EmailControllerTests()
        {
            _mockLogger = new Mock<ILogger<EmailController>>();
            _controller = new EmailController(_mockLogger.Object);
        }

        [Fact]
        public async Task SendEmailAsync_ReturnsNotImplemented()
        {
            // Arrange
            SendEmailRequest request = new SendEmailRequest
            {
                To = "test@example.com",
                Subject = "Test Subject",
                Body = "Test Body"
            };

            // Act
            ActionResult<ApiResponse<object>> result = await _controller.SendEmailAsync(request);

            // Assert
            ObjectResult statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(501, statusCodeResult.StatusCode);
            
            ApiResponse<object> response = Assert.IsType<ApiResponse<object>>(statusCodeResult.Value);
            Assert.False(response.Success);
            Assert.Equal("Not Implemented", response.Message);
            Assert.Null(response.Data);
        }

        [Fact]
        public async Task SendEmailAsync_LogsRequest()
        {
            // Arrange
            SendEmailRequest request = new SendEmailRequest
            {
                To = "test@example.com",
                Subject = "Test Subject",
                Body = "Test Body"
            };

            // Act
            await _controller.SendEmailAsync(request);

            // Assert
            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("SendEmailAsync called with request")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }
    }
}