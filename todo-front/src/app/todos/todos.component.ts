import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import Swal from 'sweetalert2';
import { TodoTask } from '../models/model';
import { Pagination } from '../models/pagination';
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
  pageNumber = 1;
  pageSize = 3;
  error = null;

  constructor(
    private dataService: DataService,
    private toastr: ToastrService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.getTasks();
  }

   getTasks() {
    this.dataService.getAllTodos(this.pageNumber, this.pageSize).subscribe((res) => {
      if(res.result) this.todos = res.result;
      if(res.pagination) this.pagination = res.pagination
    }, err => {
      this.error = err.error;
    });
  }

  pagedChanged(event: any) {
    this.pageNumber = event.page;
    this.getTasks();
  }

  toggleCompleted(todo: TodoTask) {
    todo.completed = !todo.completed;

    if (todo.completed) {
      this.toastr.success('Your task is completed!');
    } else {
      this.toastr.warning('Task is not completed!');
    }

    this.dataService
      .updateTodo(todo.id, {
        id: todo.id,
        text: todo.text,
        completed: todo.completed,
        userId: todo.userId,
      }).subscribe(); // subscribe is required to trigger HttpClient request (because of observable!)
  }

  onSubmit(form: NgForm) {
    if (form.invalid) return;

    this.dataService
      .addTodo({ text: form.value.todo, userId: 1 }, this.pageNumber, this.pageSize)
      .subscribe((res: any) => {
        // console.log(res); // interface dor this!
        if(res.result) this.todos = res.result.tasks;
        if(res.pagination) this.pagination = res.pagination;
        this.error = null;

        Swal.fire({
          position: 'top-end',
          icon: 'success',
          title: res.result.message,
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

      this.dataService
        .updateTodo(id, {
          id: todo.id,
          text: result,
          completed: todo.completed,
          userId: 1,
        }, this.pageNumber, this.pageSize)
        .subscribe((res:any) => {
          // console.log(res);
          if(res.result) this.todos = res.result.newTasks;
          if(res.pagination) this.pagination = res.pagination;
          this.toastr.success(res.result.message);
        });
    });
  }

  onDelete(id: number) {
    this.dataService.deleteTodo(id, this.pageNumber, this.pageSize).subscribe((res: any) => {
      //interface for res:any
      if(res.result) this.todos = res.result.tasks;
      if(res.pagination) this.pagination = res.pagination;

      Swal.fire({
        position: 'top-end',
        icon: 'success',
        title: res.result.message,
        showConfirmButton: false,
        timer: 1000,
        width: '400px',
      });
    });
  }
}
