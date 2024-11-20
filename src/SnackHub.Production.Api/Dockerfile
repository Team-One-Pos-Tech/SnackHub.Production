# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/SnackHub.Production.Api/SnackHub.Production.Api.csproj", "src/SnackHub.Production.Api/"]
COPY ["src/SnackHub.Production.Application/SnackHub.Production.Application.csproj", "src/SnackHub.Production.Application/"]
COPY ["src/SnackHub.Production.Domain/SnackHub.Production.Domain.csproj", "src/SnackHub.Production.Domain/"]
COPY ["src/SnackHub.Production.Infra/SnackHub.Production.Infra.csproj", "src/SnackHub.Production.Infra/"]
RUN dotnet restore "./src/SnackHub.Production.Api/SnackHub.Production.Api.csproj"
COPY . .
WORKDIR "/src/src/SnackHub.Production.Api"
RUN dotnet build "./SnackHub.Production.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SnackHub.Production.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SnackHub.Production.Api.dll"]