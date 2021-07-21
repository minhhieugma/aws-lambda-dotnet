# FROM mcr.microsoft.com/dotnet/runtime:6.0-focal AS base
# WORKDIR /app

FROM public.ecr.aws/lambda/dotnet:6.0 AS base
WORKDIR /var/task/

# # Creates a non-root user with an explicit UID and adds permission to access the /app folder
# # For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
# RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
# USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["Lambda.Dotnet/Lambda.Dotnet.csproj", "Lambda.Dotnet/"]
RUN dotnet restore "Lambda.Dotnet/Lambda.Dotnet.csproj"
COPY . .
WORKDIR "/src/Lambda.Dotnet"
RUN dotnet build "Lambda.Dotnet.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Lambda.Dotnet.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /var/task/
COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "Lambda.Dotnet.dll"]
