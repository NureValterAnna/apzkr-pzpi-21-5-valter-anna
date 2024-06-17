import { Component } from '@angular/core';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-patient-sidebar',
  templateUrl: './patient-sidebar.component.html',
  styleUrls: ['./patient-sidebar.component.css']
})
export class PatientSidebarComponent {
  name: string = "";

  constructor(public accountService: AccountService) { }

  ngOnInit(): void {
    this.name = this.accountService.getName();
  }

  logout(){
    this.accountService.logout();
  }
}
