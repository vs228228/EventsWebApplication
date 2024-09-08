import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { EventToServer } from '../../models/event.model';
import { EventService } from '../../services/event-service/event.service';

@Component({
  selector: 'app-edit-event',
  templateUrl: './edit-event.component.html',
  styleUrls: ['./edit-event.component.css']
})
export class EditEventComponent {
  event: EventToServer;
  selectedFile: File | null = null;

  constructor(
    private eventService: EventService,
    public dialogRef: MatDialogRef<EditEventComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { event: EventToServer }
  ) {
    this.event = { ...data.event };
  }

   saveEvent() {
    if (!this.event) {
      console.error('Event is not initialized');
      return;
    }

    const formData = new FormData();
    formData.append('id', this.event.id.toString());
    formData.append('title', this.event.title);
    formData.append('description', this.event.description);
    formData.append('dateAndTime', this.event.dateAndTime);
    formData.append('place', this.event.place);
    formData.append('maxParticipants', this.event.maxParticipants.toString());
    formData.append('type', this.event.type);

    if (this.selectedFile) {
      formData.append('photo', this.selectedFile, this.selectedFile.name);
    }

    this.eventService.updateEvent(formData).subscribe({
      next: () => {
        console.log('Мероприятие обновлено успешно');
        alert("Мероприятие обновлено успешно");
        this.closeDialog();
      },
      error: (error) => {
        console.error('Error updating event:', error);
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
