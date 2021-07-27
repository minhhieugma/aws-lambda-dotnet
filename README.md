# AWS Lambda & .NET

![AWS%20Lambda%20&%20NET%20cfb8e4348edf4e42b67a0dc558e3a080/600px-AWS_Lambda_logo.svg.png](AWS%20Lambda%20&%20NET%20cfb8e4348edf4e42b67a0dc558e3a080/600px-AWS_Lambda_logo.svg.png)

![AWS%20Lambda%20&%20NET%20cfb8e4348edf4e42b67a0dc558e3a080/440px-Microsoft_.NET_logo.png](AWS%20Lambda%20&%20NET%20cfb8e4348edf4e42b67a0dc558e3a080/440px-Microsoft_.NET_logo.png)

![AWS%20Lambda%20&%20NET%20cfb8e4348edf4e42b67a0dc558e3a080/docker.png](AWS%20Lambda%20&%20NET%20cfb8e4348edf4e42b67a0dc558e3a080/docker.png)

All things we need to deploy .NET 5 AWS Lambda Support with Container Images!

AWS provides many resources about Lambda function and how to run it with .NET. However, I see it is quite obsolete and not very clear enough for newcomers. That's why I create this place to put all things I have gone through.

# Source Code Structure

- Lambda.Dotnet
    - A very simple project that we can write code and deploy it on AWS Lambda
- Lambda.Dotnet.Mediator
    - Similar to `Lambda.Dotnet`. However, this project has an implementation of `MediatR`
- Lambda.Tests
    - Help us to test the function locally before deploying to AWS Lambda

## Configuration

We use `appsetting.json` as a normal .NET application. During testing time, we can manipulate Environment Variables via `Environment.SetEnvironmentVariable`

## Docker

Currently, we use .NET 5 for the whole solution but we still can change it to any other .NET version. We even can build a custom Docker image as well.

- In `Dockerfile`, we have `COPY ["nuget.config", "."]` to support a private package repository.
- In case you need to run the code with `.NET 6` which is not supported yet by AWS. Please let me know.

## Deployment

We need to create a `Lambda function` with the `Container image` option.

- Container image, it is a lambda function option to create a function that allows running Docker in it

![AWS%20Lambda%20&%20NET%20cfb8e4348edf4e42b67a0dc558e3a080/Untitled.png](AWS%20Lambda%20&%20NET%20cfb8e4348edf4e42b67a0dc558e3a080/Untitled.png)

## Image configuration - CMD

To let the Lambda function know which .NET function needs to be called when the lambda function event occurred, we have to correct `ENTRYPOINT` by updating `CMD`:

- FunctionHandler

    ```bash
    Lambda.Dotnet::Lambda.Dotnet.Program::FunctionHandler
    ```

    ```bash
    Lambda.Dotnet.Mediator::Lambda.Dotnet.Mediator.Program::FunctionHandler
    ```

- ScheduledEventHandler

    ```bash
    Lambda.Dotnet::Lambda.Dotnet.Program::ScheduledEventHandler
    ```

    ```bash
    Lambda.Dotnet.Mediator::Lambda.Dotnet.Mediator.Program::ScheduledEventHandler
    ```

- ApiGatewateHandler

```bash
Lambda.Dotnet::Lambda.Dotnet.Program::ApiGatewateHandler
```

```bash
Lambda.Dotnet.Mediator::Lambda.Dotnet.Mediator.Program::ApiGatewateHandler
```

![AWS%20Lambda%20&%20NET%20cfb8e4348edf4e42b67a0dc558e3a080/Untitled%201.png](AWS%20Lambda%20&%20NET%20cfb8e4348edf4e42b67a0dc558e3a080/Untitled%201.png)

![AWS%20Lambda%20&%20NET%20cfb8e4348edf4e42b67a0dc558e3a080/Untitled%202.png](AWS%20Lambda%20&%20NET%20cfb8e4348edf4e42b67a0dc558e3a080/Untitled%202.png)

## References

There are some other resources from AWS that you might want to check out

- [https://aws.amazon.com/developer/language/net/?whats-new-dotnet.sort-by=item.additionalFields.postDateTime&whats-new-dotnet.sort-order=desc](https://aws.amazon.com/developer/language/net/?whats-new-dotnet.sort-by=item.additionalFields.postDateTime&whats-new-dotnet.sort-order=desc)
- [https://aws.amazon.com/blogs/developer/net-5-aws-lambda-support-with-container-images/](https://aws.amazon.com/blogs/developer/net-5-aws-lambda-support-with-container-images/)