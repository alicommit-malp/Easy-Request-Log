# Easy Request Log in .Net Core 

After finishing up yo Asp.Net core project project weather it is a MVC or API,
you may want to monitor all the requests which are being received by yo endpoints.
The log file may suite yo needs but in case you need to query the reuqets to
see which endpoint are being called the most or which user has requested which
resoces a well structure database will make much more sense. With this in mind
I have designed a Nuget library to provide this functionality in 2 lines of code.

## Usage

Install the [Nuget Package]() 

```

dotnet add package easy-request-log

```


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

    app.UseAuthorization();

    app.UseRequestLogger();

    ...

}

```

And that's it, the easy-request-log will make a sqlite database and add
each incoming request to the database, the database has a limit of 10,000
records which is configable in the ConfigureService method by passing
the maxLogCount, as it can be seen in the example above the maxLogCount
has been set to 3 which means the easy-request-log will keep only last 3
records in the database on 11th, the oldedst record will be removed.

## Accessing the logs

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
        retn _requestLoggerService.Find().ToList();
    }
}

```
by using the DI you can access the RequestLoggerService which is being registered
behind the sence and is ready to be used and the result will be like this

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

