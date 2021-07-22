# aws-lambda-dotnet
AWS Lambda function using .NET with Docker


# Image configuration
- CMD override: Lambda.Dotnet.Mediator::Lambda.Dotnet.Mediator.Program::FunctionHandler
- CMD override: Lambda.Dotnet.Mediator::Lambda.Dotnet.Mediator.Program::ApiGatewateHandler
- CMD override: Lambda.Dotnet.Mediator::Lambda.Dotnet.Mediator.Program::ScheduledEventHandler