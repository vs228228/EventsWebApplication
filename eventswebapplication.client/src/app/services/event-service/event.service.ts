// src/app/services/event.service.ts
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PaginatedEvents } from '../../models/event.model';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  private apiUrl = 'https://localhost:7059/api/Events';  // Замените на ваш URL

  constructor(private http: HttpClient) { }

  getEvents(pageNumber: number, pageSize: number): Observable<PaginatedEvents> {
    const params = new HttpParams()
      .set('pageNumber', pageNumber.toString())
      .set('pageSize', pageSize.toString());

    let ans = this.http.get<PaginatedEvents>(this.apiUrl, { params });
    return ans;
  }
}
