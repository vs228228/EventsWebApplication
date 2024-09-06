export interface BirthdayDto {
  year: number;
  month: number;
  day: number;
}

export interface User {
  id: number,
  name: string;
  surname: string;
  email: string;
  birthday: string
  isAdmin: boolean;
}

export interface UserRegistration {
  name: string;
  surname: string;
  email: string;
  password: string;
  birthday: BirthdayDto;
  isAdmin: boolean;
}
