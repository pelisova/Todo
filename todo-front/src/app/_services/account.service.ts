import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { CreateUser, LoginUser, User } from '../_models/user';
import { UserLogin, UserRegister } from '../_models/userResponse';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = environment.apiUrl;
  public currentUser$ = new ReplaySubject<User | null>(1);

  constructor(private http: HttpClient) {}

  register(user: CreateUser) {
    return this.http.post<UserRegister>(
      this.baseUrl + 'account/register',
      user
    );
  }

  login(user: LoginUser) {
    return this.http.post<UserLogin>(this.baseUrl + 'account/login', user).pipe(
      map((res: UserLogin) => {
        if (res) {
          let user = res.data;
          this.setCurrentUser(user);
        }
        return res;
      })
    );
  }

  logout() {
    localStorage.removeItem('token');
    this.currentUser$.next(null);
  }

  setCurrentUser(user: User) {
    if (user.token) {
      localStorage.setItem('token', user.token);
    }

    this.currentUser$.next(user);
  }

  getCurrentUser() {
    return this.http.get<User>(this.baseUrl + 'user/email');
  }
}
