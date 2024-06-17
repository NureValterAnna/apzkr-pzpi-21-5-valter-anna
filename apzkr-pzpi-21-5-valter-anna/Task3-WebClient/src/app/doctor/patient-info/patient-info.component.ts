import { Component, Input, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Prescription } from 'src/app/_models/prescription';
import { User } from 'src/app/_models/user';
import { PrescriptionService } from 'src/app/_services/prescription.service';
import { UserService } from 'src/app/_services/user.service';
import { PrescriptionDialogComponent } from './prescription-dialog/prescription-dialog.component';
import * as moment from 'moment';


@Component({
  selector: 'app-patient-info',
  templateUrl: './patient-info.component.html',
  styleUrls: ['./patient-info.component.css']
})
export class PatientInfoComponent implements OnInit {
  patient?: User;
  prescriptions: Prescription[] = [];
  patientId!: number;

  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private presctiptionService: PrescriptionService,
    private dialogRef: MatDialog,
    private router: Router
  ) {}

  ngOnInit() {
    this.patientId = +this.route.snapshot.paramMap.get('id')!;
    this.userService.getById(this.patientId).subscribe((patient: User) => {
      this.patient = patient;
    });

    this.presctiptionService.getPrescriptionsByUserId(this.patientId).subscribe(
      (prescriptions: Prescription[]) => {
        this.prescriptions = prescriptions;
        for (let prescription of this.prescriptions) {
          let utcTime = prescription.prescriptionDateStart; 
          let localTime = moment.utc(utcTime).local().format('YYYY-MM-DD HH:mm:ss');
          prescription.prescriptionDateStart = new Date(localTime);
          utcTime = prescription.prescriptionDateEnd; 
          localTime = moment.utc(utcTime).local().format('YYYY-MM-DD HH:mm:ss');
          prescription.prescriptionDateEnd = new Date(localTime);
        }
      }
    );
  }

  selectPrescription(prescriptionId: number){
    this.router.navigate(['doctor/prescription-info', prescriptionId]);
  }

  openDialog(){
    this.dialogRef.open(PrescriptionDialogComponent,{
      data: { patientId: this.patientId }
    });

    this.dialogRef.afterAllClosed.subscribe(() => {
      this.presctiptionService.getPrescriptionsByUserId(this.patientId).subscribe(
        (prescriptions: Prescription[]) => {
          this.prescriptions = prescriptions;
        }
      );
    });
  }

  deletePrescription(prescriptionId: number){
    this.presctiptionService.delete(prescriptionId).subscribe(() => {
      this.prescriptions = this.prescriptions.filter(prescription => prescription.id !== prescriptionId);
    });
  }

}
