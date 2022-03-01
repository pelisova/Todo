import { Injectable } from '@angular/core';
import { CreateUser } from '../models/user';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  users: CreateUser[] = [
    new CreateUser('vaso', 'vaso@gmail.com', 'vaso123', 'vaso123'),
    new CreateUser('vaso1', 'vaso@gmail.com', 'vaso123', 'vaso123'),
    new CreateUser('vaso2', 'vaso@gmail.com', 'vaso123', 'vaso123'),
  ];

  constructor() {}

  createUser(user: CreateUser) {
    this.users.push(user);
  }

  getAll() {
    return this.users;
  }
}
