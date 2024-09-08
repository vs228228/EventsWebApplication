// src/app/services/event.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EventToServer, PaginatedEvents } from '../../models/event.model';
import { firstValueFrom } from 'rxjs';
import { Event } from '../../models/event.model';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  private apiUrl = 'https://localhost:7059/api/Events';

  constructor(private http: HttpClient) { }

  updateEvent(formData: FormData){
    return this.http.put<void>(this.apiUrl, formData);
  }

  getEvents(pageNumber: number, pageSize: number, searchString: string = ''): Observable<PaginatedEvents> {
    var params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString())
      .set('searchString', searchString);
 

    let ans = this.http.get<PaginatedEvents>(this.apiUrl, { params });
    return ans;
  }

  createEvent(formData: FormData){
    return this.http.post<void>(this.apiUrl, formData);
  }

  deleteEvent(id: number) {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  async getEventById(eventId: string): Promise<Event> {
    const url = `${this.apiUrl}/${eventId}`;
    return firstValueFrom(this.http.get<Event>(url))

  }

  async registerUserForEvent(eventId: number): Promise<void> {
    const userId = localStorage.getItem('userId');
    await this.http.post(`${this.apiUrl}/registerForEvent`, { userId, eventId }).toPromise();
  }

  async unregisterUserFromEvent(eventId: number): Promise<void> {
    const userId = localStorage.getItem('userId');
    await this.http.post(`${this.apiUrl}/unregisterFromEvent`, { userId, eventId }).toPromise();
  }

  async checkUserRegistration(eventId: number, userId: number): Promise<boolean> {
    try {
     /*const params = { userId, eventId };
      *//*var params = new HttpParams()
        .set(`userId`, userId)
        .set(`eventId`, eventId)*/
      const ans = await firstValueFrom(
        this.http.post<boolean>(`${this.apiUrl}/isUserRegisterToEvent`, { userId, eventId })
      );
      return ans !== undefined ? ans : false;
    } catch (error) {
      console.error('Ошибка при проверке регистрации пользователя:', error);
      return false;
    }
  }

}
