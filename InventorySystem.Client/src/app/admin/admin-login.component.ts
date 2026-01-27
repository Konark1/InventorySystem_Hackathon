import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../auth';

@Component({
  selector: 'app-admin-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div class="admin-login-container">
      <div class="admin-login-card">
        <div class="admin-header">
          <div class="shield-icon">🛡️</div>
          <h1>System Administration</h1>
          <p class="warning">⚠️ Authorized Personnel Only</p>
        </div>

        <form (ngSubmit)="login()" class="admin-login-form">
          <div class="form-group">
            <label for="email">Administrator Email</label>
            <input
              type="email"
              id="email"
              [(ngModel)]="credentials.email"
              name="email"
              placeholder="admin@inventory.com"
              required
              autocomplete="off"
            />
          </div>

          <div class="form-group">
            <label for="password">Secure Password</label>
            <input
              type="password"
              id="password"
              [(ngModel)]="credentials.password"
              name="password"
              placeholder="Enter secure password"
              required
              autocomplete="off"
            />
          </div>

          <div *ngIf="errorMessage" class="error-message">
            {{ errorMessage }}
          </div>

          <button type="submit" class="admin-login-btn" [disabled]="isLoading">
            <span *ngIf="!isLoading">Access Control Panel</span>
            <span *ngIf="isLoading">Authenticating...</span>
          </button>
        </form>

        <div class="security-notice">
          🔒 All access attempts are monitored and logged for security purposes.
        </div>
      </div>
    </div>
  `,
  styles: [`
    .admin-login-container {
      min-height: 100vh;
      display: flex;
      align-items: center;
      justify-content: center;
      background: linear-gradient(135deg, #1a1a2e 0%, #16213e 100%);
      padding: 20px;
    }

    .admin-login-card {
      background: #0f0f23;
      border: 1px solid #2a2a4e;
      border-radius: 16px;
      padding: 48px;
      max-width: 480px;
      width: 100%;
      box-shadow: 0 20px 60px rgba(0, 0, 0, 0.5);
    }

    .admin-header {
      text-align: center;
      margin-bottom: 40px;
    }

    .shield-icon {
      font-size: 56px;
      margin-bottom: 20px;
      opacity: 0.9;
      animation: pulse 2s ease-in-out infinite;
    }

    @keyframes pulse {
      0%, 100% { transform: scale(1); }
      50% { transform: scale(1.05); }
    }

    .admin-header h1 {
      font-size: 32px;
      font-weight: 700;
      color: #e0e0e0;
      margin: 0 0 12px 0;
      letter-spacing: -0.5px;
    }

    .warning {
      color: #ff6b6b;
      font-size: 14px;
      font-weight: 600;
      margin: 0;
      display: flex;
      align-items: center;
      justify-content: center;
      gap: 6px;
    }

    .admin-login-form {
      margin-bottom: 32px;
    }

    .form-group {
      margin-bottom: 24px;
    }

    .form-group label {
      display: block;
      font-weight: 600;
      color: #b0b0b0;
      margin-bottom: 10px;
      font-size: 13px;
      text-transform: uppercase;
      letter-spacing: 0.8px;
    }

    .form-group input {
      width: 100%;
      padding: 16px 18px;
      background: #1a1a2e;
      border: 2px solid #2a2a4e;
      border-radius: 10px;
      font-size: 15px;
      color: #e0e0e0;
      transition: all 0.3s ease;
      box-sizing: border-box;
      font-family: inherit;
    }

    .form-group input:focus {
      outline: none;
      border-color: #4a90e2;
      background: #16213e;
      box-shadow: 0 0 0 4px rgba(74, 144, 226, 0.1);
    }

    .form-group input::placeholder {
      color: #666;
    }

    .error-message {
      background: rgba(255, 107, 107, 0.15);
      color: #ff6b6b;
      padding: 14px 18px;
      border-radius: 10px;
      margin-bottom: 24px;
      font-size: 14px;
      border: 1px solid rgba(255, 107, 107, 0.3);
      display: flex;
      align-items: center;
      gap: 10px;
    }

    .error-message::before {
      content: '⚠️';
      font-size: 18px;
    }

    .admin-login-btn {
      width: 100%;
      padding: 18px;
      background: linear-gradient(135deg, #4a90e2 0%, #357abd 100%);
      color: white;
      border: none;
      border-radius: 10px;
      font-size: 16px;
      font-weight: 700;
      cursor: pointer;
      transition: all 0.3s ease;
      text-transform: uppercase;
      letter-spacing: 1px;
      box-shadow: 0 8px 24px rgba(74, 144, 226, 0.3);
    }

    .admin-login-btn:hover:not(:disabled) {
      background: linear-gradient(135deg, #357abd 0%, #2868a8 100%);
      box-shadow: 0 12px 32px rgba(74, 144, 226, 0.4);
      transform: translateY(-2px);
    }

    .admin-login-btn:disabled {
      opacity: 0.5;
      cursor: not-allowed;
      transform: none;
    }

    .security-notice {
      margin-top: 32px;
      padding: 18px;
      background: rgba(74, 144, 226, 0.08);
      border-left: 4px solid #4a90e2;
      border-radius: 8px;
      font-size: 12px;
      color: #b0b0b0;
      text-align: center;
      line-height: 1.6;
    }
  `]
})
export class AdminLoginComponent {
  credentials = {
    email: '',
    password: ''
  };
  errorMessage = '';
  isLoading = false;

  constructor(
    private http: HttpClient,
    private router: Router,
    private authService: AuthService
  ) {}

  login() {
    this.errorMessage = '';
    this.isLoading = true;

    const apiUrl = 'https://inventory-api-konark-eac8bhaxg6hxhaec.centralindia-01.azurewebsites.net/api/auth/login';

    this.http.post<{ token: string }>(apiUrl, this.credentials).subscribe({
      next: (response) => {
        localStorage.setItem('token', response.token);
        
        // Verify admin role
        const role = this.authService.getUserRole();
        
        if (role === 'Admin') {
          this.router.navigate(['/admin/dashboard']);
        } else {
          this.errorMessage = 'Access Denied: Insufficient privileges';
          localStorage.removeItem('token');
          this.isLoading = false;
        }
      },
      error: (error) => {
        console.error('Authentication failed:', error);
        this.errorMessage = 'Authentication failed. Access denied.';
        this.isLoading = false;
      }
    });
  }
}
