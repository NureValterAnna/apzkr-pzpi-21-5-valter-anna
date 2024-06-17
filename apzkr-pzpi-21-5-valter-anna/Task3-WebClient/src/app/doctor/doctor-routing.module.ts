import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DoctorHomeComponent } from './doctor-home/doctor-home.component';
import { DispensersComponent } from '../administration/dispensers/dispensers.component';
import { PatientsListComponent } from './patients-list/patients-list.component';
import { PatientInfoComponent } from './patient-info/patient-info.component';
import { PrescriptionInfoComponent } from './prescription-info/prescription-info.component';

const routes: Routes = [
  {
    path: '', component: DoctorHomeComponent,
    children: [
      { path: '', redirectTo: 'patients', pathMatch: 'full' },
      { path: 'patients', component: PatientsListComponent },
      { path: 'patient-info/:id', component: PatientInfoComponent },
      { path: 'prescription-info/:id', component: PrescriptionInfoComponent}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DoctorRoutingModule { }
