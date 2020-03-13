using System.Collections.Generic;
using System.Linq;
using Easy_Request_log.data.entity;
using Easy_Request_log.Service.RequestLogger;
using Microsoft.AspNetCore.Mvc;

namespace demo.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class LogController : ControllerBase
    {
        private readonly IRequestLoggerService _requestLoggerService;

        public LogController(IRequestLoggerService requestLoggerService)
        {
            _requestLoggerService = requestLoggerService;
        }


        [HttpGet]
        public IEnumerable<RequestLog> Get()
        {
            return _requestLoggerService.Find().OrderByDescending(z=>z.Datetime);
        }
    }
}