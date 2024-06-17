import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { User } from '../_models/user';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private userSubject: BehaviorSubject<User | null>;
  public user: Observable<User | null>;

  constructor(private router: Router, private http: HttpClient) { 
    // Initialize the userSubject with the user from localStorage (if available)
    this.userSubject = new BehaviorSubject(JSON.parse(localStorage.getItem('user')!));
    this.user = this.userSubject.asObservable();
  }

  // Getter to access the current user value
  public get userValue() {
    return this.userSubject.value;
  }

  /**
   * Logs in the user by making a POST request to the API.
   * @param email - The user's email address.
   * @param password - The user's password.
   * @returns An Observable containing the user data.
   */
  login(email: string, password: string) {
    return this.http.post<any>(`${environment.apiUrl}/api/account/login`, { email, password })
      .pipe(
        map(user => {
          // Store user details in localStorage and update the userSubject
          localStorage.setItem('user', JSON.stringify(user));
          this.userSubject.next(user);
          return user;
        })
      );
  }

  /**
   * Registers a new user by making a POST request to the API.
   * @param name - The user's first name.
   * @param surname - The user's last name.
   * @param age - The user's age.
   * @param email - The user's email address.
   * @param password - The user's password.
   * @returns An Observable containing the user data.
   */
  register(name: string, surname: string, age: number, email: string, password: string) {
    return this.http.post<any>(`${environment.apiUrl}/api/account/register`, { name, surname, age, email, password })
      .pipe(
        map(user => {
          // Store user details in localStorage and update the userSubject
          localStorage.setItem('user', JSON.stringify(user));
          this.userSubject.next(user);
          return user;
        })
      );
  }

  /**
   * Logs out the user by removing the user data from localStorage and resetting the userSubject.
   * Also navigates the user to the login page.
   */
  logout() {
    localStorage.removeItem('user');
    this.userSubject.next(null);
    this.router.navigate(['/account/login']);
  }

  /**
   * Gets the role of the currently logged-in user.
   * @returns The user's role if available, otherwise undefined.
   */
  getRole() {
    const userString = localStorage.getItem('user');
    if (userString) {
      const user = JSON.parse(userString);
      return user.Role;
    }
  }

  /**
   * Gets the name of the currently logged-in user.
   * @returns The user's name if available, otherwise undefined.
   */
  getName() {
    const userString = localStorage.getItem('user');
    if (userString) {
      const user = JSON.parse(userString);
      return user.Name;
    }
  }
}
