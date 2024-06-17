import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Stock } from '../_models/stock';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class MedicineStockService {

  constructor(private http: HttpClient) { }

  get() {
    return this.http.get<Stock[]>(`${environment.apiUrl}/api/stock`);
  }

  getById(stockId: number) {
    return this.http.get<Stock>(`${environment.apiUrl}/api/stock/${stockId}`);
  }

  update(stock: Stock) {
    return this.http.put<string>(`${environment.apiUrl}/api/stock`, {
      dispenserId: stock.dispenserId,
      medicineId: stock.medicineId,
      quantity: stock.quantity
    });
  }

  create(stock: Stock) {
    return this.http.post<string>(`${environment.apiUrl}/api/stock`, {
      medicineId: stock.medicineId,
      dispenserId: stock.dispenserId,
      quantity: stock.quantity
    });
  }

  delete(stockId: number) {
    return this.http.delete(`${environment.apiUrl}/api/stock/${stockId}`);
  }
}
