# Temporary script to migrate to Azure SQL Database
$env:WEBSITE_SITE_NAME = "inventory-api-konark"
dotnet ef database update --project InventorySystem1.Infrastructure --startup-project InventorySystem.Api
