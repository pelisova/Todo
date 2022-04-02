import {
  AfterViewInit,
  Component,
  ElementRef,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import { TodoTask } from '../_models/model';
// import tippy from 'tippy.js';

@Component({
  selector: 'app-todo-item',
  templateUrl: './todo-item.component.html',
  styleUrls: ['./todo-item.component.scss'],
})
export class TodoItemComponent implements OnInit {
  @Input() todo: TodoTask | undefined;
  @Output() todoToggle: EventEmitter<void> = new EventEmitter();
  @Output() deleteEvent: EventEmitter<void> = new EventEmitter();
  @Output() editEvent: EventEmitter<void> = new EventEmitter();

  constructor() {}

  toggleCompletedMethod() {
    this.todoToggle.emit();
  }

  deleteEventMethod() {
    this.deleteEvent.emit();
  }

  onEdit() {
    this.editEvent.emit();
  }

  ngOnInit(): void {}
}
