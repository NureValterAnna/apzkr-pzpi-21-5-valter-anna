import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PatientHomeComponent } from './patient-home/patient-home.component';
import { PrescriptionsComponent } from './prescriptions/prescriptions.component';
import { MedicinesComponent } from './medicines/medicines.component';

const routes: Routes = [
  {
    path: '', component: PatientHomeComponent,
    children: [
      { path: '', redirectTo: 'prescriptions', pathMatch: 'full' },
      { path: 'prescriptions', component: PrescriptionsComponent },
      { path: 'medicines', component: MedicinesComponent },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PatientRoutingModule { }
