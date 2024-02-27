import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit
{
  loginForm!: FormGroup;

  get email() {return this.loginForm.get('email');}
  get password() {return this.loginForm.get('password');}


  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthenticationService,
    private router : Router
  ) { }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });
  }

  get formControls() {
    return this.loginForm.controls;
  }

  onSubmit(): void {

    if (this.loginForm.invalid) {
      return;
    }

    const email = this.loginForm.value.email;
    const password = this.loginForm.value.password;

    this.authService.login(email, password)
      .subscribe(
        (response) => {

          localStorage.setItem('currentUser', JSON.stringify(response.user.id));

          if (response.user.roleNames == 'Manager')
          {
              this.router.navigate(['manager/leave-list'])
              console.log('Redirecting to manager dashboard');
          }
          else if (response.user.roleNames == 'Employe')
          {
              this.router.navigate(['employe/employeLeave'])
              console.log('Redirecting to employee dashboard');
          }
          else
          {
              console.log('Unknown role');
          }

        },
        (error) => {
          // Handle login error
          console.error('Login failed:', error);
        }
      );
  }
}
