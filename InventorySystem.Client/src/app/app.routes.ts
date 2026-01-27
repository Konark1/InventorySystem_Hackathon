import { Routes } from '@angular/router';
import { adminGuard } from './auth/admin-guard';
import { LandingComponent } from './landing/landing.component';
import { AppComponent } from './app';

export const routes: Routes = [
  { 
    path: '', 
    component: LandingComponent // Landing page as home
  },
  { 
    path: 'login', 
    component: AppComponent // Your existing login/dashboard
  },
  { 
    path: 'register', 
    component: AppComponent // Your existing register
  },
  { 
    path: 'admin', 
    loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule),
    canActivate: [adminGuard]
  },
  {
    path: '**',
    redirectTo: ''
  }
];
