using System.Linq;
using Easy_Request_log.data.entity;

namespace Easy_Request_log.Service.RequestLogger
{
    public interface IRequestLoggerService
    {
        IQueryable<RequestLog> Find(int limit = 1000);
        void Log(RequestLog requestLog);
    }
}