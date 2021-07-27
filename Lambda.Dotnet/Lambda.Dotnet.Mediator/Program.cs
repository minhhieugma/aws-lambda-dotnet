using System;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.CloudWatchEvents.ScheduledEvents;
using Amazon.Lambda.Core;
using Lambda.Dotnet.Mediator.Commands;
using Lambda.Dotnet.Mediator.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
namespace Lambda.Dotnet.Mediator
{
    public class Program
    {
        private IServiceProvider serviceProvider;
        private ILogger logger;
        private IMediator mediator;

        public Program()
        {
            this.serviceProvider = DependencyInjection.BuildServiceProvider();

            logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            mediator = serviceProvider.GetRequiredService<IMediator>();
        }

        public async Task<APIGatewayProxyResponse> ApiGatewateHandler(APIGatewayProxyRequest apigProxyEvent)
        {
            logger.LogError("Headers:{Headers}", apigProxyEvent.Headers);

            logger.LogInformation("Body:{Body}", apigProxyEvent.Body);

            await this.mediator.Send(new TestCommand { });
            var response = await this.mediator.Send(new TestQuery { });

            return new APIGatewayProxyResponse
            {
                Body = apigProxyEvent.Body,
                StatusCode = 200,
            };
        }

        /// <summary>
        /// A simple function that takes a string and returns both the upper and lower case version of the string.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Casing FunctionHandler(string input, ILambdaContext context)
        {
            return new Casing(input?.ToLower(), input?.ToUpper());
        }


        public async Task ScheduledEventHandler(ScheduledEvent scheduledEvents)
        {

        }


        public record Casing(string Lower, string Upper);

    }
}
