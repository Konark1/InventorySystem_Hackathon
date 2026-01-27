import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

interface User {
  id: string;
  email: string;
  fullName: string;
  shopName: string;
  phoneNumber: string;
  role: string;
  businessCategory: string;
  registeredDate: Date;
}

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {
  private apiUrl = 'https://inventory-api-konark-eac8bhaxg6hxhaec.centralindia-01.azurewebsites.net/api';
  
  totalShops: number = 0;
  totalItems: number = 0;
  users: User[] = [];
  filteredUsers: User[] = [];
  searchQuery: string = '';
  isLoading: boolean = false;
  selectedUser: User | null = null;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.loadAdminStats();
    this.loadUsers();
  }

  loadAdminStats() {
    this.isLoading = true;
    
    // This calls your secret Azure API endpoint
    this.http.get<any>(`${this.apiUrl}/admin/stats`).subscribe({
      next: (data) => {
        this.totalShops = data.shopCount || 0;
        this.totalItems = data.itemCount || 0;
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error loading admin stats:', error);
        // Use mock data if API fails
        this.totalShops = 12;
        this.totalItems = 245;
        this.isLoading = false;
      }
    });
  }

  loadUsers() {
    // Load all registered users
    this.http.get<User[]>(`${this.apiUrl}/admin/users`).subscribe({
      next: (data) => {
        this.users = data;
        this.filteredUsers = data;
      },
      error: (error) => {
        console.error('Error loading users:', error);
        // Mock data for demonstration
        this.users = [
          {
            id: '1',
            email: 'shop1@example.com',
            fullName: 'Rajesh Kumar',
            shopName: 'Kumar Electronics',
            phoneNumber: '9876543210',
            role: 'ShopOwner',
            businessCategory: 'Electronics',
            registeredDate: new Date('2024-01-15')
          },
          {
            id: '2',
            email: 'admin@example.com',
            fullName: 'Admin User',
            shopName: 'Admin Panel',
            phoneNumber: '9999999999',
            role: 'Admin',
            businessCategory: 'System',
            registeredDate: new Date('2024-01-01')
          },
          {
            id: '3',
            email: 'shop2@example.com',
            fullName: 'Priya Sharma',
            shopName: 'Sharma Grocery',
            phoneNumber: '9876543211',
            role: 'ShopOwner',
            businessCategory: 'Grocery',
            registeredDate: new Date('2024-02-20')
          }
        ];
        this.filteredUsers = this.users;
      }
    });
  }

  onSearch(): void {
    if (!this.searchQuery.trim()) {
      this.filteredUsers = this.users;
      return;
    }
    
    const query = this.searchQuery.toLowerCase();
    this.filteredUsers = this.users.filter(user =>
      user.fullName.toLowerCase().includes(query) ||
      user.email.toLowerCase().includes(query) ||
      user.shopName.toLowerCase().includes(query) ||
      user.businessCategory.toLowerCase().includes(query)
    );
  }

  viewUserDetails(user: User): void {
    this.selectedUser = user;
  }

  closeUserDetails(): void {
    this.selectedUser = null;
  }

  deleteUser(userId: string): void {
    if (confirm('Are you sure you want to delete this user? This action cannot be undone.')) {
      this.isLoading = true;
      
      this.http.delete(`${this.apiUrl}/admin/users/${userId}`).subscribe({
        next: () => {
          this.users = this.users.filter(u => u.id !== userId);
          this.filteredUsers = this.filteredUsers.filter(u => u.id !== userId);
          this.isLoading = false;
          alert('User deleted successfully');
        },
        error: (error) => {
          console.error('Error deleting user:', error);
          this.isLoading = false;
          alert('Error deleting user. Please try again.');
        }
      });
    }
  }

  logout() {
    localStorage.removeItem('token');
    window.location.href = '/';
  }
}
