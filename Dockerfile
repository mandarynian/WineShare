FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS build-env
WORKDIR /app
COPY . .

RUN dotnet publish "./aplication/WineDocumentation.Api/WineDocumentation.Api.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
WORKDIR /app
COPY --from=build-env /app .
ENTRYPOINT ["dotnet", "WineDocumentation.Api.dll"]
