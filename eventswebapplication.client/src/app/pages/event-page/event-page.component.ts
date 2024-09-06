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
  eventId: number | undefined;

  constructor(
    private route: ActivatedRoute,
    private eventService: EventService
  ) { }

  ngOnInit(): void {
    this.loadEvent();
  }

  async loadEvent(): Promise<void> {
    const currentEventId = this.route.snapshot.paramMap.get('id');
    this.eventId = Number(currentEventId);
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
}
