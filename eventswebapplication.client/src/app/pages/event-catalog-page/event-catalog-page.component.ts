// src/app/pages/event-catalog-page/event-catalog-page.component.ts
import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { PaginatedEvents } from '../../models/event.model';
import { EventService } from '../../services/event-service/event.service';

@Component({
  selector: 'app-event-catalog-page',
  templateUrl: './event-catalog-page.component.html',
  styleUrls: ['./event-catalog-page.component.css']
})
export class EventCatalogPageComponent implements OnInit {
  events: any[] = [];
  pageSize: number = 10;
  pageNumber: number = 1;
  pageSizeOptions: number[] = [6, 10, 20];
  totalEvents: number = 0;
  searchString: string = ``;

  constructor(private eventService: EventService) { }

  ngOnInit(): void {
    this.loadEvents();
  }

  loadEvents(pageNumber: number = this.pageNumber, pageSize: number = this.pageSize): void {
    console.log(this.searchString);
    this.eventService.getEvents(pageNumber, pageSize, this.searchString).subscribe(
      (response: any) => {
        var data = response.result;
        this.events = data.items;
        this.totalEvents = data.totalCount;
      },
      error => console.error('Error fetching events', error)
    );
  }

  getImageUrl(filename: string): string {
    return `https://localhost:7059/${filename.substring(1)}`;
  }

  onPageChanged(event: PageEvent): void {
    this.pageNumber = event.pageIndex + 1;
    this.pageSize = event.pageSize;
    this.loadEvents(this.pageNumber, this.pageSize);
  }

  searchEvents(): void {
    this.pageNumber = 1;
    this.loadEvents(this.pageNumber, this.pageSize);
  }
}
