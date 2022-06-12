FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /appliaction/WineDocumentation.Api/
COPY ["appliaction/WineDocumentation.Api/WineDocumentation.Api.csproj", "WineDocumentation.Api/"]
RUN dotnet restore "WineDocumentation.API/WineDocumentation.Api.csproj"
COPY . .
WORKDIR "/appliaction/WineDocumentation.Api"
RUN dotnet build "WineDocumentation.Api/WineDocumentation.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WineDocumentation.Api/WineDocumentation.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WineDocumentation.Api.dll"]
