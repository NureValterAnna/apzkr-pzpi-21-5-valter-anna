import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { map } from 'rxjs';

export const doctorGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const toastr = inject(ToastrService);

  return accountService.user.pipe(
    map(user => {
      console.log(user);
      if(!user) {
        return false;
      } 

      if(accountService.getRole() === "doctor"){
        return true;
      } else{
        toastr.error('You cannot enter this panel');
        return false;
      }
    })
  )
};
