import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common'; 
import { FormsModule } from '@angular/forms'; 
import { Router } from '@angular/router';
import { InventoryService, InventoryItem, InventoryTransaction } from './services/inventory';
import { AuthService } from './auth';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class AppComponent implements OnInit {
  // Authentication State
  isAuthenticated: boolean = false;
  isLoginMode: boolean = true;
  
  // Auth Data
  authData = { 
    email: '', 
    password: '', 
    shopName: '',
    // NEW
    fullName: '',
    phoneNumber: '',
    physicalAddress: '',
    aadhaarNumber: '',
    businessCategory: '',
    age: 0 // Initialize with 0 instead of null
  };
  
  items: InventoryItem[] = [];
  
  // Variables for the "Add New Item" form
  newItem: any = { 
    name: '', 
    quantity: 0,
    lowStockThreshold: 5,
    price: 0 // 👈 Initialize it with 0
  };

  // Search and Low Stock Variables
  searchQuery: string = '';
  lowStockCount: number = 0;

  // Loading State
  isLoading: boolean = false;

  // History Variables
  selectedHistory: InventoryTransaction[] = [];
  selectedItemName: string = '';

  constructor(
    private inventoryService: InventoryService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit() {
    // Check current route to determine if register or login
    const currentUrl = this.router.url;
    if (currentUrl.includes('/register')) {
      this.isLoginMode = false; // Show register form
    } else if (currentUrl.includes('/login')) {
      this.isLoginMode = true; // Show login form
    }
    
    // Check if already logged in
    this.isAuthenticated = this.authService.isLoggedIn();
    
    // If logged in, load data immediately
    if (this.isAuthenticated) {
      this.loadItems();
    }
  }

  // --- AUTH METHODS ---
  onAuthSubmit() {
    this.isLoading = true; // ⏳ START SPINNER

    if (this.isLoginMode) {
      this.authService.login(this.authData).subscribe({
        next: () => {
          this.isAuthenticated = true;
          
          // Check user role and redirect accordingly
          const role = this.authService.getUserRole();
          
          if (role === 'Admin') {
            // Admin user - redirect to admin dashboard
            this.router.navigate(['/admin']);
          } else {
            // Regular shop owner - stay on inventory dashboard
            this.loadItems();
          }
          
          // ✅ SUCCESS POPUP
          Swal.fire({
            title: 'Welcome Back!',
            text: 'Login successful.',
            icon: 'success',
            timer: 1500,
            showConfirmButton: false
          });
          this.isLoading = false;
        },
        error: (err) => {
          this.isLoading = false;
          
          // ❌ ERROR POPUP
          Swal.fire({
            title: 'Login Failed',
            text: 'Please check your email or password.',
            icon: 'error',
            confirmButtonText: 'Try Again'
          });
        }
      });
    } else {
      // REGISTER LOGIC
      this.authService.register(this.authData).subscribe({
        next: () => {
          this.isLoading = false; // ✅ STOP SPINNER
          
          // ✅ SUCCESS REGISTER
          Swal.fire({
            title: 'Account Created!',
            text: 'Please login with your new account.',
            icon: 'success',
            confirmButtonText: 'Okay'
          });
          this.isLoginMode = true;
        },
        error: (err) => {
          this.isLoading = false; // ✅ STOP SPINNER
          
          Swal.fire({
            title: 'Registration Failed',
            text: 'Something went wrong. Try a different email.',
            icon: 'error'
          });
        }
      });
    }
  }

  logout() {
    this.authService.logout();
    this.isAuthenticated = false;
    this.items = [];
    this.lowStockCount = 0;
  }

  // --- INVENTORY METHODS ---
  loadItems() {
    this.isLoading = true; // ⏳ START SPINNER

    this.inventoryService.getItems(this.searchQuery).subscribe({
      next: (data) => {
        this.items = data;
        
        // Calculate Low Stock Count (items with quantity < 5)
        this.lowStockCount = this.items.filter(i => i.quantity < 5).length;
        
        this.isLoading = false; // ✅ STOP SPINNER
      },
      error: (err) => {
        console.error('Failed to load items', err);
        this.isLoading = false; // ✅ STOP SPINNER
      }
    });
  }

  // Triggered when user types in search
  onSearch() {
    this.loadItems();
  }

  // 2. Add Item
  addNewItem() {
    this.inventoryService.addItem(this.newItem).subscribe(() => {
      this.loadItems(); 
      // Reset form
      this.newItem = {
        name: '',
        quantity: 0,
        lowStockThreshold: 5,
        price: 0
      };
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

  // 5. View History
  viewHistory(item: InventoryItem) {
    this.selectedItemName = item.name;
    
    this.inventoryService.getItemHistory(item.id).subscribe(data => {
      this.selectedHistory = data;
      
      // Optional: Scroll to the history section
      setTimeout(() => {
        document.getElementById('history-section')?.scrollIntoView({ behavior: 'smooth' });
      }, 100);
    });
  }

  // Helper methods for status display
  getStatusClass(item: InventoryItem): string {
    if (item.quantity === 0) return 'status-out-of-stock';
    if (item.quantity <= item.lowStockThreshold) return 'status-low-stock';
    return 'status-in-stock';
  }

  getStatusText(item: InventoryItem): string {
    if (item.quantity === 0) return '❌ Out of Stock';
    if (item.quantity <= item.lowStockThreshold) return '⚠️ Low Stock';
    return '✅ In Stock';
  }
}