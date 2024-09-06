import { Component, OnInit } from '@angular/core';
import { EventService } from '../../services/event-service/event.service';
import { Event } from '../../models/event.model';

@Component({
  selector: 'app-user-events',
  templateUrl: './user-events.component.html',
  styleUrl: './user-events.component.css'
})
export class UserEventsComponent implements OnInit {
  events: Event[] = [];

  constructor(private eventService: EventService) { }

  ngOnInit(): void {
    this.loadUserEvents();
  }

  loadUserEvents(): void {
    
  }

  getImage(imagePath: string): void {

  }
}
