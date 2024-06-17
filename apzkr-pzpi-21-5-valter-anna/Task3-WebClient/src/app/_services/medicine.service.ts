import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Dispenser } from '../_models/dispenser';
import { environment } from 'src/environments/environment.development';
import { Medicine } from '../_models/medicine';

@Injectable({
  providedIn: 'root'
})
export class MedicineService {

  constructor(private http: HttpClient) { }

  get() {
    return this.http.get<Medicine[]>(`${environment.apiUrl}/api/medicine`);
  }

  getById(medicineId: number | undefined) {
    return this.http.get<Medicine>(`${environment.apiUrl}/api/medicine/${medicineId}`);
  }

  update(medicine: Medicine) {
    return this.http.put<string>(`${environment.apiUrl}/api/medicine`, {
      id: medicine.id,
      title: medicine.title,
      description: medicine.description
    });
  }

  create(medicine: Medicine) {
    return this.http.post<string>(`${environment.apiUrl}/api/medicine`, {
      title: medicine.title,
      description: medicine.description
    });
  }

  delete(medicineId: number) {
    return this.http.delete(`${environment.apiUrl}/api/medicine/${medicineId}`);
  }
}
