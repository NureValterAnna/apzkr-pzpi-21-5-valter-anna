import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { Stock } from 'src/app/_models/stock';
import { MedicineStockService } from 'src/app/_services/medicine-stock.service';
import { StocksDialogComponent } from './stocks-dialog/stocks-dialog.component';

@Component({
  selector: 'app-stocks',
  templateUrl: './stocks.component.html',
  styleUrls: ['./stocks.component.css']
})
export class StocksComponent implements OnInit {
  stocks: Stock[] = []

  constructor(private stockService: MedicineStockService, private dialogRef: MatDialog, private toastr: ToastrService){}

  ngOnInit() {
    this.stockService.get().subscribe(
      (res: Stock[]) => {
        this.stocks = res.sort((a, b) => a.id - b.id);
      }
    )
  }

  openDialog(stockId: number, actionName: string) {
    this.dialogRef.open(StocksDialogComponent,
    {
      data: { stockId, actionName, stocks: this.stocks }
    });

    this.dialogRef.afterAllClosed.subscribe(() => {
      this.stockService.get().subscribe(
        (res: Stock[]) => {
          this.stocks = res.sort((a, b) => a.id - b.id);
        }
      );
    });
  }

  deleteStock(stockId: number) {
    this.stockService.delete(stockId).subscribe(() => {
      this.stocks = this.stocks.filter(stock => stock.id !== stockId);
    });
  }

}
