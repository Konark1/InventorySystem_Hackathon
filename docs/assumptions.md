# 📝 Project Assumptions & Limitations

## Assumptions

1. **Single User:** The system is designed for a single store manager; no login is required.
2. **Currency:** All financial values are assumed to be in standard units (e.g., USD) if implemented.
3. **Stock Rules:** Inventory cannot drop below zero. The system prevents negative stock updates.
4. **Database:** SQLite is used for simplicity and portability in the hackathon environment.
5. **CORS Policy:** Open CORS policy (`AllowAll`) is configured to allow frontend development and testing.
6. **Low Stock Alerts:** Low stock status is calculated on the client side using `quantity <= lowStockThreshold` logic.
7. **API Architecture:** Clean Architecture pattern with separate layers: Core (Domain), Application (Interfaces), Infrastructure (Data Access), and API (Presentation).
8. **Frontend Framework:** Angular 21 with standalone components, TypeScript, and Bootstrap 5 for styling.

## Limitations (Hackathon Scope)

### 1. Data Persistence
- The deployed version uses SQLite database
- Data persists within the container but may reset if Railway redeploys or restarts
- For production, migration to PostgreSQL or SQL Server is recommended

### 2. Validation
- Basic frontend validation is in place
- Complex business rules (e.g., warehouse capacity limits, supplier management) are future scope
- No duplicate name checking for inventory items

### 3. Security
- HTTPS is enforced in production deployment
- API authentication (JWT/OAuth) was out of scope for the prototype
- CORS is set to `AllowAnyOrigin()` for development convenience - should be restricted in production
- No rate limiting or DDoS protection implemented

### 4. API Features Not Implemented
- No GET single item by ID endpoint
- No DELETE or UPDATE (PUT) operations for inventory items
- No search, filter, or pagination for large datasets
- No audit trail or history tracking

### 5. Frontend Limitations
- Basic UI without advanced UX features
- No offline support or PWA capabilities
- Limited error handling and user feedback
- No data export/import functionality

### 6. Scalability
- Single-instance deployment without load balancing
- No caching layer (Redis/Memory Cache)
- No message queue for async operations
- Database connection pooling uses defaults

## Future Enhancements

1. **Authentication & Authorization:** Implement JWT-based authentication with role-based access
2. **Advanced Features:** Add reporting, analytics, and dashboard visualizations
3. **Notifications:** Email/SMS alerts for low stock items
4. **Multi-tenant:** Support for multiple stores/warehouses
5. **Mobile App:** Native mobile application for iOS/Android
6. **Integration:** Connect with external systems (e-commerce, POS, suppliers)
