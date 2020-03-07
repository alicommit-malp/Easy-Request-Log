using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Easy_Request_log.data;
using Easy_Request_log.data.entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

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

        public async Task Invoke(HttpContext context, RequestLoggerDbContext requestLoggerDbContext)
        {
            var requestLog = new RequestLog()
            {
                Datetime = DateTime.UtcNow,
                Username = context.User.Identity.Name
            };

            context.Request.EnableRewind();
            var buffer = new byte[Convert.ToInt32(context.Request.ContentLength)];
            await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);

            requestLog.Path = context.Request.Path;
            requestLog.QueryString = context.Request.QueryString.ToString();
            requestLog.Body = bodyAsText;

            context.Request.Body.Position = 0;

            await _next(context);

            requestLog.StatusCode = (HttpStatusCode) context.Response.StatusCode;

            requestLoggerDbContext.Add(requestLog);
            await requestLoggerDbContext.SaveChangesAsync();
        }
    }
}