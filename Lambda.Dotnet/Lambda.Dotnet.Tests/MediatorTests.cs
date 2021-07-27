using System;
using System.Collections.Generic;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.CloudWatchEvents.ScheduledEvents;
using Amazon.Lambda.TestUtilities;
using Lambda.Dotnet.Mediator;
using Xunit;

namespace Lambda.Tests
{
    public class MediatorTests
    {
        [Fact]
        public void TestToUpperFunction()
        {

            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Program();
            var context = new TestLambdaContext();

            var casing = function.FunctionHandler("hello world", context);

            Assert.Equal("hello world", casing.Lower);
            Assert.Equal("HELLO WORLD", casing.Upper);
        }

        [Fact]
        public async void TestAPIGatewayProxyRequest()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "development");

            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Program();
            var request = new APIGatewayProxyRequest();
            request.RequestContext = new APIGatewayProxyRequest.ProxyRequestContext
            {
                RequestId = Guid.NewGuid().ToString(),
            };

            request.Headers = new Dictionary<string, string>();
            request.Body = "{}";


            var casing = await function.ApiGatewateHandler(request);

            Assert.Equal(200, casing.StatusCode);
        }


        [Fact]
        public async void ScheduledEventHandlerTest()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "development");

            // Invoke the lambda function and confirm the string was upper cased.
            var function = new Program();
            var request = new ScheduledEvent();

            await function.ScheduledEventHandler(request);

            //Assert.Equal(200, casing.StatusCode);
        }
    }
}
