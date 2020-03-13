using System.Linq;
using System.Threading.Tasks;
using Easy_Request_log.data;
using Easy_Request_log.data.entity;

namespace Easy_Request_log.Service.RequestLogger
{
    public class RequestLoggerService : IRequestLoggerService
    {
        private readonly RequestLoggerDbContext _dbContext;

        public RequestLoggerService(RequestLoggerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(RequestLog requestLog)
        {
            await _dbContext.AddAsync(requestLog);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Remove(RequestLog requestLog)
        {
            _dbContext.Remove(requestLog);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<RequestLog> Find()
        {
            return _dbContext.RequestLogs.AsQueryable();
        }
    }
}