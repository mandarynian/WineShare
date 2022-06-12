# Get Base Image (Full .NET Core SDK)
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
<<<<<<< HEAD
=======
EXPOSE 80
>>>>>>> 49a9b7c721986ed12de070bf708d35c1bb5c4a03
ENTRYPOINT ["dotnet", "WineDocumentation.Api.dll"]
