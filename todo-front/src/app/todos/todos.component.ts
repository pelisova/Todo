import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import Swal from 'sweetalert2';
import { CreateTask, PagParams, TodoTask } from '../_models/model';
import { PaginatedResult, Pagination } from '../_models/pagination';
import { AccountService } from '../_services/account.service';
import { DataService } from '../_services/data.service';
import { TodoDialogComponent } from '../todo-dialog/todo-dialog.component';
import { User } from '../_models/user';

@Component({
  selector: 'app-todos',
  templateUrl: './todos.component.html',
  styleUrls: ['./todos.component.scss'],
})
export class TodosComponent implements OnInit {
  todos: TodoTask[] = [];
  pagination!: Pagination;
  currentUser: User | null | undefined;
  pageNum: number = 1;

  PagParams: PagParams = {
    pageNumber: this.pageNum,
    pageSize: 5,
  };

  error = null;

  constructor(
    private dataService: DataService,
    private toastr: ToastrService,
    private dialog: MatDialog,
    public accountService: AccountService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.accountService.currentUser$.subscribe(
      (user) => (this.currentUser = user)
    );
    this.getData();
  }

  getData() {
    this.dataService.getAllTodos(this.PagParams).subscribe(
      (res: PaginatedResult<TodoTask[]>) => {
        if (res.result) this.todos = res.result.data.items;
        if (res.pagination) this.pagination = res.pagination;
      },
      (err) => {
        this.error = err.error;
      }
    );
  }

  setPageNumber(pageNumber: number) {
    this.PagParams.pageNumber = pageNumber;
    this.getData();
  }

  pagedChanged(event: any) {
    this.pageNum = event.page;
    this.PagParams.pageNumber = this.pageNum;
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
      this.toastr.success('Your task is completed.');
    } else {
      this.toastr.warning('Task is not completed.');
    }

    // subscribe is required to trigger HttpClient request (because of observable!)
    this.dataService.updateTodo(todo.id, taskToUpdate).subscribe();
  }

  onSubmit(form: NgForm) {
    if (form.invalid) return;

    let createTask: CreateTask = {
      text: form.value.todo,
      userId: this.currentUser?.id,
    };

    this.dataService
      .addTodo(createTask, this.PagParams)
      .subscribe((res: PaginatedResult<TodoTask[]>) => {
        if (res.result) this.todos = res.result.data.items;
        if (res.pagination) this.pagination = res.pagination;
        this.error = null;

        Swal.fire({
          position: 'top-end',
          icon: 'success',
          title: res.result?.message,
          showConfirmButton: false,
          timer: 1000,
          width: '400px',
          heightAuto: false,
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
          if (res.result) this.todos = res.result.data.items;
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
        if (res.result) {
          this.todos = res.result.data.items;
          if (this.todos.length == 0) {
            this.setPageNumber((this.pageNum += -1));
          }
        }

        if (res.pagination) this.pagination = res.pagination;

        Swal.fire({
          position: 'top-end',
          icon: 'success',
          title: res.result?.message,
          showConfirmButton: false,
          timer: 1000,
          width: '400px',
          heightAuto: false,
        });
      });
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/login');
  }
}
