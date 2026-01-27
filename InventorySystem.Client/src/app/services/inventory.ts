import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

//  matches the C# class 
export interface InventoryItem {
  id: number;
  name: string;
  quantity: number;
  lowStockThreshold: number;
  price: number;
}

@Injectable({
  providedIn: 'root'
})
export class InventoryService {
  //PORT IS HERE:
  private apiUrl = 'https://inventory-api-konark-eac8bhaxg6hxhaec.centralindia-01.azurewebsites.net/api/inventory';

  constructor(private http: HttpClient) { }

  // 1. Get All Items (with optional search)
  getItems(search: string = ''): Observable<InventoryItem[]> {
    return this.http.get<InventoryItem[]>(`${this.apiUrl}?search=${search}`);
  }

  // 2. Add New Item
  addItem(item: any): Observable<any> {
    return this.http.post(this.apiUrl, item);
  }

  // 3. Update Stock 
  updateStock(id: number, change: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/stock?id=${id}&change=${change}`, {});
  }

  // 4. Delete Item
  deleteItem(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  // 5. Get Item History
  getItemHistory(id: number): Observable<InventoryTransaction[]> {
    return this.http.get<InventoryTransaction[]>(`${this.apiUrl}/${id}/history`);
  }
}

export interface InventoryTransaction {
  id: number;
  quantityChanged: number;
  transactionType: string;
  transactionDate: string;
}