import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { register } from '../models/register.model';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private baseUrl = 'https://localhost:7069/api/User/';

  constructor(private http: HttpClient) { }

  login(email: string, password: string): Observable<any> {
    return this.http.post<any>(this.baseUrl+'Login', { email, password })
    .pipe(
      catchError(error => {
        return throwError(error);
      })
    );
  }

  register(register:register) : Observable<any>
  {
    return this.http.post<any>(this.baseUrl+'register',register);
  }
}
