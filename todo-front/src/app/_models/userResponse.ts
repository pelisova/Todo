import { TodoTask } from './model';

export type UserRegister = {
  message: string | undefined;
  data: Data;
};

export type Data = {
  userName: string;
  email: string;
};

export type UserLogin = {
  message: string | undefined;
  data: UserData;
};

export type UserData = {
  id: number;
  username: string;
  email: string;
  roles: string[];
  token: string;
};
