import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Medicine } from 'src/app/_models/medicine';
import { MedicineIntakeInformation } from 'src/app/_models/medicineIntakeInformation';
import { Prescription } from 'src/app/_models/prescription';
import { MedicineService } from 'src/app/_services/medicine.service';
import { PrescriptionService } from 'src/app/_services/prescription.service';

@Component({
  selector: 'app-prescription-info',
  templateUrl: './prescription-info.component.html',
  styleUrls: ['./prescription-info.component.css']
})
export class PrescriptionInfoComponent {
  isInfo = false;
  prescription?: Prescription;
  prescriptionId!: number;
  medicineIntakeInformation?: MedicineIntakeInformation;

  constructor(
    private presctiptionService: PrescriptionService,
    private medicineService: MedicineService,
    private route: ActivatedRoute,
  ) {}

  ngOnInit() {
    this.prescriptionId = +this.route.snapshot.paramMap.get('id')!;

    this.presctiptionService.getPrescriptionsById(this.prescriptionId).subscribe(
      (res: Prescription)=> {
        this.prescription = res;
      }
    );

    this.presctiptionService.getMedicineIntakeInformation(this.prescriptionId).subscribe(
      (res: MedicineIntakeInformation) => {
        if(res.expectedTotalDoses == 0) {
          this.isInfo = false;
        } 
        else{
          this.isInfo = true;
          this.medicineIntakeInformation = res;
        }
      }
    );
  }
}
