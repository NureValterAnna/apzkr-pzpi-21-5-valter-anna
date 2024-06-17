import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DispensersService } from 'src/app/_services/dispensers.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Dispenser } from 'src/app/_models/dispenser';

@Component({
  selector: 'app-dispenser-dialog',
  templateUrl: './dispenser-dialog.component.html',
  styleUrls: ['./dispenser-dialog.component.css']
})
export class DispenserDialogComponent implements OnInit {
  createForm: FormGroup = this.fb.group({
    dispenserName: ['', Validators.required],
    location: ['', Validators.required],
  });

  constructor(
    private fb: FormBuilder,
    private dispenserService: DispensersService,
    private dialogRef: MatDialogRef<DispenserDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { dispenserId: number, actionName: string }
  ) {}

  ngOnInit(): void {
    switch (this.data.actionName) {
      case 'update':
        this.dispenserService.getById(this.data.dispenserId).subscribe(
          (dispenser: Dispenser) => {
            this.createForm.patchValue({
              dispenserName: dispenser.dispenserName,
              location: dispenser.location,
            });
          }
        );
        break;
      default:
        break;
    }
  }

  get dispenserName() {
    return this.createForm.get('dispenserName');
  }

  get location() {
    return this.createForm.get('location');
  }

  onSubmit() {
    if (this.createForm.valid) {
      const dispenser: Dispenser = {
        id: this.data.dispenserId,
        dispenserName: this.createForm.value.dispenserName,
        location: this.createForm.value.location,
      };

      switch (this.data.actionName) {
        case 'create':
          this.dispenserService.create(dispenser).subscribe(() => {
            this.dialogRef.close();
          });
          break;
        case 'update':
          this.dispenserService.update(dispenser).subscribe(() => {
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
