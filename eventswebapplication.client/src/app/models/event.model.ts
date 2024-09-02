
export interface PaginatedEvents {
  currentPage: number;
  items: Event[];
  pageSize: number;
  totalCount: number;
  totalPages: number;
}

export interface Event {
  id: number;
  title: string;
  description: string;
  dateAndTime: string;
  place: string;
  imagePath: string;
  maxParticipants: number;
  countOfParticipants: number;
  type: string;
}
