# Quick API Health Check Script

$apiUrl = "https://inventory-api-konark-eac8bhaxg6hxhaec.centralindia-01.azurewebsites.net"

Write-Host "`n🔍 Testing Inventory API Deployment...`n" -ForegroundColor Cyan

# Test 1: Basic connectivity
Write-Host "Test 1: Checking API connectivity..." -ForegroundColor Yellow
try {
    $response = Invoke-WebRequest -Uri "$apiUrl/swagger" -UseBasicParsing -TimeoutSec 10
    if ($response.StatusCode -eq 200) {
        Write-Host "✅ API is online and responding!" -ForegroundColor Green
    }
} catch {
    Write-Host "❌ API is not responding. Error: $($_.Exception.Message)" -ForegroundColor Red
    Write-Host "   Please wait a few minutes for deployment to complete." -ForegroundColor Yellow
    exit 1
}

# Test 2: Test admin login (if admin user exists)
Write-Host "`nTest 2: Testing admin login..." -ForegroundColor Yellow
try {
    $loginBody = @{
        email = "admin@inventory.com"
        password = "Admin@123"
    } | ConvertTo-Json

    $loginResponse = Invoke-RestMethod -Uri "$apiUrl/api/auth/login" -Method Post -Body $loginBody -ContentType "application/json"
    
    if ($loginResponse.token) {
        Write-Host "✅ Admin login successful!" -ForegroundColor Green
        Write-Host "   Token received: $($loginResponse.token.Substring(0, 50))..." -ForegroundColor Gray
        
        # Decode token to check role
        $tokenParts = $loginResponse.token.Split('.')
        $payload = [System.Text.Encoding]::UTF8.GetString([System.Convert]::FromBase64String($tokenParts[1] + "=="))
        
        if ($payload -like "*Admin*") {
            Write-Host "✅ Token contains Admin role!" -ForegroundColor Green
        } else {
            Write-Host "⚠️  Token does not contain Admin role" -ForegroundColor Yellow
        }
    }
} catch {
    if ($_.Exception.Response.StatusCode -eq 401) {
        Write-Host "⚠️  Admin user not found or wrong password" -ForegroundColor Yellow
        Write-Host "   Please run CreateAdminUser.sql in Azure Query Editor" -ForegroundColor Cyan
    } else {
        Write-Host "❌ Login test failed: $($_.Exception.Message)" -ForegroundColor Red
    }
}

Write-Host "`n✅ API health check complete!`n" -ForegroundColor Green
Write-Host "Next steps:" -ForegroundColor Cyan
Write-Host "1. Run CreateAdminUser.sql in Azure Query Editor" -ForegroundColor White
Write-Host "2. Test login again with admin@inventory.com / Admin@123" -ForegroundColor White
Write-Host "3. Deploy Angular frontend to Netlify" -ForegroundColor White
