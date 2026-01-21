import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common'; 
import { FormsModule } from '@angular/forms'; 
import { InventoryService, InventoryItem } from './services/inventory';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class AppComponent implements OnInit {
  items: InventoryItem[] = [];
  
  // Variables for the "Add New Item" form
  newItemName: string = '';
  newItemQty: number = 0;
  newItemThreshold: number = 5;

  constructor(private inventoryService: InventoryService) {}

  ngOnInit() {
    this.loadItems();
  }

  // 1. Fetch Data
  loadItems() {
    this.inventoryService.getItems().subscribe(data => {
      this.items = data;
    });
  }

  // 2. Add Item
  addNewItem() {
    const newItem = {
      name: this.newItemName,
      quantity: this.newItemQty,
      lowStockThreshold: this.newItemThreshold
    };

    this.inventoryService.addItem(newItem).subscribe(() => {
      this.loadItems(); 
      // Reset form
      this.newItemName = '';
      this.newItemQty = 0;
    });
  }

  // 3. Update Stock (+ or -)
  updateStock(id: number, change: number) {
    this.inventoryService.updateStock(id, change).subscribe(() => {
      this.loadItems();
    });
  }

  // 4. Delete Item
  deleteItem(id: number) {
    if (confirm('Are you sure you want to delete this item?')) {
      this.inventoryService.deleteItem(id).subscribe(() => {
        this.loadItems();
      });
    }
  }
}