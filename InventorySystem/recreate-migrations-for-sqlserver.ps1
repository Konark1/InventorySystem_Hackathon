# Script to regenerate migrations for SQL Server
Write-Host "Setting environment to force SQL Server..." -ForegroundColor Yellow
$env:WEBSITE_SITE_NAME = "inventory-api-konark"

Write-Host "Removing old migrations..." -ForegroundColor Yellow
Remove-Item -Path "InventorySystem1.Infrastructure\Migrations\*" -Recurse -Force -ErrorAction SilentlyContinue

Write-Host "Creating new SQL Server migration..." -ForegroundColor Yellow
dotnet ef migrations add InitialCreate --project InventorySystem1.Infrastructure --startup-project InventorySystem.Api

Write-Host "Applying migration to Azure database..." -ForegroundColor Yellow
dotnet ef database update --project InventorySystem1.Infrastructure --startup-project InventorySystem.Api

Write-Host "Done!" -ForegroundColor Green
