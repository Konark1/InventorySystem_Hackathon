import { Routes } from '@angular/router';
import { adminGuard } from './auth/admin-guard';
import { LandingComponent } from './landing/landing.component';
import { AppComponent } from './app';
import { AdminLoginComponent } from './admin/admin-login.component';
import { AdminDashboardComponent } from './admin/admin-dashboard.component';

export const routes: Routes = [
  { 
    path: '', 
    component: LandingComponent
  },
  { 
    path: 'login', 
    component: AppComponent
  },
  { 
    path: 'register', 
    component: AppComponent
  },
  // Hidden admin routes - no links, only accessible by direct URL
  { 
    path: 'admin/login', 
    component: AdminLoginComponent
  },
  { 
    path: 'admin/dashboard', 
    component: AdminDashboardComponent,
    canActivate: [adminGuard]
  },
  { 
    path: 'admin', 
    redirectTo: 'admin/login',
    pathMatch: 'full'
  },
  {
    path: '**',
    redirectTo: ''
  }
];
