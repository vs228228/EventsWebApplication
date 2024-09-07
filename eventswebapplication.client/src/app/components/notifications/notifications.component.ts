import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { Notification } from '../../models/notification.model';
import { UserService } from '../../services/user-service/user.service';

@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrls: ['./notifications.component.css']
})
export class NotificationsComponent implements OnInit {
  notifications: Notification[] = [];
  totalNotifications = 0; 
  pageSize = 5; 
  pageIndex = 0; 

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.loadNotifications(this.pageIndex, this.pageSize);
  }

  async loadNotifications(pageIndex: number, pageSize: number): Promise<void> {
    const serverPageIndex = pageIndex + 1;

    // Получаем уведомления с сервера
    this.userService.getNotifications(serverPageIndex, pageSize)
      .then(response => {
        this.notifications = response.items;
        this.totalNotifications = response.totalCount;
      })
      .catch(error => {
        console.error('Ошибка загрузки уведомлений:', error);
      });
  }

  async removeNotification(notification: Notification): Promise<void> {
    await this.userService.deleteNotification(notification.id);

    this.notifications = this.notifications.filter(n => n.id !== notification.id);

    console.log(`Уведомление с ID ${notification.id} удалено`);

    this.loadNotifications(this.pageIndex, this.pageSize);
  }

  onPageChange(event: PageEvent): void {
    this.pageIndex = event.pageIndex; 
    this.loadNotifications(this.pageIndex, event.pageSize);
  }
}
