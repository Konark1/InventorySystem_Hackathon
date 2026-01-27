# 📡 API Documentation

**Base URL:** `https://inventory-api-konark-eac8bhaxg6hxhaec.centralindia-01.azurewebsites.net/api`

**Interactive Documentation:** [Swagger/Scalar UI](https://inventory-api-konark-eac8bhaxg6hxhaec.centralindia-01.azurewebsites.net/scalar/v1)

---

## 🔐 Authentication Endpoints

### 1. Register New User
`POST /auth/register` - Register a new shop owner account

**Request Body:**
```json
{
  "email": "shop@example.com",
  "password": "StrongPass123!",
  "fullName": "John Doe",
  "shopName": "John's Electronics",
  "phoneNumber": "9876543210",
  "physicalAddress": "123 Main St",
  "aadhaarNumber": "123456789012",
  "businessCategory": "Electronics",
  "age": 30
}
```

**Password Requirements:** Min 6 chars, 1 uppercase, 1 lowercase, 1 digit, 1 special char

---

### 2. Login
`POST /auth/login` - Get JWT token (expires in 7 days)

**Request Body:**
```json
{
  "email": "shop@example.com",
  "password": "StrongPass123!"
}
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

---

## 📦 Inventory Endpoints (Requires Auth)

### 3. Get All Items
`GET /inventory` - Get all items for authenticated user

### 4. Get Single Item
`GET /inventory/{id}` - Get specific item details

### 5. Add Item
`POST /inventory` - Create new inventory item

### 6. Update Item
`PUT /inventory/{id}` - Update existing item

### 7. Delete Item
`DELETE /inventory/{id}` - Delete item permanently

### 8. Increment Stock
`PUT /inventory/{id}/increment` - Increase quantity by 1

### 9. Decrement Stock
`PUT /inventory/{id}/decrement` - Decrease quantity by 1

### 10. Transaction History
`GET /inventory/{id}/transactions` - Get audit trail

---

## 👤 Admin Endpoints (Requires Admin Role)

### 11. System Stats
`GET /admin/stats` - Get total users, items, and value

### 12. All Users
`GET /admin/users` - List all registered users with details

### 13. Promote User
`POST /admin/promote/{userId}` - Upgrade to Admin role

### 14. Demote User
`POST /admin/demote/{userId}` - Downgrade to ShopOwner

### 15. Delete User
`DELETE /admin/users/{userId}` - Remove user and their items

---

## 🔒 Authorization Matrix

| Endpoint | Anonymous | Shop Owner | Admin |
|----------|-----------|------------|-------|
| Register/Login | ✅ | ✅ | ✅ |
| Inventory CRUD | ❌ | ✅ (own) | ✅ (all) |
| Admin endpoints | ❌ | ❌ | ✅ |

---

**Full documentation:** [View Complete API Docs](./api_endpoints_full.md)  
**Interactive testing:** [Swagger UI](https://inventory-api-konark-eac8bhaxg6hxhaec.centralindia-01.azurewebsites.net/scalar/v1)
