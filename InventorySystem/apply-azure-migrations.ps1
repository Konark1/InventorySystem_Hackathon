# Force migrations to run on Azure SQL Database
$env:WEBSITE_SITE_NAME = "inventory-api-konark"

Write-Host "🚀 Applying migrations to Azure SQL Database..." -ForegroundColor Cyan

dotnet ef database update --project InventorySystem1.Infrastructure --startup-project InventorySystem.Api --verbose

Write-Host "✅ Migrations applied successfully!" -ForegroundColor Green
