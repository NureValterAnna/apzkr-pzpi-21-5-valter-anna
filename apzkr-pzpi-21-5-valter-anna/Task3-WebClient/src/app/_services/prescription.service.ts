import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Prescription } from '../_models/prescription';
import { environment } from 'src/environments/environment.development';
import { NewPrescription } from '../_models/newPrescription';
import { MedicineIntakeInformation } from '../_models/medicineIntakeInformation';
import { Medicine } from '../_models/medicine';

@Injectable({
  providedIn: 'root'
})
export class PrescriptionService {

  constructor(private http: HttpClient) { }

  getPrescriptionsByUserId(userId: number) {
    return this.http.get<Prescription[]>(`${environment.apiUrl}/api/prescription/user/${userId}`);
  }

  getPrescriptionsById(id: number) {
    return this.http.get<Prescription>(`${environment.apiUrl}/api/prescription/${id}`);
  }

  getPrescriptionsAuthorized() {
    return this.http.get<Prescription[]>(`${environment.apiUrl}/api/prescription`);
  }

  getMedicinesAuthorized(){
    return this.http.get<Medicine[]>(`${environment.apiUrl}/api/medicine/authorized`);
  }

  getMedicineIntakeInformation(prescriptionId: number){
    return this.http.get<MedicineIntakeInformation>(`${environment.apiUrl}/api/prescription/${prescriptionId}/medicine-intake-information`);
  }

  getPercentageOfMedicineTaken(prescriptionId: number){
    return this.http.get<number>(`${environment.apiUrl}/api/prescription/${prescriptionId}/percentage-of-medicine-taken`);
  }

  delete(prescriptionId: number) {
    return this.http.delete(`${environment.apiUrl}/api/prescription/${prescriptionId}`);
  }

  create(prescription: NewPrescription) {
    return this.http.post<string>(`${environment.apiUrl}/api/prescription`, {
      medicineId: prescription.medicineId,
      userId: prescription.userId,
      dose: prescription.dose,
      timesPerDay: prescription.timesPerDay,
      prescriptionDateStart: prescription.prescriptionDateStart,
      prescriptionDateEnd: prescription.prescriptionDateEnd,
    });
  }
}
