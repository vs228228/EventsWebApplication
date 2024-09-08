import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { EventService } from '../../services/event-service/event.service';

@Component({
  selector: 'app-create-event',
  templateUrl: './create-event.component.html',
  styleUrls: ['./create-event.component.css']
})
export class CreateEventComponent {
  event = {
    title: '',
    description: '',
    dateAndTime: '',
    place: '',
    maxParticipants: 0,
    type: '',
    imageFile: null
  };
  selectedFile: File | null = null;

  constructor(
    private eventService: EventService,
    public dialogRef: MatDialogRef<CreateEventComponent>
  ) { }

  saveEvent(): void {
    if (!this.event) {
      console.error('Event is not initialized');
      return;
    }

    const formData = new FormData();
    formData.append('title', this.event.title);
    formData.append('description', this.event.description);
    formData.append('dateAndTime', this.event.dateAndTime);
    formData.append('place', this.event.place);
    formData.append('maxParticipants', this.event.maxParticipants.toString());
    formData.append('type', this.event.type);

    if (this.selectedFile) {
      formData.append('photo', this.selectedFile, this.selectedFile.name);
    }

    this.eventService.createEvent(formData).subscribe({
      next: () => {
        console.log('Мерприятие создано успешно');
        alert("Мерприятие создано успешно");
        this.closeDialog();
      },
      error: (error) => {
        console.error('Error creating event:', error);
      }
    });
  }

  closeDialog(): void {
    this.dialogRef.close();
  }

  onFileChange(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files[0]) {
      this.selectedFile = input.files[0];
    }
  }
}
