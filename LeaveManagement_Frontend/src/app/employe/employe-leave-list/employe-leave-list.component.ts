import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { NgToastService } from 'ng-angular-popup';
import { leave } from 'src/app/models/leave.model';
import { EmployeService } from 'src/app/services/employe.service';
import { LeaveAddComponent } from '../leave-add/leave-add.component';

@Component({
  selector: 'app-employe-leave-list',
  templateUrl: './employe-leave-list.component.html',
  styleUrls: ['./employe-leave-list.component.css']
})
export class EmployeLeaveListComponent implements OnInit
{
  leaves : leave[] = []
  userId: any;
  constructor(private employe : EmployeService , private toster : NgToastService ,  public dialog: MatDialog)
  {

  }

  ngOnInit(): void {
    this.empLeave();
    this.userId = this.retrieveUserIdFromLocalStorage();
  }
  retrieveUserIdFromLocalStorage(): string | null {
    const storedUserId = localStorage.getItem('currentUser');
    return storedUserId ? storedUserId.replace(/"/g, '') : null;
  }
  empLeave()
  {
    this.employe.empLeave()
    .subscribe (
      (data) =>
      {
        this.leaves = data;
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

  leaveCancel(leave:leave):void
  {
    leave.status='Cancel';
    this.employe.leaveCancel(leave)
    .subscribe(
      response => {
        console.log('Leave status updated successfully:', response);
        this.toster.success({detail:'success',summary:"leave Cancel",duration:3000});
        this.empLeave()
      },
      error => {
        console.error('Failed to update leave status:', error);
        this.toster.error({detail:'success',summary:error.error,duration:3000});

      }
    );
  }

  applyLeave() : void
  {
    let leave : leave = {
      id:0,
      userId : '',
      leaveTypeId : '',
      startDate : new Date(),
      endDate : new Date(),
      dateOfRequest : new Date(),
      reasonForLeave : '',
      status : '',
      user: {
        id : '',
        firstName : '',
        lastName : '',
        department : '',
        designation : '',
        emial : '',
        phoneNumber : '',
        password : '',
        roleNames : ''
      },
      leaveType : {
        type : '',
      }
    };
    const dialogRef = this.dialog.open(LeaveAddComponent, {
      data: leave ,
      width: '50%'
    });
    dialogRef.afterClosed().subscribe({
      next : (res) => {
        if(res != undefined)
        {
          this.employe.applyLeave({
            id : res.id,
            userId : this.userId,
            leaveTypeId : res.leaveTypeId,
            startDate : res.startDate,
            endDate : res.endDate,
            reasonForLeave : res.reasonForLeave,
            status:'InProgress'
          }).subscribe({
            next : () => {
              this.toster.success({detail:'success',summary:"Leave Apply!!",duration:3000});
              this.empLeave();
            },
          });
        }
      }
    });
  }

  isLeaveDatePast(startDate: Date): boolean {
    const today = new Date();
    startDate = new Date(startDate);
    return startDate < today;
  }
}
