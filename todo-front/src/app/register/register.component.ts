import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CreateUser } from '../_models/user';
import { UserRegister } from '../_models/userResponse';
import { AccountService } from '../_services/account.service';

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
    private toastr: ToastrService,
    private route: Router
  ) {}

  ngOnInit(): void {
    this.signupForm = new FormGroup({
      firstName: new FormControl(null, Validators.required),
      lastName: new FormControl(null, Validators.required),
      userName: new FormControl(null, Validators.required),
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
  }

  showPassword() {
    this.show = !this.show;
  }

  onSubmit() {
    const user = this.signupForm.value;
    const firstPassword = user.password;
    const retypePassword = user.retypePassword;

    let createUser: CreateUser = {
      firstName: user.firstName,
      lastName: user.lastName,
      userName: user.userName,
      email: user.email,
      password: user.password,
    };

    if (firstPassword !== retypePassword) return;
    this.accountService.register(createUser).subscribe(
      (res: UserRegister) => {
        if (res) {
          this.route.navigateByUrl('/login');
          this.signupForm.reset();
          this.toastr.success(res.message);
          setTimeout(() => {
            this.toastr.success('Please sign in');
          }, 500);
        }
      },
      (error) => {
        if (error) this.toastr.warning('Username or Email are incorrect!');
      }
    );
  }
}
