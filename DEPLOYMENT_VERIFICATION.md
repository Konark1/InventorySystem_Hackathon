# Deployment Verification & Testing Guide

## ✅ Step-by-Step Verification

### 1. Check GitHub Actions Status (2-5 minutes)
- URL: https://github.com/Konark1/InventorySystem_Hackathon/actions
- Look for: "Deploy to Azure App Service" workflow
- Status should be: ✅ Green checkmark (success)
- If ❌ Red X: Click on it to see error logs

### 2. Check Azure App Service Status
- URL: https://portal.azure.com
- Navigate to: App Services → inventory-api-konark
- Check: "Status" should be "Running"
- Click: "Browse" button at top to open your API

### 3. Test API Endpoints

#### A. Health Check (Basic)
Open in browser:
```
https://inventory-api-konark-eac8bhaxg6hxhaec.centralindia-01.azurewebsites.net/
```
Should return: "Welcome to Inventory API" or Swagger page

#### B. Swagger UI (API Documentation)
```
https://inventory-api-konark-eac8bhaxg6hxhaec.centralindia-01.azurewebsites.net/swagger
```
Should show: Interactive API documentation

### 4. Create Admin User in Database

#### Using Azure Portal Query Editor:
1. Go to: SQL Databases → InventoryDB → Query editor
2. Login with: sqladmin / InventoryApp@2026
3. Paste and run the SQL from: `InventorySystem/CreateAdminUser.sql`
4. Check result: Should say "Admin user created successfully!"

### 5. Test Admin Login

#### Using Swagger:
1. Go to Swagger URL (from step 3B)
2. Find: POST `/api/auth/login`
3. Click: "Try it out"
4. Request body:
```json
{
  "email": "admin@inventory.com",
  "password": "Admin@123"
}
```
5. Click: "Execute"
6. Expected Response (200):
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

#### Verify Token Has Admin Role:
1. Copy the token from response
2. Go to: https://jwt.io
3. Paste token in "Encoded" section
4. Check "Payload" section should contain:
```json
{
  "role": "Admin",
  "email": "admin@inventory.com"
}
```

### 6. Test Admin API Endpoints

#### Using Swagger (with authentication):
1. In Swagger, click "Authorize" button (🔒 icon at top)
2. Enter: `Bearer YOUR_TOKEN_HERE`
3. Click: "Authorize"
4. Try: GET `/api/admin/stats`
5. Expected: System statistics (200 OK)

### 7. Test Frontend Admin Dashboard

#### Deploy Angular App:
```powershell
cd InventorySystem.Client
ng build
```
Upload `dist/InventorySystem.Client/browser` folder to Netlify

#### Test Admin Login Flow:
1. Go to: Your Netlify URL
2. Login with: admin@inventory.com / Admin@123
3. Should redirect to: `/admin` route
4. Should see: Admin Dashboard with user list

---

## 🐛 Common Issues & Solutions

### Issue 1: GitHub Action Fails
**Symptom**: Red X in GitHub Actions
**Solution**: 
- Check if `AZURE_WEBAPP_PUBLISH_PROFILE` secret is added
- Download from Azure → App Service → Get publish profile
- Add to GitHub → Settings → Secrets

### Issue 2: 500 Internal Server Error
**Symptom**: API returns 500 error
**Solution**:
- Check Azure logs: App Service → Log stream
- Common cause: Database connection string issue
- Verify: Connection string in Program.cs matches Azure SQL

### Issue 3: Admin Login Returns 401 Unauthorized
**Symptom**: Login fails even with correct credentials
**Solutions**:
- Check: Admin user exists in database
- Run: CreateAdminUser.sql script
- Verify: Password hash is correct

### Issue 4: Admin Dashboard Shows 403 Forbidden
**Symptom**: Can login but admin routes blocked
**Solutions**:
- Check: JWT token contains "role": "Admin"
- Verify: admin-guard.ts is checking role correctly
- Test: Decode token at jwt.io

---

## 🎯 Success Criteria

✅ GitHub Actions deployment completes successfully
✅ Azure App Service is running
✅ Swagger UI loads and shows all endpoints
✅ Admin user exists in database
✅ Admin login returns valid JWT token
✅ JWT token contains "role": "Admin"
✅ Admin API endpoints return data (not 401/403)
✅ Frontend redirects admin to `/admin` route
✅ Admin dashboard loads and displays user list

---

## 📞 Need Help?

If any step fails, check:
1. GitHub Actions logs for build errors
2. Azure App Service logs for runtime errors
3. Browser console for frontend errors
4. Network tab for API call failures
