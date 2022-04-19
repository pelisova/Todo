import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TodosComponent } from './todos/todos.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { TodoItemComponent } from './todo-item/todo-item.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { ToastrModule } from 'ngx-toastr';
import { MatDialogModule } from '@angular/material/dialog';
import { TodoDialogComponent } from './todo-dialog/todo-dialog.component';
import { ToolTipDirective } from './_shared/tool-tip.directive';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { RegisterComponent } from './register/register.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { LoginComponent } from './login/login.component';
import { WelcomeComponent } from './welcome/welcome.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { AdminPageComponent } from './admin/admin-page/admin-page.component';
import { MatMenuModule } from '@angular/material/menu';

@NgModule({
  declarations: [
    AppComponent,
    TodosComponent,
    TodoItemComponent,
    TodoDialogComponent,
    ToolTipDirective,
    RegisterComponent,
    NotFoundComponent,
    LoginComponent,
    WelcomeComponent,
    AdminPageComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    HttpClientModule,
    MatListModule,
    MatDialogModule,
    MatMenuModule,
    SweetAlert2Module.forRoot(),
    ToastrModule.forRoot({
      timeOut: 1500,
      positionClass: 'toast-top-left',
      preventDuplicates: false,
      easing: 'easi-in',
      // progressBar: true,
      // progressAnimation: 'increasing',
    }),
    PaginationModule.forRoot(),
    BsDropdownModule.forRoot(),
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
