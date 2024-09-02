import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
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

  constructor(
    private route: ActivatedRoute,
    private eventService: EventService
  ) { }

  ngOnInit(): void {
    this.loadEvent();
  }

  async loadEvent(): Promise<void> {
    const eventId = this.route.snapshot.paramMap.get('id');
    if (eventId) {
      try {
        this.event = await this.eventService.getEventById(eventId);
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
}
