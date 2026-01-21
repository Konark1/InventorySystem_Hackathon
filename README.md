# 📦 Inventory Management System

**Hackathon Submission**  
A full-stack inventory tracking solution built with **Angular 21** and **.NET 8 Clean Architecture**.

---

## 🏆 Deliverables Checklist

### 1. 🎨 Design
* **Wireframes:** [View Low-Fidelity Wireframe](./design/wireframe.png) *(Pending Upload)*
* **UX Flow:** [View UX Flow Diagram](./design/ux_flow.png) *(Pending Upload)*

### 2. 🚀 Prototype (Live Links)
* **Frontend (Netlify):** [https://gregarious-empanada-1304aa.netlify.app/](https://gregarious-empanada-1304aa.netlify.app/)
* **Backend API (Railway):** [https://inventorysystemhackathon-production.up.railway.app/swagger](https://inventorysystemhackathon-production.up.railway.app/swagger)
* **Database Schema:** SQLite (In-container persistence)

### 3. 📖 Documentation
* **API Endpoints:** [View API Docs](./docs/api_endpoints.md)
* **Assumptions:** [View Assumptions](./docs/assumptions.md)

### 4. ✅ Testing
* **Test Report:** [View Unit Test Evidence](./tests/test_execution.png) *(Pending Upload)*

---

## 🛠️ Code Structure
*(Source code located in root directory)*

```
InventorySystem/
├── InventorySystem/              # .NET Solution Folder
│   ├── InventorySystem.Api/          # Backend API (.NET 8 Web API)
│   ├── InventorySystem.Application/  # Service Interfaces
│   ├── InventorySystem.Core/         # Domain Models & Business Logic
│   ├── InventorySystem1.Infrastructure/ # Data Access & EF Core (Active)
│   ├── InventorySystem.Infrastructure/  # (VB - Not in use)
│   └── InventorySystem.Tests/        # Unit Tests
├── InventorySystem.Client/       # Frontend (Angular 21)
├── ClassLibrary1/                # Test/Demo Library
├── docs/                         # Documentation (API, Assumptions)
├── design/                       # Wireframes & UX Flow (Pending)
├── tests/                        # Test Evidence (Pending)
├── Dockerfile                    # Docker Configuration
└── README.md                     # This file
```

---

## 🚀 Technologies Used

### Backend
- **.NET 8** - Web API
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

## ⚙️ Quick Start (Local Development)

### Prerequisites
- .NET 8 SDK
- Node.js 18+ & npm 11.6.2+
- Angular CLI (`npm install -g @angular/cli`)

### Backend Setup
```bash
cd InventorySystem/InventorySystem.Api
dotnet restore
dotnet run
# API runs on https://localhost:7xxx
# Swagger UI: https://localhost:7xxx/swagger
```

### Frontend Setup
```bash
cd InventorySystem.Client
npm install
ng serve
# App runs on http://localhost:4200
```

### Docker Setup (Alternative)
```bash
# From root directory
docker build -t inventory-system .
docker run -p 8080:8080 inventory-system
# API runs on http://localhost:8080
```

---

## 📋 Features

✅ View all inventory items  
✅ Add new products to inventory  
✅ Update stock levels (increment/decrement)  
✅ Low stock threshold alerts (visual indicators)  
✅ Clean Architecture implementation  
✅ RESTful API with Swagger documentation  
✅ Responsive Angular UI with Bootstrap  
✅ SQLite database persistence  
✅ CORS enabled for cross-origin requests  
✅ Docker containerized deployment  

---

## 🔗 Repository

**GitHub:** [https://github.com/Konark1/InventorySystem_Hackathon.git](https://github.com/Konark1/InventorySystem_Hackathon.git)

---

## 👥 Team / Developer

[Your Name/Team Name Here]

---

## 📝 License

This project was created for hackathon purposes.

---

## 📌 Notes

- The **design/** and **tests/** folders are currently empty. Upload wireframes and test evidence screenshots before final submission.
- The actual infrastructure layer is in **InventorySystem1.Infrastructure/** (not InventorySystem.Infrastructure which contains VB files).
- Database persists within the Railway container but may reset on redeployment.
- For local development, the SQLite database file is created automatically on first run.
