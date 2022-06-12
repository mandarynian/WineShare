FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS build-env
WORKDIR /src
COPY ./appliaction/WineDocumentation.Api/WineDocumentation.Api.csproj ./appliaction/WineDocumentation.Api/
COPY ./appliaction/WineDocumentation.Infrastructure/WineDocumentation.Infrastructure.csproj ./appliaction/WineDocumentation.Infrastructure/
COPY ./appliaction/WineDocumentation.Core/WineDocumentation.Core.csproj ./appliaction/WineDocumentation.Core/

RUN dotnet restore ./appliaction/WineDocumentation.Api/WineDocumentation.Api.csproj
COPY . .

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
WORKDIR /src/appliaction/WineDocumentation.Api/
RUN dotnet publish -c Release -o /app

COPY --from=build-env /app .
ENTRYPOINT ["dotnet", "WineDocumentation.Api.dll"]
