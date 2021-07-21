using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

public class TestCommand : IRequest<Unit>
{


    public class Handler : IRequestHandler<TestCommand>
    {
        private readonly ILogger logger;

        public Handler(ILogger<TestCommand> logger)
        {
            this.logger = logger;
        }

        public async Task<Unit> Handle(TestCommand request, CancellationToken cancellationToken)
        {
            this.logger.LogInformation("Executing..");

            return Unit.Value;
        }
    }

}