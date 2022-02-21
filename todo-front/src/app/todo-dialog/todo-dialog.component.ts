import { Component, Inject, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { TodoTask } from '../models/model';

@Component({
  selector: 'app-todo-dialog',
  templateUrl: './todo-dialog.component.html',
  styleUrls: ['./todo-dialog.component.scss'],
})
export class TodoDialogComponent implements OnInit {
  constructor(
    private dialog: MatDialogRef<TodoDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public todo: TodoTask
  ) {}

  ngOnInit(): void {}

  onSubmitDialog(form: NgForm) {
    // console.log(form.value);
    this.dialog.close(form.value.todoEdit);
  }

  cancelDialog() {
    this.dialog.close();
  }
}
