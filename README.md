# Easy Request Log in .Net Core

After finishing up your [Asp.Net Core]("") project weather if it is a MVC or an API project,
you may want to monitor all the requests which are being received by your endpoints.
You can obviously log them in a file, however log files are unstructured and messy
for instance to see which endpoint are being called the most or which user has requested which
resources is pretty hard. With this in mind I have designed a Nuget library in order to provide a request
logging functionality in just 2 lines of code called Easy-Request-Log.

## Usage

Install it from [Nuget Package](https://www.nuget.org/packages/Easy-Request-log/1.0.0)
Project Source code is [here](https://github.com/alicommit-malp/Easy-Request-Log)

```bash
dotnet add package easy-request-log
```

Add following to your Startup.cs class

```c#
public void ConfigeServices(IServiceCollection services)
{
    ...
    services.AddRequestLogger(maxLogCount:3);
    ...
}


public void Confige(IApplicationBuilder app, IWebHostEnvironment env)
{
    ...
    app.UseRequestLogger();
    ...
}

```

And that's it, the easy-request-log will make a sqlite database and add
each incoming request to the database, the database has a limit of 10,000
records by default which you can change it in the ConfigureService method by passing
the maxLogCount parameter, as it can be seen in the example above the maxLogCount
has been set to 3 for the sake of the demo which means that the easy-request-log will keep only the last 3
records in the database on 4th request, the oldest request will be removed from the database.

## Querying the logs

```c#

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
        return _requestLoggerService.Find().ToList();
    }
}
```

by using the Dependency Injection you can access the RequestLoggerService which is being registered
behind the scene with Scooped lifetime.

The result of hitting the http://your_ip/log will be like this:

```json
[
  {
    "id": "88360c6e-cf8b-483d-95c3-5e4a3a397286",
    "datetime": "2020-03-08T17:03:09.513447",
    "username": null,
    "path": "/log",
    "queryString": "",
    "body": "",
    "statusCode": 200
  },
  {
    "id": "8bdbfe96-95f8-436a-a8ce-b03feea53481",
    "datetime": "2020-03-08T17:03:09.327419",
    "username": null,
    "path": "/log",
    "queryString": "",
    "body": "",
    "statusCode": 200
  },
  {
    "id": "cdb2f9ac-31e5-412a-ac33-501255ba5de3",
    "datetime": "2020-03-08T17:03:09.138145",
    "username": null,
    "path": "/log",
    "queryString": "",
    "body": "",
    "statusCode": 200
  }
]

```

Install it from [Nuget Package](https://www.nuget.org/packages/Easy-Request-log/1.0.0)
Project Source code is [here](https://github.com/alicommit-malp/Easy-Request-Log)

Happy Coding :)
