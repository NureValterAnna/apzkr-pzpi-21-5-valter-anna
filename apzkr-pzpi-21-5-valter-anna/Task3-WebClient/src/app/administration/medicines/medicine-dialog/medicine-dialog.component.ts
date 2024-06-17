import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MedicineService } from 'src/app/_services/medicine.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Medicine } from 'src/app/_models/medicine';

@Component({
  selector: 'app-medicine-dialog',
  templateUrl: './medicine-dialog.component.html',
  styleUrls: ['./medicine-dialog.component.css']
})
export class MedicineDialogComponent implements OnInit {
    createForm: FormGroup = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
    });
  
    constructor(
      private fb: FormBuilder,
      private medicinesService: MedicineService,
      private dialogRef: MatDialogRef<MedicineDialogComponent>,
      @Inject(MAT_DIALOG_DATA) public data: { medicineId: number, actionName: string }
    ) {}

    ngOnInit(): void {
      switch (this.data.actionName) {
        case 'update':
          this.medicinesService.getById(this.data.medicineId).subscribe(
            (medicine: Medicine) => {
              this.createForm.patchValue({
                title: medicine.title,
                description: medicine.description,
              });
            }
          );
          break;
        default:
          break;
      }
    }

    get title() {
      return this.createForm.get('title');
    }
  
    get description() {
      return this.createForm.get('description');
    }
  
    onSubmit() {
      if (this.createForm.valid) {
        const medicine: Medicine = {
          id: this.data.medicineId,
          title: this.createForm.value.title,
          description: this.createForm.value.description,
        };
  
        switch (this.data.actionName) {
          case 'create':
            this.medicinesService.create(medicine).subscribe(() => {
              this.dialogRef.close();
            });
            break;
          case 'update':
            this.medicinesService.update(medicine).subscribe(() => {
              this.dialogRef.close();
            });
            break;
          default:
            this.dialogRef.close();
            break;
        }
      }
    }
}
