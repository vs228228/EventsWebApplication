import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { Notification } from '../../models/notification.model';
import { UserService } from '../../services/user-service/user.service';

@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrl: './notifications.component.css'
})
export class NotificationsComponent implements OnInit {
  notifications: Notification[] = [];
  totalNotifications = 0;
  pageSize = 5;

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.loadNotifications(1, this.pageSize);
  }

  async loadNotifications(pageIndex: number, pageSize: number): Promise<void> {

    this.userService.getNotifications(pageIndex, pageSize)
      .then(notifications => {
        this.notifications = notifications.items;
        this.totalNotifications = notifications.totalPages;
      })
  }

  async removeNotification(notification: Notification): Promise<void> {
    await this.userService.deleteNotification(notification.id);

    this.notifications = this.notifications.filter(n => n.id !== notification.id);
    console.log(`Уведомление с ID ${notification.id} удалено`);

  }

  onPageChange(event: PageEvent): void {
    this.loadNotifications(event.pageIndex, event.pageSize);
  }
}
