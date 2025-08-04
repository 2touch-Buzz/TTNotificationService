using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Xenios.TTNotificationService.Middleware;

namespace Xenios.TTNotificationService.Tests
{
    public class CorrelationIdMiddlewareTests
    {
        [Fact]
        public async Task InvokeAsync_GeneratesNewCorrelationId_WhenNoneProvided()
        {
            // Arrange
            var context = new DefaultHttpContext();
            var next = new Mock<RequestDelegate>();
            var middleware = new CorrelationIdMiddleware(next.Object);

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            Assert.True(context.Items.ContainsKey("CorrelationId"));
            Assert.True(Guid.TryParse(context.Items["CorrelationId"]?.ToString(), out _));
            Assert.True(context.Response.Headers.ContainsKey("X-Correlation-ID"));
            next.Verify(x => x(context), Times.Once);
        }

        [Fact]
        public async Task InvokeAsync_UsesProvidedCorrelationId_WhenValid()
        {
            // Arrange
            var correlationId = Guid.NewGuid().ToString();
            var context = new DefaultHttpContext();
            context.Request.Headers["X-Correlation-ID"] = correlationId;
            var next = new Mock<RequestDelegate>();
            var middleware = new CorrelationIdMiddleware(next.Object);

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            Assert.Equal(correlationId, context.Items["CorrelationId"]);
            Assert.Equal(correlationId, context.Response.Headers["X-Correlation-ID"]);
            next.Verify(x => x(context), Times.Once);
        }

        [Fact]
        public async Task InvokeAsync_GeneratesNewCorrelationId_WhenProvidedIdIsInvalid()
        {
            // Arrange
            var invalidCorrelationId = "invalid-guid";
            var context = new DefaultHttpContext();
            context.Request.Headers["X-Correlation-ID"] = invalidCorrelationId;
            var next = new Mock<RequestDelegate>();
            var middleware = new CorrelationIdMiddleware(next.Object);

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            Assert.NotEqual(invalidCorrelationId, context.Items["CorrelationId"]);
            Assert.True(Guid.TryParse(context.Items["CorrelationId"]?.ToString(), out _));
            next.Verify(x => x(context), Times.Once);
        }
    }
}