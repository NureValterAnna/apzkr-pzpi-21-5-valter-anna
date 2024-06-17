import { Component } from '@angular/core';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-admin-sidebar',
  templateUrl: './admin-sidebar.component.html',
  styleUrls: ['./admin-sidebar.component.css']
})
export class AdminSidebarComponent {

  name: string = "";

  constructor(public accountService: AccountService) { }

  ngOnInit(): void {
    this.name = this.accountService.getName();
  }

  logout(){
    this.accountService.logout();
  }

}
