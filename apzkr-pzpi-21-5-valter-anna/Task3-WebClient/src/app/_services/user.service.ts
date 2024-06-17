import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../_models/user';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  get() {
    return this.http.get<User[]>(`${environment.apiUrl}/api/user`);
  }

  getPatients(){
    return this.http.get<User[]>(`${environment.apiUrl}/api/user/patients`);
  }

  getById(userId: number) {
    return this.http.get<User>(`${environment.apiUrl}/api/user/${userId}`);
  }

  update(user: User) {
    return this.http.put<string>(`${environment.apiUrl}/api/user/change-role`, {
      id: user.id,
      role: user.role
    });
  }

  delete(userId: number) {
    return this.http.delete(`${environment.apiUrl}/api/user/${userId}`);
  }

  export() {
    return this.http.get<string>(`${environment.apiUrl}/api/user/export`);
  }

  import(json: string) {
    return this.http.post<User[]>(`${environment.apiUrl}/api/user/import`, { json });
  }
}
