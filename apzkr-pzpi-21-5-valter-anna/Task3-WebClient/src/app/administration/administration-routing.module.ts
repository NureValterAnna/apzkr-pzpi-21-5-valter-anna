import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DispensersComponent } from './dispensers/dispensers.component';
import { AdminHomeComponent } from './admin-home/admin-home.component';
import { MedicinesComponent } from './medicines/medicines.component';
import { UsersComponent } from './users/users.component';
import { StocksComponent } from './stocks/stocks.component';
import { adminGuard } from '../_guards/admin.guard';

const routes: Routes = [
  {
    path: '', component: AdminHomeComponent,
    children: [
      { path: '', redirectTo: 'dispensers', pathMatch: 'full' },
      { path: 'dispensers', component: DispensersComponent },
      { path: 'medicines', component: MedicinesComponent },
      { path: 'users', component: UsersComponent },
      { path: 'stocks', component: StocksComponent },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdministrationRoutingModule { }
