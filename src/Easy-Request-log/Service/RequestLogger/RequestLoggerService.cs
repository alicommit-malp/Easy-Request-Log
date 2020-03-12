using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Easy_Request_log.data;
using Easy_Request_log.data.entity;
using Easy_Request_log.Extension.Service;

namespace Easy_Request_log.Service.RequestLogger
{
    public class RequestLoggerService : IRequestLoggerService
    {
        private readonly RequestLoggerDbContext _dbContext;

        public RequestLoggerService(RequestLoggerDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void Log(RequestLog requestLog)
        {
            var count = _dbContext.RequestLogs.Count();
            if (count >= RequestLoggerServiceExtension.MaxLogCount)
            {
                var last = _dbContext.RequestLogs.OrderBy(p => p.Datetime).FirstOrDefault();
                if (last != null)
                    _dbContext.Remove(last);
            }

            _dbContext.Add(requestLog);
            _dbContext.SaveChanges();
        }


        public IEnumerable<RequestLog> Find(int limit = 1000)
        {
            return _dbContext.RequestLogs.OrderByDescending(z => z.Datetime).Take(limit).ToList();
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
