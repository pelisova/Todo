import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { CreateTask, TodoTask } from '../models/model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class DataService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getAllTodos(): Observable<TodoTask[]> {
    return this.http.get<TodoTask[]>(this.baseUrl + 'task');
  }

  addTodo(todo: CreateTask) {
    return this.http.post<TodoTask[]>(this.baseUrl + 'task', todo);
  }

  updateTodo(id: number, todo: TodoTask) {
    return this.http.patch<TodoTask[]>(this.baseUrl + 'task/' + id, todo);
  }

  deleteTodo(id: number) {
    return this.http.delete<TodoTask[]>(this.baseUrl + 'task/' + id, {});
  }
}
