import { Component } from '@angular/core';
import { MedicineService } from 'src/app/_services/medicine.service';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { MedicineDialogComponent } from './medicine-dialog/medicine-dialog.component';
import { Medicine } from 'src/app/_models/medicine';

@Component({
  selector: 'app-medicines',
  templateUrl: './medicines.component.html',
  styleUrls: ['./medicines.component.css']
})
export class MedicinesComponent {

  medicines: Medicine[] = [];

  constructor(private medicinesService: MedicineService, private dialogRef: MatDialog){}

  ngOnInit() {
    this.medicinesService.get().subscribe(
      (res: Medicine[]) => {
        this.medicines = res.sort((a, b) => a.id - b.id);
      }
    )
  }

  openDialog(medicineId: number, actionName: string) {
    this.dialogRef.open(MedicineDialogComponent, {
      data: { medicineId, actionName }
    });

    this.dialogRef.afterAllClosed.subscribe(() => {
      this.medicinesService.get().subscribe(
        (res: Medicine[]) => {
          this.medicines = res;
        }
      );
    });
  }

  deleteMedicine(medicineId: number) {
    this.medicinesService.delete(medicineId).subscribe(() => {
      this.medicines = this.medicines.filter(medicine => medicine.id !== medicineId);
    });
  }
}
