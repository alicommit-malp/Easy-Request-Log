using System.Linq;
using System.Threading.Tasks;
using Easy_Request_log.data.entity;

namespace Easy_Request_log.Service.RequestLogger
{
    public interface IRequestLoggerService
    {
        IQueryable<RequestLog> Find();
        Task Add(RequestLog requestLog);
        Task Remove(RequestLog requestLog);
    }
}