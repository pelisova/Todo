import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { TodoTask } from '../_models/model';
import { UserDto } from '../_models/user';

@Injectable({
  providedIn: 'root',
})
export class AdminService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getUsers() {
    return this.http.get<UserDto[]>(this.baseUrl + 'user/users');
  }

  getUserTasks(userId: number) {
    return this.http.get<TodoTask[]>(this.baseUrl + 'task/user/' + userId);
  }
}
