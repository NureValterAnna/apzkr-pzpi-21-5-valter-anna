import { Component } from '@angular/core';
import { Medicine } from 'src/app/_models/medicine';
import { PrescriptionService } from 'src/app/_services/prescription.service';

@Component({
  selector: 'app-medicines',
  templateUrl: './medicines.component.html',
  styleUrls: ['./medicines.component.css']
})
export class MedicinesComponent {
  medicines: Medicine[] = [];

  constructor(
    private prescriptionSerice: PrescriptionService,
  ){}

  ngOnInit() {
    this.loadMedicines();
  }

  loadMedicines(){
    this.prescriptionSerice.getMedicinesAuthorized().subscribe(
      (medicines: Medicine[]) => {
        this.medicines = medicines;
      }
    );
  }

}
