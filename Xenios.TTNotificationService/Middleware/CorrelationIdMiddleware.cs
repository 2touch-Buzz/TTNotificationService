using Serilog.Context;

namespace Xenios.TTNotificationService.Middleware
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string correlationId = GetOrCreateCorrelationId(context);
            
            // Store in HttpContext
            context.Items["CorrelationId"] = correlationId;
            
            // Add to response headers
            context.Response.Headers.Append("X-Correlation-ID", correlationId);
            
            // Push to Serilog context
            using (LogContext.PushProperty("CorrelationId", correlationId))
            {
                await _next(context);
            }
        }

        private static string GetOrCreateCorrelationId(HttpContext context)
        {
            // Check for incoming correlation ID header
            if (context.Request.Headers.TryGetValue("X-Correlation-ID", out Microsoft.Extensions.Primitives.StringValues correlationIdValues))
            {
                string correlationId = correlationIdValues.FirstOrDefault() ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(correlationId) && Guid.TryParse(correlationId, out _))
                {
                    return correlationId;
                }
            }
            
            // Generate new GUID if none provided or invalid
            return Guid.NewGuid().ToString();
        }
    }
}