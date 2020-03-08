using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Easy_Request_log.Service.RequestLogger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace demo.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class LogController : ControllerBase
    {
        private readonly IRequestLoggerService _requestLoggerService;
        private readonly ILogger<LogController> _logger;

        public LogController(ILogger<LogController> logger,IRequestLoggerService requestLoggerService)
        {
            _logger = logger;
            _requestLoggerService= requestLoggerService;
        }


        [HttpGet]
        public IEnumerable<HttpStatusCode> Get()
        {
            return _requestLoggerService.Find(z => !string.IsNullOrEmpty(z.Path))
                .ToList().Select(z=>z.StatusCode);
        }
    }
}
