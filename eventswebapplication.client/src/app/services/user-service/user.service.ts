import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../../models/user.model';
import { firstValueFrom } from 'rxjs';
import { PaginatedNotifications } from '../../models/notification.model';
import { Event } from '../../models/event.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private apiUrl = 'https://localhost:7059/api/User';

  constructor(private http: HttpClient, private router: Router) { }

  async getUserById(id: number): Promise<User> {
    const data = { id }
    var url = `${this.apiUrl}/${encodeURIComponent(String(id))}`
    return await firstValueFrom(this.http.get<User>(url));
  }

  async getUserByEmail(email: string): Promise<User> {
    const url = `${this.apiUrl}/getByEmail/${encodeURIComponent(String(email))}`
    var user = await firstValueFrom(this.http.get<User>(url));
    return await firstValueFrom(this.http.get<User>(url));
  }

  async getRegisteredEvent(userId: number): Promise<Event[]> {
    var url = `${this.apiUrl}/getRegisteredEvent/${encodeURIComponent(String(userId))}`;

    let ans = firstValueFrom(this.http.get<Event[]>(url));
    return ans;
  }

  async getNotifications(pageNumber: number, pageSize: number): Promise<PaginatedNotifications> {
    var  id = localStorage.getItem(`userId`);
    const params = new HttpParams()
      .set(`userId`, Number(id))
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());

    const url = `${this.apiUrl}/notification/`;

    let ans = firstValueFrom(this.http.get<PaginatedNotifications>(url, { params }));
    return ans;
  }

  async deleteNotification(id: number) {
    try {
      const params = new HttpParams().set('notificationId', id.toString());
      const url = `${this.apiUrl}/notification`;
      var ans = await this.http.delete(url, { params }).toPromise();
      console.log('Notification deleted:', ans);
    } catch (error) {
      console.error('Error deleting notification:', error);
    }
  }

}
