import { Component } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  loginForm: FormGroup = this.fb.group({
    email: ['', [Validators.required]],
    password: ['', Validators.required]
  });

  constructor(
    private fb: FormBuilder, 
    private accountService: AccountService,
    private router: Router,
    ) { }

  get email() {
    return this.loginForm.get('email');
  }

  get password() {
    return this.loginForm.get('password');
  }

  onSubmit() {
      if (this.loginForm.valid) {
       this.accountService.login( this.email?.value, this.password?.value).subscribe({
        next: (user) => {
          if(user.Role == "admin") this.router.navigateByUrl("admin/dispensers");
          if(user.Role == "doctor") this.router.navigate(['doctor/patients']);
          if(user.Role == "patient") this.router.navigate(['patient/prescriptions']);
          console.log('Login successful', user);
        },
        error: (error) => {
          console.error('Login failed', error);
        }
      });
      }
    }

}
