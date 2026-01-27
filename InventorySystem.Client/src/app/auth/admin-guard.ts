import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth'; // Adjust path to your auth service

export const adminGuard = () => {
  const authService = inject(AuthService);
  const router = inject(Router);

  // 🧪 TEMPORARY: Bypass guard for testing - REMOVE IN PRODUCTION!
  const TESTING_MODE = false;
  if (TESTING_MODE) {
    return true; // Allow access to admin page for testing
  }

  const role = authService.getUserRole(); // Create this method to read role from JWT

  if (role === 'Admin') {
    return true;
  } else {
    router.navigate(['/']); // Redirect to home page to hide admin portal
    return false;
  }
};
