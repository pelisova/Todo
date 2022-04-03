import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { Toast, ToastrService } from 'ngx-toastr';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(private toastr: ToastrService) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    const token: string | null = localStorage.getItem('token');

    if (token) {
      const expiry = JSON.parse(atob(token.split('.')[1])).exp;

      const IsExpired: boolean =
        Math.floor(new Date().getTime() / 1000) >= expiry;

      if (IsExpired) {
        localStorage.removeItem('token');
        this.toastr.info('Oops! Your session is expired! Try to refresh page.');
      } else {
        request = request.clone({
          setHeaders: {
            Authorization: `Bearer ${token}`,
          },
        });
      }
    }

    return next.handle(request);
  }
}
