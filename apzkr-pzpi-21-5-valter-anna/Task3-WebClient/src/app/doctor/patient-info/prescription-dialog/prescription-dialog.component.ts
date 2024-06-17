import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Medicine } from 'src/app/_models/medicine';
import { NewPrescription } from 'src/app/_models/newPrescription';
import { Prescription } from 'src/app/_models/prescription';
import { MedicineService } from 'src/app/_services/medicine.service';
import { PrescriptionService } from 'src/app/_services/prescription.service';

@Component({
  selector: 'app-prescription-dialog',
  templateUrl: './prescription-dialog.component.html',
  styleUrls: ['./prescription-dialog.component.css']
})
export class PrescriptionDialogComponent implements OnInit {
  createForm: FormGroup;
  medicines: Medicine[] = []; 

  constructor(
    private fb: FormBuilder,
    private prescriptionService: PrescriptionService,
    private medicineService: MedicineService,
    private dialogRef: MatDialogRef<PrescriptionDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { patientId: number }  
  ) {
    this.createForm = this.fb.group({
      medicine: [null, Validators.required],
      dose: [null, Validators.required],
      timesPerDay: [null, Validators.required],
      prescriptionDateStart: [null, Validators.required],
      prescriptionDateEnd: [null, Validators.required],
    });
  }

  ngOnInit(): void {
    this.medicineService.get().subscribe(medicines => {
      this.medicines = medicines;
    });
  }

  get dose(){
    return this.createForm.get('dose');
  }

  get timesPerDay() {
    return this.createForm.get('timesPerDay');
  }

  get prescriptionDateStart() {
    return this.createForm.get('prescriptionDateStart');
  }

  get prescriptionDateEnd() {
    return this.createForm.get('prescriptionDateEnd');
  }

  onSubmit() {
    if (this.createForm.valid) {
      const prescription: NewPrescription = {
        userId: this.data.patientId,
        medicineId: this.createForm.value.medicine.id,
        dose: this.createForm.value.dose,
        timesPerDay: this.createForm.value.timesPerDay,
        prescriptionDateStart: this.createForm.value.prescriptionDateStart,
        prescriptionDateEnd: this.createForm.value.prescriptionDateEnd,
      };

      console.log(prescription);

      this.prescriptionService.create(prescription).subscribe(() => {
        this.dialogRef.close();
      });
    }
  }
}
