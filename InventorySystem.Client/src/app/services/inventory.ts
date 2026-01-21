import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

//  matches the C# class 
export interface InventoryItem {
  id: number;
  name: string;
  quantity: number;
  lowStockThreshold: number;
}

@Injectable({
  providedIn: 'root'
})
export class InventoryService {
  //PORT IS HERE:
  private apiUrl = 'https://inventorysystemhackathon-production.up.railway.app/api/inventory';

  constructor(private http: HttpClient) { }

  // 1. Get All Items
  getItems(): Observable<InventoryItem[]> {
    return this.http.get<InventoryItem[]>(this.apiUrl);
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
}