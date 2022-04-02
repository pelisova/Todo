import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LoginUser } from '../_models/user';
import { UserLogin } from '../_models/userResponse';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  signupForm!: FormGroup;
  show: boolean = false;

  constructor(
    private accountService: AccountService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.signupForm = new FormGroup({
      email: new FormControl(null, [Validators.email, Validators.required]),
      password: new FormControl(null, [
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

    let loginUser: LoginUser = {
      email: user.email,
      password: user.password,
    };

    this.accountService.login(loginUser).subscribe(
      (res: UserLogin) => {
        if (res) {
          this.signupForm.reset();
          this.router.navigateByUrl('/home');
          setTimeout(() => {
            this.toastr.success(res.message);
          }, 500);
        }
      },
      (error) => {
        if (error) this.toastr.warning('Username or Email are incorrect!');
      }
    );
  }
}
