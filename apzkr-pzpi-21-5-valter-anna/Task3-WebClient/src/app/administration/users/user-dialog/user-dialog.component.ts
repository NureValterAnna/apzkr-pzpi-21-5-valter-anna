import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { User } from 'src/app/_models/user';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-user-dialog',
  templateUrl: './user-dialog.component.html',
  styleUrls: ['./user-dialog.component.css']
})
export class UserDialogComponent implements OnInit {
  createForm: FormGroup = this.fb.group({
    role: ['', Validators.required],
  });

  roles: string[] = ["admin", "doctor", "patient"];

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private dialogRef: MatDialogRef<UserDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { userId: number }
  ) {}

  ngOnInit(): void {
    this.userService.getById(this.data.userId).subscribe(

    )
  }

  get role(){
    return this.createForm.get('role');
  }

  onSubmit(){
    if (this.createForm.valid) {
      const user: User = {
        id: this.data.userId,
        role: this.createForm.value.role
      };

      this.userService.update(user).subscribe(() => {
        this.dialogRef.close();
      })  
    }
  }
}
