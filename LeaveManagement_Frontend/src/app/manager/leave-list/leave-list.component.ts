import { Component, OnInit } from '@angular/core';
import { NgToastService } from 'ng-angular-popup';
import { leave } from 'src/app/models/leave.model';
import { ManagerService } from 'src/app/services/manager.service';

@Component({
  selector: 'app-leave-list',
  templateUrl: './leave-list.component.html',
  styleUrls: ['./leave-list.component.css']
})
export class LeaveListComponent implements OnInit
{
  leaveList : leave[] = [];

  constructor(private manager : ManagerService , private toster : NgToastService)
  {

  }
  ngOnInit(): void {
   this.getList();
  }

  getList()
  {
    this.manager.getAllLeave()
    .subscribe(
      (data) =>
      {
        this.leaveList = data;
      },
      error => {
        console.error(error);

      }
    )
  }
  calculateTotalDays(startDate: Date, endDate: Date): number {
    const oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds
    const start = new Date(startDate);
    const end = new Date(endDate);
    return Math.round(Math.abs((start.getTime() - end.getTime()) / oneDay)+1);
  }

  leaveApprove(leave:leave):void
  {

    leave.status='Approved';
    this.manager.statusUpdate(leave)
    .subscribe(
      response => {
        console.log('Leave status updated successfully:', response);
        this.toster.success({detail:'success',summary:'Updated Successfully!!',duration:3000});
        this.getList()
      },
      error => {
        console.error('Failed to update leave status:', error);
        // Handle error, show error message, or log the error as needed
        // this.toster.error('Error occurred while updating leave status.', );
      }
    );
  }

  leaveReject(leave:leave):void
  {
    leave.status='Reject';
    this.manager.statusUpdate(leave)
    .subscribe(
      response => {
        console.log('Leave status updated successfully:', response);
        this.toster.success({detail:'success',summary:response,duration:3000});
        this.getList()
      },
      error => {
        console.error('Failed to update leave status:', error);
        this.toster.error({detail:'error',summary:error.error,duration:3000});

      }
    );
  }

  downloadAllLeaves() {
    this.manager.getAllLeave().subscribe(leaves => {
      const csvData = this.manager.convertToCSV(leaves);
      const blob = new Blob([csvData], { type: 'text/csv' });
      const url = window.URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = url;
      a.download = 'all_leaves.csv'; // specify filename
      document.body.appendChild(a);
      a.click();
      window.URL.revokeObjectURL(url);
      document.body.removeChild(a);
    });
  }

}
