import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth'; // Adjust path to your auth service

export const adminGuard = () => {
  const authService = inject(AuthService);
  const router = inject(Router);

  const role = authService.getUserRole(); // Create this method to read role from JWT

  if (role === 'Admin') {
    return true;
  } else {
    router.navigate(['/dashboard']); // Kick them to their shop dashboard
    return false;
  }
};
