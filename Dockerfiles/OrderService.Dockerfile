# ----------- Build Stage -----------
FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build
WORKDIR /src

# Copy .csproj files
COPY ../Shared/Shared.csproj Shared/
COPY ../OrderService/OrderService.API/OrderService.API.csproj OrderService.API/
COPY ../OrderService/OrderService.Application/OrderService.Application.csproj OrderService.Application/
COPY ../OrderService/OrderService.Domain/OrderService.Domain.csproj OrderService.Domain/
COPY ../OrderService/OrderService.Infrastructure/OrderService.Infrastructure.csproj OrderService.Infrastructure/

# Restore
RUN dotnet restore OrderService.API/OrderService.API.csproj

# Copy all sources
COPY .. .

# Publish
WORKDIR /src/OrderService.API
RUN dotnet publish -c Release -o /app/publish

# ----------- Runtime Stage -----------
FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "OrderService.API.dll"]








