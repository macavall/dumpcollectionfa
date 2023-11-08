using System;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace dumpcollectionfa
{
    public class http1
    {
        private readonly ILogger _logger;

        public http1(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<http1>();
        }

        [Function("http1")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            string fail = Environment.GetEnvironmentVariable("fail");

            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");

            if (fail.ToLower() == "true")
            {
                OverflowMethod();
            }
            else
            {
                response.WriteString("Fail is set to FALSE");
            }
            

            return response;
        }

        public void OverflowMethod()
        {
            OverflowMethod();
        }
    }
}
