FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS build-env
WORKDIR /app
COPY . .

RUN dotnet publish "./aplication/WineDocumentation.Api/WineDocumentation.Api.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "WineDocumentation.Api.dll"]
