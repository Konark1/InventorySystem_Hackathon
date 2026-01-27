# 📦 Inventory Management System

**Full-Stack Cloud Application**  
A production-ready inventory management system built with **Angular 21**, **.NET 8**, and **Azure Cloud Services**.

---

## 🏆 Project Overview

### 1. 🎨 Design
* **UI Design:** Modern Figma-inspired interface with purple gradient theme
* **Architecture Diagrams:** 12 comprehensive PlantUML diagrams ([View Diagrams](./diagrams/))
* **Design System:** Clean cards, professional tables, responsive layout

### 2. 🚀 Live Deployment
* **Frontend (Netlify):** [https://dynamic-platypus-d176c2.netlify.app](https://dynamic-platypus-d176c2.netlify.app)
* **Backend API (Azure):** [https://inventory-api-konark-eac8bhaxg6hxhaec.centralindia-01.azurewebsites.net](https://inventory-api-konark-eac8bhaxg6hxhaec.centralindia-01.azurewebsites.net/swagger)
* **Database:** Azure SQL Database (Production-grade with SSL encryption)

### 3. 📖 Documentation
* **API Endpoints:** [View API Docs](./docs/api_endpoints.md)
* **PlantUML Diagrams:** [View Architecture Diagrams](./diagrams/)
* **Presentation Script:** [View Demo Script](./PRESENTATION_SCRIPT.md)
* **Contributing Guide:** [View Guidelines](./CONTRIBUTING.md)

### 4. ✅ Features
* **Multi-Role Authentication:** JWT-based with Admin and Shop Owner roles
* **Real-Time Inventory:** CRUD operations with transaction history
* **Admin Dashboard:** User management, system statistics, role management
* **Modern UI:** Figma-inspired design with autocomplete forms

```
InventorySystem/
├── InventorySystem/                  # .NET Solution
│   ├── InventorySystem.Api/              # Backend API (.NET 8 Web API)
│   │   ├── Controllers/                      # API endpoints (Auth, Inventory, Admin)
│   │   ├── Program.cs                        # Startup configuration
│   │   └── appsettings.json                  # Azure SQL connection strings
│   ├── InventorySystem.Application/      # Service Interfaces (IInventoryService)
│   ├── InventorySystem.Core/             # Domain Models (ApplicationUser, InventoryItem)
│   ├── InventorySystem1.Infrastructure/  # Data Access Layer (EF Core, DbContext)
│   │   ├── InventoryDbContext.cs             # Entity Framework context
│   │   ├── InventoryService.cs               # Business logic implementation
│   │   └── Migrations/                       # Database migrations
│   └── InventorySystem.Tests/            # xUnit Tests (Pending)
├── InventorySystem.Client/           # Frontend (Angular 21)
│   ├── src/app/
│   │   ├── landing/                      # Landing page component (Figma-inspired)
│   │   ├── admin/                        # Admin dashboard & login
│   │   ├── auth/                         # Auth guards & services
│   │   ├── services/                     # API services (inventory, auth)
│   │   ├── app.ts                        # Main app component (shop dashboard)
│   │   ├── app.html                      # Authentication & dashboard UI
│   │   ├── app.css                       # Global styles (9KB)
│   │   └── app.routes.ts                 # Route configuration
│   ├── angular.json                      # Angular build config
│   ├── package.json                      # npm dependencies
│   └── dist/  y Stack

### Backend (.NET 8)
- **ASP.NET Core 8.0** - Web API Framework
- **Entity Framework Core 9.0** - ORM with Code-First migrations
- **ASP.NET Core Identity** - Authentication & user management
- **JWT Bearer Tokens** - Secure API authentication (7-day expiration)
- **BCrypt.NET** - Password hashing
- **Swagger/Scalar** - Interactive API documentation
- **Azure SQL Database** - Production database with SSL

### Frontend (Angular 21)
- **Angular 21.0.0** - Modern SPA framework with standalone components
- **TypeScript 5.9** - Type-safe development
- **RxJS 7.8** - Reactive programming for async operations
- **SweetAlert2** - Beautiful alert popups
- **FormsModule** - Template-driven forms with validation
- **HttpClient** - RESTful API communication with interceptors

### Cloud Infrastructure
- **Microsoft Azure** - Cloud platform
  - **Azure App Service** - .NET 8 Linux hosting (Central India region)
  - **Azure SQL Database** - Managed database with automatic backups
  - **Application Insights** - Performance monitoring (optional)
- **Netlify** - Frontend CDN with global edge servers
- **GitHub** - Version control and code repository

### Security & Authentication
- **JWT (JSON Web Tokens)** - Stateless authentication
- **BCrypt Password Hashing** - Cost factor 10, auto-salted
- **CORS Policy** - Configured for Netlify origin
- **HTTPS/SSL** - Encrypted data transmission
- **Route Guards** - Frontend & backend authorization
- **Role-Based Access Control** - Admin vs Shop Owner permissions
- **Entity Framework Core 8.0.11** - ORM
- **SQLite** - Database
- **Swagger/OpenAPI** - API Documentation (Scalar AspNetCore 2.12.12)

### Frontend
- **Angular 21.0.0** - SPA Framework
- **TypeScript 5.9** - Type-safe JavaScript
- **RxJS 7.8** - Reactive Programming
- **Bootstrap 5.3.8** - UI Framework
- **HttpClient** - API Communication
- **Vitest 4.0.8** - Testing Framework

### DevOps
- **Railway** - Backend Hosting (Docker Container)
- **Netlify** - Frontend Hosting
- **Docker** - Containerization
- **Git/GitHub** - Version Control

---

## ⚙️ Local Development Setup

### Prerequisites
- **.NET 8 SDK** - [Download](https://dotnet.microsoft.com/download/dotnet/8.0)
- **Node.js 18+** & **npm** - [Download](https://nodejs.org/)
- **Angular CLI** - `npm install -g @angular/cli`
- **SQL Server** (LocalDB or Express) - For local database
- **Git** - Version control

### Backend Setup
```bash
# Navigate to API project
cd InventorySystem/InventorySystem.Api

# Restore NuGet packages
dotnet restore

# Update database connection string in appsettings.json
# For local: Use LocalDB or SQL Server Express

# Apply EF Core migrations
dotnet ef database update

# Run the API
dotnet run

# API runs on https://localhost:7xxx
# Swagger UI: https://localhost:7xxx/scalar/v1
```

### Frontend Setup
```bash
# Navigate to Angular project
cd InventorySystem.Client

# Install npm dependencies
npm install

# Update API URL in src/app/services/inventory.ts (optional for local testing)
# Change to http://localhost:5000 or your local API URL

# Start development server
ng serve

# App runs on http://localhost:4200
```

### Environment Configuration

**Backend (appsettings.Development.json):**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=InventoryDB;Trusted_Connection=true;"
  },
  "Jwt": {
    "Key": "your-super-secret-key-here-minimum-32-characters",
    "Issuer": "InventorySystemAPI",
    "Audience": "InventorySystemClient"
  }
}
```

**Frontend (src/environments/):**
```typescript
export const environment = {
  production: false,
  apiUrl: 'https://localhost:7xxx/api'
};
```
```bash
# From root directory
docker build -t inventory-system .
docker run -p 8080:8080 inventory-system
# API runs on http://localhost:8080
```

---

## 📋 Key Features

### Authentication & Authorization
✅ **Multi-Role System** - Admin and Shop Owner roles with JWT tokens  
✅ **Secure Registration** - Full user profile (name, shop, phone, address, Aadhaar, age)  
✅ **Stealth Admin Login** - Hidden dark-themed admin portal at `/admin/login`  
✅ **Route Guards** - Frontend (AuthGuard, AdminGuard) and backend authorization  
✅ **Password Security** - BCrypt hashing with automatic salt generation  

### Inventory Management
✅ **CRUD Operations** - Create, read, update, delete products  
✅ **Stock Management** - Increment/decrement with single-click buttons  
✅ **Transaction History** - Complete audit trail for every stock change  
✅ **Low Stock Alerts** - Visual indicators when quantity below threshold  
✅ **Search & Filter** - Real-time product search  
✅ *🌐 Deployment

### Backend (Azure App Service)
**Production URL:** `https://inventory-api-konark-eac8bhaxg6hxhaec.centralindia-01.azurewebsites.net`

**Deployment Steps:**
1. Build the project: `dotnet publish -c Release -o ./publish`
2. Create deployment package: `Compress-Archive -Path ./publish/* -DestinationPath deploy.zip`
3. Deploy to Azure:
   - Via Azure Portal: App Service → Deployment Center → Zip Deploy
   - Via Kudu: `https://inventory-api-konark.scm.azurewebsites.net/ZipDeployUI`
   - Via CLI: `az webapp deploy --resource-group InventorySystem --name inventory-api-konark --src-path deploy.zip`

**Azure Configuration:**
- **Runtime:** .NET 8 on Linux
- **Region:** Central India
- **Database:** Azure SQL Database
  - Server: `inventory-server-konark.database.windows.net`
  - Database: `InventoryDB`
  - Connection: SSL encrypted
- **Environment Variables:**
  - `ConnectionStrings__DefaultConnection` - Azure SQL connection string
  - `Jwt__Key` - JWT secret key (32+ characters)
  - `Jwt__Issuer` - Token issuer
  - `Jwt__Audience` - Token audience

### Frontend (Netlify)
**Production URL:** `https://dynamic-platypus-d176c2.netlify.app`

**Deployment Steps:**
1. Build the project: `ng build --configuration production`
2. Deploy to Netlify:
   - Manual: Drag `dist/InventorySystem.Client/browser` folder to Netlify dashboard
   - CLI: `netlify deploy --prod --dir dist/InventorySystem.Client/browser`
   - Git: Connect GitHub repository for automatic deployments

**Netlify Configuration:**
- **Build Command:** `ng build --configuration production`
- **Publish Directory:** `dist/InventorySystem.Client/browser`
- **Build Output:** ~704 KB total (main.js: 473 KB, styles.css: 231 KB)
- **Redirects:** Configure `_redirects` file for Angular routing:
  ```
  /*  /index.html  200
  ```

### Database Migrations
```bash
# Create new migration after model changes
cd InventorySystem/InventorySystem.Api
dotnet ef migrations add MigrationName

# Update database (local)
dotnet ef database update

# For Azure: Migrations run automatically on deployment
# Or manually via Azure Data Studio / SQL Server Management Studio
```

### CORS Configuration
Backend is configured to allow requests from:
- `https://dynamic-platypus-d176c2.netlify.app` (Production)
- `http://localhost:4200` (Development)

### SSL/HTTPS
- **Azure App Service:** Built-in SSL certificate (*.azurewebsites.net)
- **Netlify:** Free SSL certificate with automatic renewal
- **Azure SQL:** Encrypted connection enforcedication, Infrastructure, API)  
✅ **Entity Framework Core** - Code-first migrations, LINQ queries  
✅ **Dependency Injection** - Service lifetime management  
✅ **RESTful API** - Standard HTTP methods and status codes  
✅ **CORS Configuration** - Secure cross-origin resource sharing  
✅ **Swagger Documentation** - Interactive API testing with Scalar UI  
✅ **Azure Cloud** - Production-grade hosting with SSL encryption  
✅ **Global CDN** - Fast frontend delivery via Netlify edge servers  

---

## � Deployment

### Backend (Railway)
The backend API is deployed on Railway using Docker:
1. Connected GitHub repository to Railway
2. Railway automatically detects Dockerfile and builds the container
3. Set environment variable: `ASPNETCORE_URLS=http://+:8080`
4. Railway assigns public URL: `https://inventorysystemhackathon-production.up.railway.app`
5. Swagger UI available at: `/swagger`

### Frontend (Netlify)
The Angular frontend is deployed on Netlify:
1. Build command: `npm run build`
2. Publish directory: `dist/inventory-system.client/browser`
3. Connected GitHub repository for auto-deployment
4. Live URL: `https://gregarious-empanada-1304aa.netlify.app/`

### Environment Configuration
- **API URL:** Update `apiUrl` in `InventorySystem.Client/src/app/services/inventory.ts`
- **Database:** SQLite persists within Railway container
- **CORS:** Configured to allow all origins for demo purposes

---

## 🔗 Important Links

- **Live Frontend:** [https://dynamic-platypus-d176c2.netlify.app](https://dynamic-platypus-d176c2.netlify.app)
- **Live API:** [https://inventory-api-konark-eac8bhaxg6hxhaec.centralindia-01.azurewebsites.net](https://inventory-api-konark-eac8bhaxg6hxhaec.centralindia-01.azurewebsites.net)
- **Swagger/API Docs:** [API Documentation](https://inventory-api-konark-eac8bhaxg6hxhaec.centralindia-01.azurewebsites.net/scalar/v1)
- **GitHub Repository:** [https://github.com/Konark1/InventorySystem](https://github.com/Konark1/InventorySystem)
- **Admin Login:** [Hidden Admin Portal](https://dynamic-platypus-d176c2.netlify.app/admin/login)

### Demo Credentials
**Admin Account:**
- Email: `admin@inventory.com`
- Password: `TestPassword123!`

**Test Shop Owner:**
- Email: `vat2@mail.com`
- Password: `Qwerty1!`

---

## 📊 Architecture Diagrams

View comprehensive PlantUML diagrams in the [`diagrams/`](./diagrams/) folder:
1. System Architecture Overview
2. Authentication Flow
3. Registration Flow
4. Shop Owner Dashboard Flow
5. Admin Dashboard Flow
6. Route Guard Logic
7. Database Schema (Entity Relationship)
8. Deployment Architecture
9. API Endpoint Structure
10. Frontend Component Architecture
11. Security Flow (JWT)
12. Complete User Journey

---

## 🤝 Contributing

Contributions are welcome! Please read [CONTRIBUTING.md](./CONTRIBUTING.md) for guidelines.

### Development Workflow
1. Fork the repository
2. Create a feature branch: `git checkout -b feature/amazing-feature`
3. Commit changes: `git commit -m 'Add amazing feature'`
4. Push to branch: `git push origin feature/amazing-feature`
5. Open a Pull Request

---

## 👥 Team

**Developer:** Konark Verma  
**GitHub:** [@Konark1](https://github.com/Konark1)  
**Email:** konark@example.com

---

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](./LICENSE) file for details.

---

## 📜 Version History

See [CHANGELOG.md](./CHANGELOG.md) for detailed version history and changes.

**Current Version:** 2.0.0 (Production Release)
- ✅ Azure Cloud deployment
- ✅ SQL Server database with migrations
- ✅ Multi-role authentication system
- ✅ Admin dashboard with user management
- ✅ Figma-inspired modern UI
- ✅ Complete PlantUML documentation

---

## 📌 Important Notes

### Security
- Never commit `appsettings.json` with actual connection strings to public repositories
- JWT secret keys should be at least 32 characters and stored securely
- Use environment variables for sensitive configuration in production
- Azure SQL firewall rules should restrict access to known IP addresses

### Database
- Azure SQL Database uses automatic backups (7-day retention)
- Entity Framework migrations handle schema changes
- Connection string uses SSL encryption by default
- Consider implementing database connection pooling for high traffic

### Performance
- Frontend build size: ~704 KB (compressed ~141 KB)
- API response times: < 200ms average
- Netlify CDN provides global edge caching
- Azure App Service auto-scaling available (if configured)

### Future Enhancements
- [ ] xUnit test coverage for API endpoints
- [ ] Integration tests with in-memory database
- [ ] Real-time notifications with SignalR
- [ ] Export data to CSV/Excel
- [ ] Advanced analytics dashboard
- [ ] Email notifications for low stock
- [ ] Barcode scanning integration
- [ ] Multi-warehouse support
- [ ] Audit logs for all changes
- [ ] API rate limiting

---

## 🙏 Acknowledgments

- **Microsoft Azure** - Cloud hosting platform
- **Netlify** - Frontend CDN and hosting
- **Angular Team** - Modern web framework
- **ASP.NET Core Team** - Powerful backend framework
- **Figma** - Design inspiration
- **PlantUML** - Architecture diagram tool

---

**Built with ❤️ using .NET 8 and Angular 21**
