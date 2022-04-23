import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TodoTask } from 'src/app/_models/model';
import { UserDto } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { AdminService } from 'src/app/_services/admin.service';

@Component({
  selector: 'app-admin-page',
  templateUrl: './admin-page.component.html',
  styleUrls: ['./admin-page.component.scss'],
})
export class AdminPageComponent implements OnInit {
  users: UserDto[] | undefined;
  tasks: TodoTask[] | undefined;
  errorMsg: string | undefined;
  selectedIndex: number | undefined;

  constructor(
    private accountService: AccountService,
    private adminService: AdminService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers() {
    this.adminService.getUsers().subscribe((res) => (this.users = res));
  }

  setIndex(index: number) {
    this.selectedIndex = index;
  }

  showTasks(userId: number) {
    this.adminService.getUserTasks(userId).subscribe(
      (res) => {
        this.tasks = res;
        this.errorMsg = undefined;
      },
      (err) => {
        this.errorMsg = err.error;
        this.tasks = undefined;
        console.log(this.errorMsg);
      }
    );
  }

  deleteUser(userId: number) {
    this.adminService.deleteUser(userId).subscribe((res) => {
      this.users = res;
      this.tasks = undefined;
    });
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/login');
  }
}
