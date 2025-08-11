# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy solution and project files
COPY ["*.sln", "."]
COPY ["src/Api/UrlShortener.Api/UrlShortener.Api.csproj", "src/Api/UrlShortener.Api/"]
COPY ["src/Application/UrlShortener.Application/UrlShortener.Application.csproj", "src/Application/UrlShortener.Application/"]
COPY ["src/Domain/UrlShortener.Domain/UrlShortener.Domain.csproj", "src/Domain/UrlShortener.Domain/"]
COPY ["src/Infrastructure/UrlShortener.Infrastructure/UrlShortener.Infrastructure.csproj", "src/Infrastructure/UrlShortener.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "src/Api/UrlShortener.Api/UrlShortener.Api.csproj"

# Copy everything else
COPY . .

# Build and publish
WORKDIR "/app/src/Api/UrlShortener.Api"
RUN dotnet build -c Release -o /app/build

FROM build AS publish
WORKDIR "/app/src/Api/UrlShortener.Api"
RUN dotnet publish -c Release -o /app/publish --no-restore /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Install curl for healthcheck
RUN apt-get update && apt-get install -y --no-install-recommends \
    curl \
    && rm -rf /var/lib/apt/lists/*

# Copy the published app
COPY --from=publish /app/publish .

# Set the entry point
ENTRYPOINT ["dotnet", "UrlShortener.Api.dll"]
