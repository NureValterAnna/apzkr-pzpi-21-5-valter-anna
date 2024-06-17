import { Component, OnInit } from '@angular/core';
import { Prescription } from 'src/app/_models/prescription';
import { PrescriptionService } from 'src/app/_services/prescription.service';
import * as moment from 'moment';


@Component({
  selector: 'app-prescriptions',
  templateUrl: './prescriptions.component.html',
  styleUrls: ['./prescriptions.component.css']
})
export class PrescriptionsComponent implements OnInit {
  prescriptions: Prescription[] = [];
  percentagesOfMedicineTaken:  number[] = [];

  constructor(
    private prescriptionSerice: PrescriptionService,
  ){}

  ngOnInit() {
    this.loadPrescriptions();
  }

  loadPrescriptions(){
    this.prescriptionSerice.getPrescriptionsAuthorized().subscribe(
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
        this.loadPercentagesOfMedicineTaken();
      }
    );
  }

  loadPercentagesOfMedicineTaken() {
    this.prescriptions.forEach((prescription, index) => {
      this.prescriptionSerice.getPercentageOfMedicineTaken(prescription.id).subscribe(
        (result: number) => {
          this.percentagesOfMedicineTaken[index] = result;
        }
      );
    });
  }

}
