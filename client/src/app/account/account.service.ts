import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { IUser } from '../shared/models/user';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new BehaviorSubject<IUser>(null);
  // an observable to use wherever we need to interact with user data
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router) {}

  //#region login
  /**
   * <summary>
   * 1. we send login credentials to server and get back the user(displayname, email, token generated by server)
   * 2. using map, we project the object response from the server to our user observable and save the token locally
   * 3. we assign the observable the user object from the server
   * </summary>
   */
  login(values: any) {
    return this.http.post(this.baseUrl + 'account/login', values).pipe(
      map((user: IUser) => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
        }
      })
    );
  }
  //#endregion

  //#region register
  /**
   * <summary>
   * 1. we send register credentials to server and get back as response in headers the newly created user(displayname, email, token generated by server)
   * 2. using map operator, we project the object response from the server to our user observable and save the token locally
   * </summary>
   */
  register(values: any) {
    return this.http.post(this.baseUrl + 'account/register', values).pipe(
      map((user: IUser) => {
        localStorage.setItem('token', user.token);
      })
    );
  }
  //#endregion

  //#region logout
  /**
   * <summary>
   * At logout we remove the stored token, then nullify the user observable and navigate to home
   * </summary>
   */
  logout() {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }
  //#endregion

  //#region emailExists
  /**
   * <summary>
   * returns a boolean on email existance
   * </summary>
   */
  checkEmailExists = (email: string) =>
    this.http.get(this.baseUrl + 'account/emailExists?email=' + email);
  //#endregion

  loadCurrentUser(token: string) {
    // if (token === null) {
    //   this.currentUserSource.next(null);
    //   return of(null);
    // }
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.get(this.baseUrl + 'account', { headers }).pipe(
      map((user: IUser) => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
        }
      })
    );
  }

  getCurrentUserValue = () => this.currentUserSource.value;
}
