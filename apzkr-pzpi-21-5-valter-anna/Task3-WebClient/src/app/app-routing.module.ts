import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { authGuard } from './_guards/auth.guard';
import { adminGuard } from './_guards/admin.guard';
import { doctorGuard } from './_guards/doctor.guard';
import { NotFoundComponent } from './shared/errors/not-found/not-found.component';

const routes: Routes = [
  { 
    path: 'account', 
    loadChildren: () => import('./account/account.module').then(m => m.AccountModule)
  },
  { 
    path: 'admin', 
    loadChildren: () => import('./administration/administration.module').then(m => m.AdministrationModule),
    canActivate: [authGuard, adminGuard]
  },
  { 
    path: 'doctor', 
    loadChildren: () => import('./doctor/doctor.module').then(m => m.DoctorModule),
    canActivate: [authGuard/*, doctorGuard*/],
  },
  { 
    canActivate: [authGuard],
    path: 'patient', 
    loadChildren: () => import('./patient/patient.module').then(m => m.PatientModule)
  },
  {path: 'not-found', component: NotFoundComponent},
  {path: '**', component: NotFoundComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
