import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  signupForm!: FormGroup;
  show: boolean = false;

  constructor(
    private accountService: AccountService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.signupForm = new FormGroup({
      username: new FormControl(null, Validators.required),
      email: new FormControl(null, [Validators.email, Validators.required]),
      password: new FormControl(null, [
        Validators.minLength(4),
        Validators.required,
      ]),
      retypePassword: new FormControl(null, [
        Validators.minLength(4),
        Validators.required,
      ]),
    });
    this.getUsers();
  }

  showPassword() {
    this.show = !this.show;
  }

  onSubmit() {
    const user = this.signupForm.value;
    const password = user.password;
    const retypePassword = user.retypePassword;

    if (password !== retypePassword) return;
    this.accountService.createUser(user);
    this.getUsers();
    this.toastr.success('You are successfully registered!');
    this.signupForm.reset();
  }

  getUsers() {
    console.log(this.accountService.getAll());
  }
}
