# Manual Azure Deployment Script
# This uploads your ZIP directly to Azure using Kudu API

$zipPath = "D:\New folder\HclP2\InventorySystem\InventorySystem\InventoryAPI-Deploy.zip"
$appName = "inventory-api-konark"
$kuduUrl = "https://$appName.scm.azurewebsites.net/api/zipdeploy"

Write-Host "🚀 Starting manual deployment to Azure..." -ForegroundColor Cyan
Write-Host "📦 ZIP file: $zipPath" -ForegroundColor Gray
Write-Host "🌐 Target: $appName.azurewebsites.net" -ForegroundColor Gray
Write-Host ""

# Check if ZIP exists
if (-not (Test-Path $zipPath)) {
    Write-Host "❌ ZIP file not found at: $zipPath" -ForegroundColor Red
    Write-Host "Creating new deployment package..." -ForegroundColor Yellow
    cd "D:\New folder\HclP2\InventorySystem\InventorySystem"
    dotnet publish "InventorySystem.Api\InventorySystem.Api.csproj" -c Release -o "publish"
    Compress-Archive -Path "publish\*" -DestinationPath "InventoryAPI-Deploy.zip" -Force
    Write-Host "✅ Package created!" -ForegroundColor Green
}

Write-Host "⚠️  You need deployment credentials from Azure Portal:" -ForegroundColor Yellow
Write-Host "   1. Go to: App Services → inventory-api-konark" -ForegroundColor White
Write-Host "   2. Click: Deployment Center → FTPS credentials" -ForegroundColor White
Write-Host "   3. Copy the Application scope username and password" -ForegroundColor White
Write-Host ""

$username = Read-Host "Enter deployment username (format: appname\`$username)"
$password = Read-Host "Enter deployment password" -AsSecureString

# Convert secure string to plain text
$BSTR = [System.Runtime.InteropServices.Marshal]::SecureStringToBSTR($password)
$passwordPlain = [System.Runtime.InteropServices.Marshal]::PtrToStringAuto($BSTR)

# Create authorization header
$base64AuthInfo = [Convert]::ToBase64String([Text.Encoding]::ASCII.GetBytes(("{0}:{1}" -f $username, $passwordPlain)))

Write-Host ""
Write-Host "📤 Uploading to Azure... (this may take 1-2 minutes)" -ForegroundColor Cyan

try {
    $response = Invoke-RestMethod -Uri $kuduUrl `
        -Headers @{Authorization=("Basic {0}" -f $base64AuthInfo)} `
        -Method POST `
        -InFile $zipPath `
        -ContentType "multipart/form-data" `
        -TimeoutSec 300

    Write-Host "✅ Deployment successful!" -ForegroundColor Green
    Write-Host ""
    Write-Host "🌐 Your API is now live at:" -ForegroundColor Cyan
    Write-Host "   https://$appName.azurewebsites.net" -ForegroundColor White
    Write-Host ""
    Write-Host "Next steps:" -ForegroundColor Yellow
    Write-Host "1. Run CreateAdminUser.sql in Azure Query Editor" -ForegroundColor White
    Write-Host "2. Test API: .\test-api-deployment.ps1" -ForegroundColor White
    
} catch {
    Write-Host "❌ Deployment failed!" -ForegroundColor Red
    Write-Host "Error: $($_.Exception.Message)" -ForegroundColor Red
    Write-Host ""
    Write-Host "💡 Try Option 1 instead: Use Kudu Zip Push Deploy (drag & drop)" -ForegroundColor Yellow
}
