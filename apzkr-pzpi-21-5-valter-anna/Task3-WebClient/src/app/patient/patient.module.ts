import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PatientRoutingModule } from './patient-routing.module';
import { PrescriptionsComponent } from './prescriptions/prescriptions.component';
import { MedicinesComponent } from './medicines/medicines.component';
import { PatientSidebarComponent } from './patient-sidebar/patient-sidebar.component';
import { PatientHomeComponent } from './patient-home/patient-home.component';
import { MedicineInfoComponent } from './medicines/medicine-info/medicine-info.component';
import { TranslateModule } from '@ngx-translate/core';
import { MomentModule } from 'ngx-moment';



@NgModule({
  declarations: [
    PrescriptionsComponent,
    MedicinesComponent,
    PatientSidebarComponent,
    PatientHomeComponent,
    MedicineInfoComponent
  ],
  imports: [
    CommonModule,
    PatientRoutingModule,
    TranslateModule,
    MomentModule
  ]
})
export class PatientModule { }
