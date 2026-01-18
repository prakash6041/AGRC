# Use the official .NET 8 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["API/GRC.API/GRC.API.csproj", "API/GRC.API/"]
COPY ["API/GRC.Application/GRC.Application.csproj", "API/GRC.Application/"]
COPY ["API/GRC.Domain/GRC.Domain.csproj", "API/GRC.Domain/"]
COPY ["API/GRC.Infrastructure/GRC.Infrastructure.csproj", "API/GRC.Infrastructure/"]

RUN dotnet restore "API/GRC.API/GRC.API.csproj"

# Copy everything else and build
COPY . .
WORKDIR "/src/API/GRC.API"
RUN dotnet build "GRC.API.csproj" -c Release -o /app/build

# Publish the app
RUN dotnet publish "GRC.API.csproj" -c Release -o /app/publish

# Use the official .NET 8 runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "GRC.API.dll"]