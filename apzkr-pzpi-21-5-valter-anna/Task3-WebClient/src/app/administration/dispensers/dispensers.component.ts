import { Component, OnInit } from '@angular/core';
import { Dispenser } from 'src/app/_models/dispenser';
import { DispensersService } from 'src/app/_services/dispensers.service';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { DispenserDialogComponent } from './dispenser-dialog/dispenser-dialog.component';

@Component({
  selector: 'app-dispensers',
  templateUrl: './dispensers.component.html',
  styleUrls: ['./dispensers.component.css']
})
export class DispensersComponent implements OnInit {
  dispensers: Dispenser[] = []
  _isFarinhate: boolean = false;

  constructor(private dispensersService: DispensersService, private dialogRef: MatDialog){}

  ngOnInit() {
    this.loadDispensers();
  }

  get isFarinhate(): boolean {
    return this._isFarinhate;
  }

  set isFarinhate(value: boolean) {
    this._isFarinhate = value;
    this.loadDispensers();
  }

  loadDispensers() {
    const temperatureUnit: string = this.isFarinhate ? "F" : "C";
    this.dispensersService.get(temperatureUnit).subscribe(
      (res: Dispenser[]) => {
        this.dispensers = res.sort((a, b) => a.id - b.id);
      }
    );
  }

  openDialog(dispenserId: number, actionName: string) {
    this.dialogRef.open(DispenserDialogComponent, {
      data: { dispenserId, actionName }
    });

    const temperatureUnit: string = "C";
    this.dialogRef.afterAllClosed.subscribe(() => {
      this.dispensersService.get(temperatureUnit).subscribe(
        (res: Dispenser[]) => {
          this.dispensers = res;
        }
      );
    });
  }

  deleteDispenser(dispenserId: number) {
    this.dispensersService.delete(dispenserId).subscribe(() => {
      this.dispensers = this.dispensers.filter(dispenser => dispenser.id !== dispenserId);
    });
  }
}
