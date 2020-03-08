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
        private readonly RequestLoggerDbContext dbContext;

        public RequestLoggerService(RequestLoggerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Log(RequestLog requestLog)
        {
            var count = dbContext.RequestLogs.Count();
            if (count >= RequestLoggerServiceExtension.MaxLogCount)
            {
                var last = dbContext.RequestLogs.OrderBy(p => p.Datetime).FirstOrDefault();
                if (last != null)
                    dbContext.Remove(last);
            }

            dbContext.Add(requestLog);
            dbContext.SaveChanges();
        }


        public IEnumerable<RequestLog> Find(int limit = 1000)
        {
            return dbContext.RequestLogs.OrderByDescending(z => z.Datetime).Take(limit).ToList();
        }

        public IEnumerable<RequestLog> Find(Expression<Func<RequestLog, bool>> predicate, int limit = 1000)
        {
            foreach (var requestLog in dbContext.RequestLogs.Where(predicate).Take(limit))
            {
                yield return requestLog;
            }
        }
    }
}
