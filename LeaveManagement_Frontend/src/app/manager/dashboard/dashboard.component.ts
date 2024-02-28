import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent {
  isLoggedIn(): boolean {
    return localStorage.getItem('email') !== null;
  }

  constructor(private router : Router){}

  logout() {
    // Clear localStorage data when logging out
    localStorage.removeItem('email');
    localStorage.removeItem('currentUser');
    this.router.navigate(['auth/login'])

  }
  retrieveemailFromLocalStorage() {

    const storedUserId = localStorage.getItem('email');
    return storedUserId ? storedUserId.replace(/"/g, '') : null;
  }
}
