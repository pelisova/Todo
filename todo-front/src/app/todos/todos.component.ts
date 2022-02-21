import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { of } from 'rxjs';
import Swal from 'sweetalert2';
import { TodoTask } from '../models/model';
import { DataService } from '../services/data.service';
import { TodoDialogComponent } from '../todo-dialog/todo-dialog.component';

@Component({
  selector: 'app-todos',
  templateUrl: './todos.component.html',
  styleUrls: ['./todos.component.scss'],
})
export class TodosComponent implements OnInit {
  todos: TodoTask[] = [];

  constructor(
    private dataService: DataService,
    private toastr: ToastrService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.getTasks();
  }

  getTasks() {
    if (this.todos.length > 0) return of(this.todos);
    this.dataService.getAllTodos().subscribe((res) => {
      this.todos = res;
    });
  }

  toggleCompleted(todo: TodoTask) {
    todo.completed = !todo.completed;
    console.log(todo.completed);

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
      })
      .subscribe((res) => {
        console.log(res);
        this.todos = res;
      });
  }

  onSubmit(form: NgForm) {
    // console.log(form);
    // console.log(form.dirty);
    // console.log(form.valid);
    // console.log(form.controls.todo.errors);
    // console.log(form.value.todo);

    if (form.invalid) return;

    this.dataService
      .addTodo({ text: form.value.todo, userId: 1 })
      .subscribe((res) => {
        // console.log(res);
        this.todos = res;

        Swal.fire({
          position: 'top-end',
          icon: 'success',
          title: 'Task is successfully created!',
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
      console.log('Dialog result: ', result);
      if (result === undefined) return;

      this.dataService
        .updateTodo(id, {
          id: todo.id,
          text: result,
          completed: todo.completed,
          userId: 1,
        })
        .subscribe((res) => {
          this.toastr.success('Your task is updated!');
          this.todos = res;
        });
    });
  }

  onDelete(id: number) {
    this.dataService.deleteTodo(id).subscribe((res) => {
      // console.log(res);
      this.todos = res;

      Swal.fire({
        position: 'top-end',
        icon: 'success',
        title: 'Task is successfully deleted!',
        showConfirmButton: false,
        timer: 1000,
        width: '400px',
      });
    });
  }
}
