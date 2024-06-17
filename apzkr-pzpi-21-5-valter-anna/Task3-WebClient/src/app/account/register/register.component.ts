import { Component, EventEmitter, Output } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {


  registerForm: FormGroup = this.fb.group({
    name: ['', [
      Validators.required,
      Validators.minLength(1),
      Validators.pattern(/^[A-Z][a-zA-Z]*$/)
    ]],
    surname: ['', [
      Validators.required,
      Validators.minLength(1),
      Validators.pattern(/^[A-Z][a-zA-Z]*$/)
    ]],
    age: ['', [
      Validators.required,
      Validators.min(1)
    ]],
    email: ['', [
      Validators.required,
      Validators.email
    ]],
    password: ['', [
      Validators.required,
      Validators.minLength(8),
      Validators.maxLength(32),
      Validators.pattern(/[A-Z]+/),
      Validators.pattern(/[a-z]+/), 
      Validators.pattern(/[0-9]+/),
      Validators.pattern(/[\!\?\*\.]+/) 
    ]],
    confirmPassword: ['', [Validators.required, this.passwordMatchValidator]]
  }, {
    validators: this.passwordMatchValidator
  });

  constructor(private fb: FormBuilder, private accountService: AccountService) { }

  get name() {
    return this.registerForm.get('name');
  }

  get surname() {
    return this.registerForm.get('surname');
  }

  get age() {
    return this.registerForm.get('age');
  }

  get email() {
    return this.registerForm.get('email');
  }

  get password() {
    return this.registerForm.get('password');
  }

  passwordMatchValidator(formGroup: FormGroup) {
    const password = formGroup.get('password')?.value;
    const confirmPassword = formGroup.get('confirmPassword')?.value;

    return password === confirmPassword ? null : { mismatch: true };
  }

  onSubmit(){
    if (this.registerForm.valid) {
      this.accountService.register(
        this.name?.value,
        this.surname?.value,
        this.age?.value,
        this.email?.value, 
        this.password?.value
      ).subscribe(
        
      );
    }
  }

}
