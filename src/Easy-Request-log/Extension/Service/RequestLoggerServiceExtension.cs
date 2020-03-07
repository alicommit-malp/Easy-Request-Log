using Easy_Request_log.data;
using Easy_Request_log.Service.RequestLogger;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Easy_Request_log.Extension.Service
{
    public static class RequestLoggerServiceExtension
    {
        public static IServiceCollection AddRequestLogger(this IServiceCollection services,
            string sqliteConnectionString = "Filename= ./requestLogger.db")
        {
            services.AddDbContext<RequestLoggerDbContext>(options => { options.UseSqlite(sqliteConnectionString); });
            services.AddScoped<IRequestLoggerService, RequestLoggerService>();
            return services;
        }
    }
}