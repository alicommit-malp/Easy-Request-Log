using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Easy_Request_log.data.entity;

namespace Easy_Request_log.Service.RequestLogger
{
    public interface IRequestLoggerService
    {
        IEnumerable<RequestLog> Find(Expression<Func<RequestLog, bool>> predicate, int limit=1000);
         void Log(RequestLog requestLog);
    }
}