using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using static TestQuery;

public class TestQuery : IRequest<ResponseModal>
{

    public class Handler : IRequestHandler<TestQuery, ResponseModal>
    {
        private readonly ILogger logger;

        public Handler(ILogger<Handler> logger)
        {
            this.logger = logger;
        }

        public async Task<ResponseModal> Handle(TestQuery query, CancellationToken cancellationToken)
        {
            this.logger.LogInformation("Querying..");

            return new ResponseModal("foo", "bar");
        }
    }

    public record ResponseModal(string Field1, string Field2);
}