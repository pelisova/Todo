import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { CreateTask, PagParams, TodoTask } from '../_models/model';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../_models/pagination';

@Injectable({
  providedIn: 'root',
})
export class DataService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getAllTodos(param: PagParams) {
    let params = this.getParams(param.pageNumber, param.pageSize);
    const res = this.http.get<TodoTask[]>(this.baseUrl + 'task/paginate', {
      observe: 'response',
      params,
    });
    return this.getPaginatedResult<TodoTask[]>(res);
  }

  addTodo(todo: CreateTask, param: PagParams) {
    let params = this.getParams(param.pageNumber, param.pageSize);
    const res = this.http.post<TodoTask[]>(this.baseUrl + 'task', todo, {
      observe: 'response',
      params,
    });
    return this.getPaginatedResult<TodoTask[]>(res);
  }

  updateTodo(id: number, todo: TodoTask, param?: PagParams) {
    let params = this.getParams(param?.pageNumber, param?.pageSize);
    const res = this.http.patch<TodoTask[]>(this.baseUrl + 'task/' + id, todo, {
      observe: 'response',
      params,
    });
    return this.getPaginatedResult<TodoTask[]>(res);
  }

  deleteTodo(id: number, param: PagParams) {
    let params = this.getParams(param.pageNumber, param.pageSize);
    const res = this.http.delete<TodoTask[]>(this.baseUrl + 'task/' + id, {
      observe: 'response',
      params,
      headers: { 'Content-Type': 'application/json' },
    });
    return this.getPaginatedResult<TodoTask[]>(res);
  }

  private getPaginatedResult<T>(result: Observable<any>) {
    return result.pipe(
      map((response) => {
        const paginatedResult: PaginatedResult<T> = new PaginatedResult<T>();
        if (response.body) paginatedResult.result = response.body;

        const paginationHeader = response.headers.get('Pagination');
        if (paginationHeader) {
          paginatedResult.pagination = JSON.parse(paginationHeader);
        }

        return paginatedResult;
      })
    );
  }

  private getParams(page?: number, itemsPerPage?: number) {
    let params;
    if (page && itemsPerPage)
      params = this.getPaginationHeaders(page, itemsPerPage);
    return params;
  }

  private getPaginationHeaders(pageNumber: number, pageSize: number) {
    let params = new HttpParams();

    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    return params;
  }
}
