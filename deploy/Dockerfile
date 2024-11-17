FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5288
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ./src /src

RUN dotnet restore "SnackHub.Production.Api/SnackHub.Production.Api.csproj"
WORKDIR "/src/SnackHub.Production.Api"

FROM build AS publish
RUN dotnet publish "SnackHub.Production.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SnackHub.Production.Api.dll"]
