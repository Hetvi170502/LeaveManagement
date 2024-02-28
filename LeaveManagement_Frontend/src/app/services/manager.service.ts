import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { leave } from '../models/leave.model';
import { DatePipe } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class ManagerService {

  private baseUrl = 'https://localhost:7069/api/';

  constructor(private http : HttpClient , private datePipe : DatePipe) { }

  getAllLeave() : Observable<leave[]>
  {
    return this.http.get<leave[]>(this.baseUrl+'Leave');
  }

  statusUpdate(leave : leave) : Observable<any>
  {
    return this.http.put<any>(this.baseUrl+'Leave',leave);
  }

  downloadAllLeaves(): Observable<Blob> {
    return this.http.get(`${this.baseUrl}/Leave/CSV`, { responseType: 'blob' });
  }
  convertToCSV(leaves: leave[]): string {
    const headers = ['Employee Name', 'Start Date', 'End Date', 'Reason' , 'Leave Type' ,'DateOfRequest'];
    const csvData = [headers.join(',')];

    for (const leave of leaves) {
      const startDate = this.datePipe.transform(leave.startDate, 'yyyy-MM-dd');
      const endDate = this.datePipe.transform(leave.endDate, 'yyyy-MM-dd');
      const dateOfRequest = this.datePipe.transform(leave.dateOfRequest, 'yyyy-MM-dd');
      const employeeName = `${leave.user.firstName} ${leave.user.lastName}`;
      const row = [employeeName, startDate , endDate, leave.reasonForLeave , leave.leaveType.type , dateOfRequest];
      csvData.push(row.join(','));
    }

    return csvData.join('\n');
  }
}
