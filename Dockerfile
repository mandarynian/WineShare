FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build-env
WORKDIR /app

# Copy csproj and restore
COPY ./appliaction/WineDocumentation.Api/ ./WineDocumentation.Api/
COPY ./appliaction/WineDocumentation.Infrastructure/ ./WineDocumentation.Infrastructure/
COPY ./appliaction/WineDocumentation.Core/ ./WineDocumentation.Core/

RUN dotnet publish ./WineDocumentation.Api/WineDocumentation.Api.csproj -c Release -o out

# Generate runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 80
EXPOSE 5001
ENTRYPOINT ["dotnet", "WineDocumentation.Api.dll"]
