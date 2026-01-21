# 📡 API Documentation

**Base URL:** `https://inventorysystemhackathon-production.up.railway.app`

---

## 📦 Inventory Endpoints

### 1. Get All Inventory Items
- **Method:** `GET`
- **Endpoint:** `/api/inventory`
- **Description:** Retrieve all inventory items from the system.
- **Request:** None
- **Response Example:**
```json
[
  {
    "id": 1,
    "name": "Laptop",
    "quantity": 10,
    "lowStockThreshold": 5
  },
  {
    "id": 2,
    "name": "Mouse",
    "quantity": 3,
    "lowStockThreshold": 5
  }
]
```

> **Note:** The `isLowStock` status can be determined on the client side by checking if `quantity <= lowStockThreshold`

### 2. Add New Item
- **Method:** `POST`
- **Endpoint:** `/api/inventory`
- **Description:** Add a new product to the inventory system.
- **Request Body:**
```json
{
  "name": "Laptop",
  "quantity": 10,
  "lowStockThreshold": 5
}
```
- **Response Example:**
```json
{
  "id": 1,
  "name": "Laptop",
  "quantity": 10,
  "lowStockThreshold": 5
}
```

---

## 📊 Stock Operations

### 3. Update Stock Levels
- **Method:** `POST`
- **Endpoint:** `/api/inventory/stock?id={id}&change={change}`
- **Description:** Update stock levels by incrementing or decrementing quantity.
- **Query Parameters:**
  - `id` (int): The ID of the inventory item
  - `change` (int): The quantity to add (positive) or remove (negative)
- **Example URLs:**
  - Add 5 items: `/api/inventory/stock?id=1&change=5`
  - Remove 3 items: `/api/inventory/stock?id=1&change=-3`
- **Response Example:**
```json
{
  "message": "Stock updated"
}
```

---

## 📋 Data Model

### InventoryItem
```json
{
  "id": 1,
  "name": "string",
  "quantity": 0,
  "lowStockThreshold": 0
}
```

**Field Descriptions:**
- `id`: Unique identifier (auto-generated)
- `name`: Product name
- `quantity`: Current stock quantity
- `lowStockThreshold`: Minimum stock level before alert

> **Note:** The model includes an `IsLowStock()` method (not serialized in JSON) that returns `true` when `quantity <= lowStockThreshold`. The frontend should implement this logic for UI indicators.

---

## 🔴 Error Responses

### 400 Bad Request
```json
{
  "error": "Invalid request data"
}
```

### 404 Not Found
```json
{
  "error": "Item not found"
}
```

### 500 Internal Server Error
```json
{
  "error": "An error occurred while processing your request"
}
```

---

## 🧪 Testing with cURL

### Get All Items
```bash
curl -X GET https://inventorysystemhackathon-production.up.railway.app/api/inventory
```

### Add New Item
```bash
curl -X POST https://inventorysystemhackathon-production.up.railway.app/api/inventory \
  -H "Content-Type: application/json" \
  -d '{"name":"Laptop","quantity":10,"lowStockThreshold":5}'
```

### Update Stock
```bash
curl -X POST "https://inventorysystemhackathon-production.up.railway.app/api/inventory/stock?id=1&change=5"
```

---

## 📝 Notes

- All endpoints return JSON responses
- No authentication required (for demo purposes)
- Stock quantity cannot go below 0 (automatically set to 0 if negative)
- Low stock status should be calculated on the client side: `quantity <= lowStockThreshold`
- The API does not include `isLowStock` in responses; implement this logic in your frontend
