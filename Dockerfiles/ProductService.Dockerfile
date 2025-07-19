# -------- Development Dockerfile (Auto Reload) --------
FROM mcr.microsoft.com/dotnet/sdk:8.0

# Set working directory
WORKDIR /app

# Copy source code
COPY . .

# Move into your API project folder
WORKDIR /app/ProductService/ProductService.API

# Enable file change detection (needed for Linux/WSL)
ENV DOTNET_USE_POLLING_FILE_WATCHER=true

# Run with hot reload
CMD ["dotnet", "watch", "run"]
