import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { leave } from '../models/leave.model';
import { DatePipe } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class EmployeService {

  private baseUrl = 'https://localhost:7069/api/';


  constructor(private http: HttpClient ) { }


  empLeave() : Observable<leave[]>
  {
    const userId = this.retrieveUserIdFromLocalStorage();
    return this.http.get<leave[]>(this.baseUrl+'Leave/'+ userId);
  }
  retrieveUserIdFromLocalStorage(): string | null {
    const storedUserId = localStorage.getItem('currentUser');
    return storedUserId ? storedUserId.replace(/"/g, '') : null;
  }

  leaveCancel(leave : leave) : Observable<any>
  {
    return this.http.put<any>(this.baseUrl+'Leave',leave);
  }

  applyLeave(leave : any) : Observable<any>
  {
    return this.http.post<any>(this.baseUrl+'Leave',leave);
  }


}
