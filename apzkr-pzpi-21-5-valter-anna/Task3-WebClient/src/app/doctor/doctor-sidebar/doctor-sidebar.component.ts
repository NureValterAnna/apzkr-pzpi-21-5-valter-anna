import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-doctor-sidebar',
  templateUrl: './doctor-sidebar.component.html',
  styleUrls: ['./doctor-sidebar.component.css']
})
export class DoctorSidebarComponent implements OnInit {
  name: string = "";

  constructor(public accountService: AccountService) { }

  ngOnInit(): void {
    this.name = this.accountService.getName();
  }
  
  logout(){
    this.accountService.logout();
  }
}
