using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Easy_Request_log.data;
using Easy_Request_log.data.entity;
using Easy_Request_log.Extension.Service;

namespace Easy_Request_log.Service.RequestLogger
{
    public class RequestLoggerService : IRequestLoggerService
    {
        public void Log(RequestLog requestLog)
        {
            using var dbContext = new RequestLoggerDbContext();
            var count = dbContext.RequestLogs.Count();
            if (count > RequestLoggerServiceExtension.MaxLogCount)
            {
                var last = dbContext.RequestLogs.OrderBy(p => p.Datetime).FirstOrDefault();
                if (last != null)
                    dbContext.RequestLogs.Remove(last);
            }

            dbContext.Add(requestLog);
            dbContext.SaveChanges();
        }

        public IEnumerable<RequestLog> Find(Expression<Func<RequestLog, bool>> predicate, int limit = 1000)
        {
            using var dbContext = new RequestLoggerDbContext();
            foreach (var requestLog in dbContext.RequestLogs.Where(predicate).Take(limit))
            {
                yield return requestLog;
            }
        }
    }
}