# Instructions to Get Azure Publish Profile and Configure GitHub Deployment

## Step 1: Download Publish Profile from Azure

1. Go to Azure Portal: https://portal.azure.com
2. Navigate to: **App Services** → **inventory-api-konark**
3. Click **"Get publish profile"** button at the top (download icon)
4. This downloads a `.PublishSettings` file
5. Open the file in Notepad and copy ALL the content

## Step 2: Add Publish Profile to GitHub Secrets

1. Go to your GitHub repository: https://github.com/Konark1/InventorySystem_Hackathon
2. Click **Settings** → **Secrets and variables** → **Actions**
3. Click **"New repository secret"**
4. Name: `AZURE_WEBAPP_PUBLISH_PROFILE`
5. Value: Paste the entire content from the .PublishSettings file
6. Click **"Add secret"**

## Step 3: Push Changes to GitHub

Run these commands:
```powershell
git add .github/workflows/azure-deploy.yml
git commit -m "Add GitHub Actions workflow for Azure deployment"
git push
```

## Step 4: Trigger Deployment

After pushing, the deployment will start automatically!
- Go to: https://github.com/Konark1/InventorySystem_Hackathon/actions
- Watch the deployment progress
- Once complete, your API will be live!

## Alternative: Manual Trigger from Azure Portal

If you prefer, you can also deploy directly from Azure:

1. Go to Azure Portal → **inventory-api-konark**
2. Click **Deployment Center** (left menu)
3. Click **"Settings"**
4. Source: Select **"GitHub"**
5. Authorize GitHub access
6. Select:
   - Organization: Konark1
   - Repository: InventorySystem_Hackathon
   - Branch: main
7. Build: Select **".NET"**
8. Click **"Save"**

Azure will automatically deploy whenever you push to main branch!
