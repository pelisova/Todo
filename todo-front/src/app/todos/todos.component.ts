import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import Swal from 'sweetalert2';
import { CreateTask, PagParams, TodoTask } from '../models/model';
import { PaginatedResult, Pagination } from '../models/pagination';
import { DataService } from '../services/data.service';
import { TodoDialogComponent } from '../todo-dialog/todo-dialog.component';

@Component({
  selector: 'app-todos',
  templateUrl: './todos.component.html',
  styleUrls: ['./todos.component.scss'],
})
export class TodosComponent implements OnInit {
  todos: TodoTask[] = [];
  pagination!: Pagination;

  PagParams: PagParams = {
    pageNumber: 1,
    pageSize: 3,
  };

  error = null;

  constructor(
    private dataService: DataService,
    private toastr: ToastrService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.getData();
  }

  getData() {
    this.dataService.getAllTodos(this.PagParams).subscribe(
      (res: PaginatedResult<TodoTask[]>) => {
        if (res.result) this.todos = res.result.data;
        if (res.pagination) this.pagination = res.pagination;
      },
      (err) => {
        this.error = err.error;
      }
    );
  }

  pagedChanged(event: any) {
    this.PagParams.pageNumber = event.page;
    this.getData();
  }

  toggleCompleted(todo: TodoTask) {
    todo.completed = !todo.completed;

    let taskToUpdate: TodoTask = {
      id: todo.id,
      text: todo.text,
      completed: todo.completed,
      userId: todo.userId,
    };

    if (todo.completed) {
      this.toastr.success('Your task is completed!');
    } else {
      this.toastr.warning('Task is not completed!');
    }

    // subscribe is required to trigger HttpClient request (because of observable!)
    this.dataService.updateTodo(todo.id, taskToUpdate).subscribe();
  }

  onSubmit(form: NgForm) {
    if (form.invalid) return;

    let createTask: CreateTask = {
      text: form.value.todo,
      userId: 1,
    };

    this.dataService
      .addTodo(createTask, this.PagParams)
      .subscribe((res: PaginatedResult<TodoTask[]>) => {
        if (res.result) this.todos = res.result.data;
        if (res.pagination) this.pagination = res.pagination;
        this.error = null;

        Swal.fire({
          position: 'top-end',
          icon: 'success',
          title: res.result?.message,
          showConfirmButton: false,
          timer: 1000,
          width: '400px',
        });
      }); // mocking here!

    form.reset();
  }

  onEdit(id: number, todo: TodoTask) {
    let dialogRef = this.dialog.open(TodoDialogComponent, {
      data: todo,
      width: '600px',
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result === undefined) return;

      let taskToUpdate: TodoTask = {
        id: todo.id,
        text: result,
        completed: todo.completed,
        userId: todo.userId,
      };

      this.dataService.updateTodo(id, taskToUpdate, this.PagParams).subscribe(
        (res: PaginatedResult<TodoTask[]>) => {
          if (res.result) this.todos = res.result.data;
          if (res.pagination) this.pagination = res.pagination;
          this.toastr.success(res.result?.message);
        },
        (err) => console.log(err)
      );
    });
  }

  onDelete(id: number) {
    this.dataService
      .deleteTodo(id, this.PagParams)
      .subscribe((res: PaginatedResult<TodoTask[]>) => {
        if (res.result) this.todos = res.result.data;
        if (res.pagination) this.pagination = res.pagination;

        Swal.fire({
          position: 'top-end',
          icon: 'success',
          title: res.result?.message,
          showConfirmButton: false,
          timer: 1000,
          width: '400px',
        });
      });
  }
}
