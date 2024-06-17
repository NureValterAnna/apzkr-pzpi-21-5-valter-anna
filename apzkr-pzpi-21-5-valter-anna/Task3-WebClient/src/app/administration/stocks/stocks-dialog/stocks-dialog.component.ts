import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Dispenser } from 'src/app/_models/dispenser';
import { Medicine } from 'src/app/_models/medicine';
import { Stock } from 'src/app/_models/stock';
import { DispensersService } from 'src/app/_services/dispensers.service';
import { MedicineStockService } from 'src/app/_services/medicine-stock.service';
import { MedicineService } from 'src/app/_services/medicine.service';

@Component({
  selector: 'app-stocks-dialog',
  templateUrl: './stocks-dialog.component.html',
  styleUrls: ['./stocks-dialog.component.css']
})
export class StocksDialogComponent {
  createForm: FormGroup = this.fb.group({
    dispenser: [null, Validators.required],
    medicine: [null, Validators.required],
    quantity: ['', Validators.required],
  });

  constructor(
    private fb: FormBuilder,
    private stockService: MedicineStockService,
    private dispenserService: DispensersService,
    private medicineService: MedicineService,
    private dialogRef: MatDialogRef<StocksDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { stockId: number, actionName: string, stocks: Stock[] }
  ) {}

  ngOnInit(): void {
    const temperatureUnit: string = "C";
    this.dispenserService.get(temperatureUnit).subscribe(
      dispensers => {
        this.dispensers = dispensers;
    });

    this.medicineService.get().subscribe(medicines => {
      this.medicines = medicines;
    });

    if (this.data.stockId) {
      this.stockService.getById(this.data.stockId).subscribe((stock) => {
        this.createForm.patchValue({
          dispenser: this.dispensers.find(d => d.id === stock.dispenserId),
          medicine: this.medicines.find(m => m.id === stock.medicineId),
          quantity: stock.quantity,
        });
      });
    }
  }

  dispensers: Dispenser[] = []; 
  medicines: Medicine[] = []; 

  get dispenser() {
    return this.createForm.get('dispenser');
  }

  get medicine() {
    return this.createForm.get('medicine');
  }

  get quantity() {
    return this.createForm.get('quantity');
  }

  onSubmit() {
    if (this.createForm.valid) {
      const stock: Stock = {
        id: this.data.stockId,
        dispenserId: this.createForm.value.dispenser.id,
        dispenserName: this.createForm.value.dispenser.dispenserName,
        medicineId: this.createForm.value.medicine.id,
        medicineTitle: this.createForm.value.medicine.title,
        quantity: this.createForm.value.quantity,
      };

      switch (this.data.actionName) {
        case 'create':
          this.stockService.create(stock).subscribe(() => {
            this.dialogRef.close();
          });
          break;
        case 'update':
          this.stockService.update(stock).subscribe(() => {
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
