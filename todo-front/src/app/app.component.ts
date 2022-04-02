import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'todo-app';

  constructor(private accountService: AccountService) {}

  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser() {
    const token: string | null = localStorage.getItem('token');
    if (token) {
      this.accountService.getCurrentUser().subscribe((user: User) => {
        this.accountService.setCurrentUser(user);
      });
    } else {
      this.accountService.currentUser$.next(null);
    }
  }
}
