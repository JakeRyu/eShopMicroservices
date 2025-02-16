FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

## Set environment variables for HTTPS certificates
#ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx
#ENV ASPNETCORE_Kestrel__Certificates__Default__Password="hyundai"
## Copy the certificate inside the container
COPY ~/.aspnet/https/aspnetapp.pfx /https/

COPY ["Services/Catalog/Catalog.API.csproj", "Services/Catalog/"]
COPY ["BuildingBlocks/BuildingBlocks.csproj", "BuildingBlocks/"]
RUN dotnet restore "Services/Catalog/Catalog.API.csproj"
COPY . .
WORKDIR "/src/Services/Catalog"
RUN dotnet build "Catalog.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Catalog.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.API.dll"]
