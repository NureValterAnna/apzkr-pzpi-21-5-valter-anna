import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/_models/user';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-patients-list',
  templateUrl: './patients-list.component.html',
  styleUrls: ['./patients-list.component.css']
})
export class PatientsListComponent {
  patients: User[] = []
  selectedPatient?: User;

  constructor(private userService: UserService, private router: Router){}

  ngOnInit() {
    this.userService.getPatients().subscribe(
      (res: User[]) => {
        this.patients = res.sort((a, b) => a.id - b.id);
      }
    )
  }

  selectPatient(patientId: number) {
    this.router.navigate(['doctor/patient-info', patientId]);
  }
}
