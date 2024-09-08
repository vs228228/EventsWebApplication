import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { BehaviorSubject, firstValueFrom, Observable } from 'rxjs';
import { AuthResponse } from '../../models/authRespons.model';
import { Router } from '@angular/router';
import { User, UserRegistration } from '../../models/user.model';
import { DatePipe } from '@angular/common';

@Injectable({
  providedIn: 'root',
})

export class AuthService {

  private apiUrl = 'https://localhost:7059/api/User';

  private loggedIn = new BehaviorSubject<boolean>(false);

  isLoggedIn$ = this.loggedIn.asObservable();
  constructor(private http: HttpClient, private router: Router) { }

  async tryEnterToSystem(userEmail: String, userPassword: String): Promise<boolean> {
    var url = this.apiUrl + `/authByPassword`
    const loginData = { userEmail, userPassword };
    try {
      var ans = await firstValueFrom(this.http.post<AuthResponse>(url, loginData));
      localStorage.setItem('refreshToken', ans.refreshToken);
      localStorage.setItem('accessToken', ans.accessToken);
      localStorage.setItem('isLoggedIn', 'true');
      const newUrl = `${this.apiUrl}/getByEmail/${encodeURIComponent(String(userEmail))}`
      var user = await firstValueFrom(this.http.get<User>(newUrl));
      console.log(user);
      localStorage.setItem('userId', user.id.toString());
      localStorage.setItem('isAdmin', user.isAdmin.toString());
      this.loggedIn.next(true);
      this.router.navigate(['']);
      return true;
    }
    catch (error) {
      console.log(error);
      return false;
    }
  }

  async registerUser(userData: UserRegistration): Promise<boolean> {
    try {
      var response = firstValueFrom(this.http.post<void>(this.apiUrl, userData));

      return true;
    } catch (error) {
      console.error('Ошибка при регистрации:', error);
      return false;
    }
  }

  async getAccessToken(): Promise<string | null> {
    return new Promise((resolve) => {
      const token = localStorage.getItem('accessToken');
      resolve(token);
    });
  }

  async refreshTokens(): Promise<boolean> {
    const refreshToken = localStorage.getItem('refreshToken');

    if (!refreshToken) {
      return false;
    }

    try {
      const response = await firstValueFrom(this.http.post<AuthResponse>(this.apiUrl + `/refreshToken`, { refreshToken }));

      localStorage.setItem('refreshToken', response.refreshToken);
      localStorage.setItem('accessToken', response.accessToken);

      return true;
    } catch (error) {
      console.error('Ошибка при обновлении токенов:', error);
      return false;
    }
  }

  async logout() {
    localStorage.removeItem('isLoggedIn');
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
    localStorage.removeItem('userId');
    localStorage.removeItem('isAdmin');
    this.loggedIn.next(false);
  }

  async checkLoginStatus() {
    const isLoggedIn = localStorage.getItem('isLoggedIn') === 'true';
    this.loggedIn.next(isLoggedIn);
  }
}
