import { AuthenticationService } from 'src/app/services/authentication.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { register } from 'src/app/models/register.model';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit
{

  registrationForm : FormGroup

  get firstName() {return this.registrationForm.get('firstName');}
  get lastName() {return this.registrationForm.get('lastName');}
  get phoneNumber() {return this.registrationForm.get('phoneNumber');}
  get department() {return this.registrationForm.get('department');}
  get designation() {return this.registrationForm.get('designation');}
  get roleNames() {return this.registrationForm.get('roleNames');}
  get email() {return this.registrationForm.get('email');}
  get password() {return this.registrationForm.get('password');}

  constructor(private authentication : AuthenticationService , private formBuilder : FormBuilder ,
                private router : Router , private toster : NgToastService)
  {
    this.registrationForm = this.formBuilder.group({
      id : [''],
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      department: ['', Validators.required],
      designation: ['', Validators.required],
      password: ['', Validators.required],
      roleNames: ['', Validators.required]
    });
  }

  ngOnInit(): void {

  }
  onSubmit() {
    if (this.registrationForm.valid) {
      const user: register = this.registrationForm.value;
      this.authentication.register(user).subscribe(
        (response) => {
          this.toster.success({detail:'Register Successsfully!!', duration:2000})
          this.router.navigate(['manager/leave-list']);
          console.log('Registration successful', response);
          // You can redirect the user or show a success message here
        },
        (error) => {
          this.toster.error({detail:'Register Failed!!', duration:2000})
          console.error('Registration failed', error);
          // Handle error, show error message, etc.
        }
      );
    }
  }
}
