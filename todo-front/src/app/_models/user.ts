import { TodoTask } from './model';

export interface CreateUser {
  firstName: string;
  lastName: string;
  userName: string;
  email: string;
  password: string;
}

export interface LoginUser {
  email: string;
  password: string;
}

export interface User {
  id: number;
  username: string;
  email: string;
  roles: string[];
  token: string;
}
