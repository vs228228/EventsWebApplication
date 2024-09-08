import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { EventService } from '../../services/event-service/event.service';
import { Event } from '../../models/event.model';

@Component({
  selector: 'app-event-page',
  templateUrl: './event-page.component.html',
  styleUrls: ['./event-page.component.css']
})
export class EventPageComponent implements OnInit {
  event: Event | null = null;
  isLoading = true;
  errorMessage = '';
  eventId: number | undefined;
  isUserLoggedIn = false;
  isUserRegistered = false;

  constructor(
    private route: ActivatedRoute,
    private eventService: EventService,
    private router: Router
  ) { }

  ngOnInit(): void {
    const currentEventId = this.route.snapshot.paramMap.get('id');
    this.eventId = Number(currentEventId);
    this.loadEvent();
    this.checkUserRegistration();
  }

  async loadEvent(): Promise<void> {
    if (this.eventId) {
      try {
        this.event = await this.eventService.getEventById(this.eventId.toString());
      } catch (error) {
        this.errorMessage = 'Error loading event details. Please try again later.';
        console.error('Error:', error);
      } finally {
        this.isLoading = false;
      }
    }
  }

  getImageUrl(filename: string): string {
    return `https://localhost:7059/${filename.substring(1)}`;
  }

  async checkUserRegistration(): Promise<void> {
    const userId = localStorage.getItem('userId');
    if (userId && this.eventId) {
      const isRegistered = await this.eventService.checkUserRegistration(this.eventId, Number(userId));
      this.isUserRegistered = isRegistered;
    }
  }

  async registerForEvent(id: number): Promise<void> {
    const isLoggedIn = localStorage.getItem('isLoggedIn');
    if (!isLoggedIn) {
      this.router.navigate(['/auth']);
    } else {
      await this.eventService.registerUserForEvent(id);
      this.isUserRegistered = true; 
    }
  }

  async unregisterFromEvent(id: number): Promise<void> {
    await this.eventService.unregisterUserFromEvent(id);
    this.isUserRegistered = false; 
  }

  hasEventPassed(): boolean {
    if (this.event) {
      const now = new Date();
      const eventDate = new Date(this.event.dateAndTime);
      return eventDate < now;
    }
    return false;
  }
}
