import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { CreateTask, TodoTask } from '../models/model';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../models/pagination';

@Injectable({
  providedIn: 'root',
})
export class DataService {
  baseUrl = environment.apiUrl;
  paginatedResult: PaginatedResult<TodoTask[]> = new PaginatedResult<TodoTask[]>();

  constructor(private http: HttpClient) {}

  getAllTodos(page?: number, itemsPerPage?:number) {

    let params = new HttpParams();

    if(page && itemsPerPage) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    return this.http.get<TodoTask[]>(this.baseUrl + 'task/paginate', {observe: 'response', params}).pipe(
      map(response => {
        if(response.body) this.paginatedResult.result = response.body;

        const paginationHeader = response.headers.get('Pagination'); 
        if(paginationHeader){
         this.paginatedResult.pagination = JSON.parse(paginationHeader); 
        }
        
        return this.paginatedResult;
      })
    );
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
