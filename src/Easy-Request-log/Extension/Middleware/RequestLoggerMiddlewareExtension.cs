using Easy_Request_log.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Easy_Request_log.Extension.Middleware
{
    public static class RequestLoggerMiddlewareExtension
    {
        public static IApplicationBuilder UseRequestLogger(this IApplicationBuilder app)
        {
            app.UseMiddleware<RequestLoggerMiddleware>();
            return app;
        }
    }
}