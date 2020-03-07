using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Easy_Request_log.data.entity;
using Easy_Request_log.Service.RequestLogger;
using Microsoft.AspNetCore.Http;

namespace Easy_Request_log.Middleware
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class RequestLoggerMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IRequestLoggerService requestLoggerService)
        {
            var requestLog = new RequestLog()
            {
                Datetime = DateTime.UtcNow,
                Username = context.User.Identity.Name
            };

            context.Request.EnableBuffering();
            ;
            var buffer = new byte[Convert.ToInt32(context.Request.ContentLength)];
            await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);

            requestLog.Path = context.Request.Path;
            requestLog.QueryString = context.Request.QueryString.ToString();
            requestLog.Body = bodyAsText;

            context.Request.Body.Position = 0;

            await _next(context);

            requestLog.StatusCode = (HttpStatusCode) context.Response.StatusCode;

            requestLoggerService.Log(requestLog);
        }
    }
}