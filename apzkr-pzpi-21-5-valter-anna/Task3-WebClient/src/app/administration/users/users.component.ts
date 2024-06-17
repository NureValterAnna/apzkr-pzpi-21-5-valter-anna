import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { User } from 'src/app/_models/user';
import { UserService } from 'src/app/_services/user.service';
import { UserDialogComponent } from './user-dialog/user-dialog.component';
import * as XLSX from 'xlsx';
import { saveAs } from 'file-saver';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent {
  users: User[] = []

  constructor(private userService: UserService, private dialogRef: MatDialog, private toastr: ToastrService){}

  ngOnInit() {
    this.userService.get().subscribe(
      (res: User[]) => {
        this.users = res.sort((a, b) => a.id - b.id);
      }
    )
  }

  openDialog(userId: number) {
    this.dialogRef.open(UserDialogComponent,
    {
      data: { userId }
    });

    this.dialogRef.afterAllClosed.subscribe(() => {
      this.userService.get().subscribe(
        (res: User[]) => {
          this.users = res.sort((a, b) => a.id - b.id);
        }
      );
    });
  }

  deleteUser(userId: number) {
    this.userService.delete(userId).subscribe(() => {
      this.users = this.users.filter(user => user.id !== userId);
    });
  }

  export() {
    this.userService.export().subscribe(
      (jsonData: any) => {
        const ws: XLSX.WorkSheet = XLSX.utils.json_to_sheet(jsonData);
        const wb: XLSX.WorkBook = XLSX.utils.book_new();
        XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');

        const excelBuffer: any = XLSX.write(wb, { bookType: 'xlsx', type: 'array' });

        const blob = new Blob([excelBuffer], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });

        const formattedDate = new Date().toISOString().replace(/\..+/, '');
        saveAs(blob, `RESERVATIONS_BACKUP_${formattedDate}.xlsx`);
      });
  }

  import(event: any): void {
    const fileList: FileList | null = event.target.files;

    if (fileList && fileList.length > 0) {
      const file: File = fileList[0];

      if (this.isExcelFile(file)) {
        const fileReader: FileReader = new FileReader();

        fileReader.onload = (e: any) => {
          const data: string = e.target.result;
          const workbook: XLSX.WorkBook = XLSX.read(data, { type: 'binary' });

          const jsonData: any[] = XLSX.utils.sheet_to_json(workbook.Sheets[workbook.SheetNames[0]]);

          console.log(JSON.stringify(jsonData));
          this.userService.import(JSON.stringify(jsonData)).subscribe((res) => {
            this.users = res;
          });
        };

        fileReader.readAsBinaryString(file);
      } else {
        this.toastr.error('Invalid file format. Please select a valid .xlsx file.', 'Error!');
      }
    }
  }

  private isExcelFile(file: File): boolean {
    return file.name.endsWith('.xlsx');
  }
}
