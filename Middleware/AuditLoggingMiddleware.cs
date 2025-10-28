using Microsoft.Extensions.Logging;

namespace InventoryApi.Middleware
{
    public class AuditLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuditLoggingMiddleware> _logger;

        public AuditLoggingMiddleware(RequestDelegate next, ILogger<AuditLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log request (e.g., for audit trails - FR5)
            _logger.LogInformation("Request: {Method} {Path}", context.Request.Method, context.Request.Path);

            await _next(context);  // Pass to next middleware
        }
    }
}