export interface PaginatedNotifications {
  currentPage: number;
  items: Notification[];
  pageSize: number;
  totalCount: number;
  totalPages: number;
}

export interface Notification {
  id: number,
  message: string,
  createdAt: string
}
