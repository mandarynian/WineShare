FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS base
WORKDIR /app
COPY . .

RUN dotnet publish "./appliaction/WineDocumentation.Api/WineDocumentation.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app ./

EXPOSE 80
ENTRYPOINT ["dotnet", "WineDocumentation.Api.dll"]
