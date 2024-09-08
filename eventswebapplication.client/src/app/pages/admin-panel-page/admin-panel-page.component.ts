import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { EventService } from '../../services/event-service/event.service';
import { Event as MyEvent } from '../../models/event.model';
import { MatDialog } from '@angular/material/dialog';
import { CreateEventComponent } from '../../components/create-event/create-event.component';
import { EditEventComponent } from '../../components/edit-event/edit-event.component';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-admin-panel-page',
  templateUrl: './admin-panel-page.component.html',
  styleUrls: ['./admin-panel-page.component.css']
})
export class AdminPanelPageComponent implements OnInit {
  events: any[] = [];
  pageSize: number = 10;
  pageNumber: number = 1;
  pageSizeOptions: number[] = [6, 10, 20];
  totalEvents: number = 0;
  searchId: string = '';
  selectedEvent: any = null;
  showCreateEventDialog: boolean = false;

  constructor(private eventService: EventService, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.loadEvents();
  }

  loadEvents(pageNumber: number = this.pageNumber, pageSize: number = this.pageSize): void {
    this.eventService.getEvents(pageNumber, pageSize, this.searchId).subscribe(
      (response: any) => {
        const data = response.result;
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

  async searchEvents() {
    if (this.searchId != "") {
      try {
        let ans = await this.eventService.getEventById(this.searchId);
        this.events = [];
        this.events.push(ans);
      }
      catch (err) {
        alert("Мероприятия с таким id не существет");
      }
    }
    else {
      this.loadEvents();
    }
  }

  openEditEventDialog(event: MyEvent): void {
    const dialogRef = this.dialog.open(EditEventComponent, {
      data: { event }
    });

    dialogRef.afterClosed().subscribe(() => {
      // Reload events when the dialog is closed
      this.loadEvents(this.pageNumber, this.pageSize);
    });
  }
  openCreateEventDialog(): void {
    const dialogRef = this.dialog.open(CreateEventComponent, {
      
    });

    dialogRef.afterClosed().subscribe(() => {
      this.loadEvents(this.pageNumber, this.pageSize);
    });
  }

  deleteEvent(eventId: number): void {
    console.log(eventId);
    this.eventService.deleteEvent(eventId).subscribe({
      next: () => {
        console.log('Мероприятие удалено успешно');
        alert("Мероприятие удалено успешно");
        this.loadEvents(this.pageNumber, this.pageSize);
      },
      error: (error) => {
        console.error('Error deleting event:', error);
      }
    });
    this.loadEvents(this.pageNumber, this.pageSize);

  }
}
