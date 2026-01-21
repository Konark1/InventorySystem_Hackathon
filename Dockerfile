# 1. Use .NET 8 SDK to build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy EVERYTHING to the cloud
COPY . .

# 2. Restore & Build
# We point to the folder inside "InventorySystem" because of your structure
RUN dotnet restore "InventorySystem/InventorySystem.Api/InventorySystem.Api.csproj"
RUN dotnet publish "InventorySystem/InventorySystem.Api/InventorySystem.Api.csproj" -c Release -o /app/publish

# 3. Run the App
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# 4. Open the Port
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "InventorySystem.Api.dll"]
