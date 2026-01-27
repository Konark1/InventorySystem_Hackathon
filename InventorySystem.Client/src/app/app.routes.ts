import { Routes } from '@angular/router';
import { adminGuard } from './auth/admin-guard';

export const routes: Routes = [
  { 
    path: 'admin', 
    loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule),
    canActivate: [adminGuard] // <--- The Bouncer is now on duty!
  },
  // your other routes...
];
