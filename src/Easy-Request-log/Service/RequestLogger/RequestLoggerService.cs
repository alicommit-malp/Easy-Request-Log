using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public IEnumerable<RequestLog> Find(Expression<Func<RequestLog, bool>> predicate, int limit = 1000)
        {
            foreach (var requestLog in _dbContext.RequestLogs.Where(predicate).Take(limit))
            {
                yield return requestLog;
            }
        }
    }
}