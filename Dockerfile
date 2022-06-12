FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app


RUN dotnet publish "./appliaction/WineDocumentation.Api/WineDocumentation.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish ./

EXPOSE 80
ENTRYPOINT ["dotnet", "WineDocumentation.Api.dll", "kestral"]
