import { Component, OnInit } from '@angular/core';
import { Event } from '../../models/event.model';
import { UserService } from '../../services/user-service/user.service';

@Component({
  selector: 'app-user-events',
  templateUrl: './user-events.component.html',
  styleUrl: './user-events.component.css'
})
export class UserEventsComponent implements OnInit {
  events: Event[] = [];

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.loadUserEvents();
  }

  loadUserEvents(): void {
    var id = localStorage.getItem(`userId`);
    this.userService.getRegisteredEvent(Number(id))
      .then(events => {
        this.events = events;
      })
  }

  getImageUrl(filename: string): string {
    return `https://localhost:7059/${filename.substring(1)}`;
  }

  openEvent(eventId: number) {
    
  }
}
