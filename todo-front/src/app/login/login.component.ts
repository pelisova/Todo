import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  signupForm!: FormGroup;
  show: boolean = false;

  constructor(private toastr: ToastrService) {}

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
    console.log('You are successfully logged in!');
    this.signupForm.reset();
  }
}
