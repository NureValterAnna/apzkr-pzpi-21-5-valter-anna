import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DoctorRoutingModule } from './doctor-routing.module';
import { DoctorHomeComponent } from './doctor-home/doctor-home.component';
import { DoctorSidebarComponent } from './doctor-sidebar/doctor-sidebar.component';
import { PatientsListComponent } from './patients-list/patients-list.component';
import { PatientInfoComponent } from './patient-info/patient-info.component';
import { PrescriptionDialogComponent } from './patient-info/prescription-dialog/prescription-dialog.component';
import { ReactiveFormsModule } from '@angular/forms';
import { PrescriptionInfoComponent } from './prescription-info/prescription-info.component';
import { TranslateModule } from '@ngx-translate/core';


@NgModule({
  declarations: [
    DoctorHomeComponent,
    DoctorSidebarComponent,
    PatientsListComponent,
    PatientInfoComponent,
    PrescriptionDialogComponent,
    PrescriptionInfoComponent
  ],
  imports: [
    CommonModule,
    DoctorRoutingModule,
    ReactiveFormsModule,
    TranslateModule
  ]
})
export class DoctorModule { }
