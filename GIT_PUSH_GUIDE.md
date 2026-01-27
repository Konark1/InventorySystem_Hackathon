# 📤 Git Push Guide

## ✅ Files TO PUSH to GitHub

### Source Code
- ✅ `InventorySystem/` - All .NET backend projects
  - `InventorySystem.Api/` - API controllers, Program.cs
  - `InventorySystem.Core/` - Domain models
  - `InventorySystem.Application/` - Service interfaces
  - `InventorySystem1.Infrastructure/` - EF Core, DbContext
  - **Exclude:** `bin/`, `obj/`, `publish/`, `.vs/`

- ✅ `InventorySystem.Client/` - Angular frontend
  - `src/app/` - All Angular components
  - `angular.json`, `package.json`, `tsconfig.json`
  - **Exclude:** `node_modules/`, `dist/`, `.angular/`

### Documentation
- ✅ `README.md` - **UPDATED** Main documentation
- ✅ `docs/api_endpoints.md` - **UPDATED** API reference
- ✅ `diagrams/` - **NEW** All 12 PlantUML diagrams
- ✅ `CONTRIBUTING.md` - Contribution guidelines
- ✅ `CHANGELOG.md` - Version history
- ✅ `PRESENTATION_SCRIPT.md` - Demo walkthrough
- ✅ `LICENSE` - MIT License
- ✅ `.gitignore` - **UPDATED** Ignore rules

### Data & Config
- ✅ `production_data.csv` - Sample test data
- ✅ `InventorySystem.slnx` - Solution file

---

## ❌ Files NOT TO PUSH (Already in .gitignore)

### Build Artifacts
- ❌ `node_modules/` - npm packages (1GB+)
- ❌ `dist/` - Angular build output
- ❌ `bin/`, `obj/` - .NET build files
- ❌ `publish/` - Deployment packages
- ❌ `*.zip` - Deployment zip files
- ❌ `.angular/` - Angular cache

### Sensitive Files
- ❌ `appsettings.json` - Contains Azure SQL connection strings
- ❌ `appsettings.Development.json` - Local config
- ❌ `appsettings.Production.json` - Production secrets
- ❌ `*.env` - Environment variables
- ❌ `.env.local` - Local secrets

### Database
- ❌ `*.db` - SQLite database files
- ❌ `*.db-shm`, `*.db-wal` - SQLite temp files

### IDE & OS
- ❌ `.vscode/` - VS Code settings
- ❌ `.vs/` - Visual Studio cache
- ❌ `.idea/` - JetBrains IDE
- ❌ `.DS_Store` - macOS metadata
- ❌ `Thumbs.db` - Windows thumbnails

### Legacy/Unused
- ❌ `Dockerfile` - **REMOVED** (using Azure native, not Docker)
- ❌ `design/` - **REMOVED** (replaced with diagrams/)
- ❌ `tests/` - **REMOVED** (old screenshot folder)
- ❌ `ClassLibrary1/` - **REMOVED** (test library)
- ❌ `InventorySystem.Infrastructure/` - VB project (not used)

---

## 🚀 Git Commands to Push

### 1. Check Current Status
```bash
cd "d:\New folder\HclP2\InventorySystem"
git status
```

### 2. Stage All Changes
```bash
# Add all updated files
git add .

# Or add specific files
git add README.md
git add docs/api_endpoints.md
git add diagrams/
git add .gitignore
git add InventorySystem/
git add InventorySystem.Client/src/
```

### 3. Commit Changes
```bash
git commit -m "docs: Update documentation for Azure deployment

- Updated README with Azure SQL and App Service details
- Comprehensive API endpoint documentation
- Added 12 PlantUML architecture diagrams
- Updated .gitignore for Azure deployment
- Removed Docker files (using Azure native)
- Added Figma-inspired UI documentation
- Updated technology stack section
- Added deployment instructions for Azure and Netlify"
```

### 4. Push to GitHub
```bash
# Push to main branch
git push origin main

# Or if you're on master
git push origin master
```

---

## 📊 Expected Repository Size

**Before cleanup:**
- ~500 MB (with node_modules, dist, bin, obj)

**After cleanup (what gets pushed):**
- ~5-10 MB (source code + docs only)

**Breakdown:**
- Source code (.ts, .cs): ~2 MB
- Documentation (.md, .puml): ~500 KB
- Configuration files: ~100 KB
- Sample data (CSV): ~50 KB

---

## ⚠️ Important Notes

### Secrets Management
**NEVER commit these to public GitHub:**
- ❌ Azure SQL connection strings
- ❌ JWT secret keys
- ❌ API keys or tokens
- ❌ Password hashes in config files

**Instead:**
- Use environment variables in Azure App Service
- Use Azure Key Vault for production secrets
- Keep `appsettings.json` in `.gitignore`
- Document required environment variables in README

### Azure Configuration
**These should be set as Azure App Service Environment Variables:**
```
ConnectionStrings__DefaultConnection = "Server=inventory-server-konark.database.windows.net;..."
Jwt__Key = "your-secret-key-minimum-32-characters"
Jwt__Issuer = "InventorySystemAPI"
Jwt__Audience = "InventorySystemClient"
```

### Frontend Configuration
**API URL is hardcoded in source:**
- `src/app/auth.ts` line 12
- `src/app/services/inventory.ts` line 14
- `src/app/admin/admin-login.component.ts` line 239
- `src/app/admin/admin-dashboard.component.ts` line 24

**These are safe to push** because they're public-facing URLs.

---

## 🔍 Pre-Push Checklist

Before pushing to GitHub, verify:

- [ ] No `node_modules/` directory
- [ ] No `dist/` or `bin/` folders
- [ ] No `.db` database files
- [ ] No `appsettings.json` with connection strings
- [ ] No deployment zip files
- [ ] No personal API keys or secrets
- [ ] README is updated with correct URLs
- [ ] API documentation reflects current endpoints
- [ ] .gitignore is comprehensive
- [ ] All PlantUML diagrams are included

---

## 🎯 Quick Verification
```bash
# Check what will be pushed
git status

# Check file sizes
du -sh .git

# List ignored files (should be empty)
git check-ignore -v **/*

# See what's staged
git diff --cached --name-only
```

---

## 📝 Recommended Commit Message Format

```
type(scope): short description

- Detailed change 1
- Detailed change 2
- Detailed change 3
```

**Types:** feat, fix, docs, style, refactor, test, chore

**Example:**
```bash
git commit -m "feat(deployment): Migrate from Railway to Azure Cloud

- Replaced SQLite with Azure SQL Database
- Configured Azure App Service for API hosting
- Updated frontend to use Azure API endpoints
- Removed Docker configuration (using Azure native)
- Added comprehensive deployment documentation
- Updated API endpoints for new Azure URLs"
```

---

## 🔗 After Pushing

1. **Verify on GitHub:**
   - Check repository size is reasonable (<20 MB)
   - Verify no sensitive files are visible
   - Test README renders correctly
   - Check diagrams are viewable

2. **Update GitHub Settings:**
   - Add repository description
   - Add topics/tags: `angular`, `dotnet`, `azure`, `inventory-management`
   - Set up branch protection rules (optional)
   - Enable GitHub Actions for CI/CD (optional)

3. **Share Links:**
   - Add GitHub repo link to LinkedIn/portfolio
   - Update resume with project details
   - Add to Azure DevOps or project management tool

---

## ✅ Final Push Command

```bash
cd "d:\New folder\HclP2\InventorySystem"
git add .
git commit -m "docs: Complete Azure migration and documentation update"
git push origin main
```

**Estimated push time:** 30-60 seconds (depends on internet speed)

---

**Last Updated:** January 27, 2026  
**Repository:** https://github.com/Konark1/InventorySystem
