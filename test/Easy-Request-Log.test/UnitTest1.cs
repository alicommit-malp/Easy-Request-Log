using System.IO;
using System.Net;
using System.Security.Principal;
using System.Threading.Tasks;
using Easy_Request_log.Middleware;
using Easy_Request_log.Service.RequestLogger;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;

namespace Easy_Request_Log.test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {
            var requestLoggerService = new Mock<IRequestLoggerService>();

            var requestMock = new Mock<HttpRequest>();
            requestMock.Setup(x => x.Scheme).Returns("http");
            requestMock.Setup(x => x.Host).Returns(new HostString("localhost"));
            requestMock.Setup(x => x.Path).Returns(new PathString("/test"));
            requestMock.Setup(x => x.PathBase).Returns(new PathString("/"));
            requestMock.Setup(x => x.Method).Returns("GET");
            requestMock.Setup(x => x.Body).Returns(new MemoryStream());
            requestMock.Setup(x => x.QueryString).Returns(new QueryString("?param1=2"));


            var contextMock = new Mock<HttpContext>();
            contextMock.Setup(x => x.Request).Returns(requestMock.Object);

            var fakeIdentity = new GenericIdentity("Ali Alp");
            var principal = new GenericPrincipal(fakeIdentity, null);
            contextMock.Setup(t => t.User).Returns(principal);
            var response = new Mock<HttpResponse>();

            response.Setup(z => z.StatusCode).Returns((int) HttpStatusCode.OK);
            contextMock.Setup(z => z.Response).Returns(response.Object);

            var requestLoggerMiddleware = new RequestLoggerMiddleware(next: (innerHttpContext) => Task.FromResult(0));


            await requestLoggerMiddleware.Invoke(contextMock.Object, requestLoggerService.Object);

            Assert.Pass();
        }
    }
}