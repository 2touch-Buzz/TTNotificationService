using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xenios.TTNotificationService.Controllers;
using Xenios.TTNotificationService.Models;

namespace Xenios.TTNotificationService.Tests
{
    public class NotificationControllerTests
    {
        private readonly Mock<ILogger<NotificationController>> _mockLogger;
        private readonly NotificationController _controller;

        public NotificationControllerTests()
        {
            _mockLogger = new Mock<ILogger<NotificationController>>();
            _controller = new NotificationController(_mockLogger.Object);
        }

        [Fact]
        public async Task SendEmailAsync_ReturnsNotImplemented()
        {
            // Arrange
            var request = new SendEmailRequest
            {
                To = "test@example.com",
                Subject = "Test Subject",
                Body = "Test Body"
            };

            // Act
            var result = await _controller.SendEmailAsync(request);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(501, statusCodeResult.StatusCode);
            
            var response = Assert.IsType<ApiResponse<object>>(statusCodeResult.Value);
            Assert.False(response.Success);
            Assert.Equal("Not Implemented", response.Message);
            Assert.Null(response.Data);
        }

        [Fact]
        public async Task SendTextMessageAsync_ReturnsNotImplemented()
        {
            // Arrange
            var request = new SendTextMessageRequest
            {
                PhoneNumber = "+1234567890",
                Message = "Test Message"
            };

            // Act
            var result = await _controller.SendTextMessageAsync(request);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(501, statusCodeResult.StatusCode);
            
            var response = Assert.IsType<ApiResponse<object>>(statusCodeResult.Value);
            Assert.False(response.Success);
            Assert.Equal("Not Implemented", response.Message);
            Assert.Null(response.Data);
        }

        [Fact]
        public async Task SendEmailAsync_LogsRequest()
        {
            // Arrange
            var request = new SendEmailRequest
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

        [Fact]
        public async Task SendTextMessageAsync_LogsRequest()
        {
            // Arrange
            var request = new SendTextMessageRequest
            {
                PhoneNumber = "+1234567890",
                Message = "Test Message"
            };

            // Act
            await _controller.SendTextMessageAsync(request);

            // Assert
            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString()!.Contains("SendTextMessageAsync called with request")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }
    }
}