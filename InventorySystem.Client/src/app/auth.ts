import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  // Swagger URL 
  private apiUrl = 'https://inventory-api-konark-eac8bhaxg6hxhaec.centralindia-01.azurewebsites.net/api/auth'; 

  constructor(private http: HttpClient) { }

  // 1. REGISTER
  register(user: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, user);
  }

  // 2. LOGIN (and save token!)
  login(credentials: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, credentials).pipe(
      tap((response: any) => {
        if (response.token) {
          // Save the "Key Card" in the browser
          localStorage.setItem('token', response.token);
        }
      })
    );
  }

  // 3. LOGOUT
  logout() {
    localStorage.removeItem('token');
  }

  // 4. CHECK IF LOGGED IN (Do we have a token?)
  isLoggedIn(): boolean {
    return !!localStorage.getItem('token'); 
  }

  // 5. GET THE TOKEN (Read the card)
  getToken(): string | null {
    return localStorage.getItem('token');
  }
}
