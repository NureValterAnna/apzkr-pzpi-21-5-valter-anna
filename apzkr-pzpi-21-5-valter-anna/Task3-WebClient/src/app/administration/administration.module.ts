import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdministrationRoutingModule } from './administration-routing.module';
import { AdminHomeComponent } from './admin-home/admin-home.component';
import { DispensersComponent } from './dispensers/dispensers.component';
import { DispenserDialogComponent } from './dispensers/dispenser-dialog/dispenser-dialog.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MedicinesComponent } from './medicines/medicines.component';
import { MedicineDialogComponent } from './medicines/medicine-dialog/medicine-dialog.component';
import { UsersComponent } from './users/users.component';
import { UserDialogComponent } from './users/user-dialog/user-dialog.component';
import { StocksComponent } from './stocks/stocks.component';
import { StocksDialogComponent } from './stocks/stocks-dialog/stocks-dialog.component';
import { AdminSidebarComponent } from './admin-sidebar/admin-sidebar.component';
import { TranslateModule } from '@ngx-translate/core';


@NgModule({
  declarations: [
    AdminHomeComponent,
    DispensersComponent,
    DispenserDialogComponent,
    MedicinesComponent,
    MedicineDialogComponent,
    UsersComponent,
    UserDialogComponent,
    StocksComponent,
    StocksDialogComponent,
    AdminSidebarComponent,
  ],
  imports: [
    CommonModule,
    AdministrationRoutingModule,
    ReactiveFormsModule,
    TranslateModule
  ]
})
export class AdministrationModule { }
