FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS build
WORKDIR /src
COPY ./appliaction/WineDocumentation.Api/WineDocumentation.Api.csproj ./appliaction/WineDocumentation.Api/
COPY ./appliaction/WineDocumentation.Infrastructure/WineDocumentation.Infrastructure.csproj ./appliaction/WineDocumentation.Infrastructure/
COPY ./appliaction/WineDocumentation.Core/WineDocumentation.Core.csproj ./appliaction/WineDocumentation.Core/

RUN dotnet restore ./appliaction/WineDocumentation.Api/WineDocumentation.Api.csproj
COPY . .
WORKDIR /src/appliaction/WineDocumentation.Core
RUN dotnet build -c Release -o /app

WORKDIR /src/appliaction/WineDocumentation.Infrastructure
RUN dotnet build -c Release -o /app

WORKDIR /src/appliaction/WineDocumentation.Api
RUN dotnet build -c Release -o /app


FROM build AS publish
WORKDIR /src/appliaction/WineDocumentation.Api/
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WineDocumentation.Api.dll"]
