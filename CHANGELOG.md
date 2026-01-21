# Changelog

All notable changes to the Inventory Management System project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Planned Features
- UPDATE (PUT) endpoint for inventory items
- DELETE endpoint for inventory items
- JWT authentication
- Pagination support
- Search and filter functionality
- Audit trail/history tracking

## [1.0.0] - 2026-01-21

### Added
- Initial release of Inventory Management System
- RESTful API with .NET 8
- Angular 21 frontend application
- SQLite database with Entity Framework Core
- Clean Architecture implementation
- Swagger/OpenAPI documentation
- Docker containerization
- Railway backend deployment
- Netlify frontend deployment

#### Backend Features
- GET all inventory items endpoint
- POST new inventory item endpoint
- POST update stock levels endpoint
- CORS configuration for cross-origin requests
- Automatic database creation on startup
- Stock quantity validation (prevents negative values)

#### Frontend Features
- View all inventory items in table format
- Add new products to inventory
- Update stock levels (increment/decrement)
- Low stock visual indicators (yellow highlight)
- Responsive Bootstrap UI
- Real-time API integration with HttpClient
- TypeScript type safety

#### Testing
- XUnit test framework setup
- Unit tests for inventory service
- Test execution screenshots

#### Documentation
- Comprehensive README with setup instructions
- API endpoints documentation
- Project assumptions and limitations
- Figma design mockups
- Flow diagrams
- Docker deployment guide
- CONTRIBUTING.md guide
- CHANGELOG.md (this file)

### Technical Stack
- Backend: .NET 8, Entity Framework Core 8.0.11, SQLite
- Frontend: Angular 21, TypeScript 5.9, Bootstrap 5.3.8, RxJS 7.8
- Testing: XUnit, Vitest 4.0.8
- DevOps: Docker, Railway, Netlify, GitHub
- API Documentation: Scalar AspNetCore 2.12.12

## Project Structure

```
v1.0.0
├── Backend (.NET 8 Clean Architecture)
│   ├── API Layer (Controllers, Program.cs)
│   ├── Application Layer (Service Interfaces)
│   ├── Core Layer (Domain Models)
│   └── Infrastructure Layer (EF Core, DbContext)
├── Frontend (Angular 21)
│   ├── Services (API integration)
│   ├── Components (UI)
│   └── Models (TypeScript interfaces)
├── Documentation
│   ├── README.md
│   ├── CONTRIBUTING.md
│   ├── CHANGELOG.md
│   ├── API Endpoints
│   └── Assumptions
└── Testing
    ├── Unit Tests (XUnit)
    └── Test Screenshots
```

---

## Version History

- **v1.0.0** (2026-01-21) - Initial hackathon submission release

---

## Links

- [GitHub Repository](https://github.com/Konark1/InventorySystem_Hackathon.git)
- [Live Frontend](https://gregarious-empanada-1304aa.netlify.app/)
- [Live API](https://inventorysystemhackathon-production.up.railway.app/swagger)
