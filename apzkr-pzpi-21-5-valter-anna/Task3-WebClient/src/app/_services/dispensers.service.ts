import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Dispenser } from '../_models/dispenser';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class DispensersService {

  constructor(private http: HttpClient) { }


  get(temperatureUnit: string) {
    return this.http.get<Dispenser[]>(`${environment.apiUrl}/api/dispenser?temperatureUnit=${temperatureUnit}`);
  }

  getById(dispenserId: number) {
    return this.http.get<Dispenser>(`${environment.apiUrl}/api/dispenser/${dispenserId}?temperatureUnit=C`);
  }

  update(dispenser: Dispenser) {
    return this.http.put<string>(`${environment.apiUrl}/api/dispenser`, {
      id: dispenser.id,
      location: dispenser.location
    });
  }

  create(dispenser: Dispenser) {
    return this.http.post<string>(`${environment.apiUrl}/api/dispenser`, {
      dispenserName: dispenser.dispenserName,
      location: dispenser.location
    });
  }

  delete(dispenserId: number) {
    return this.http.delete(`${environment.apiUrl}/api/dispenser/${dispenserId}`);
  }
}
