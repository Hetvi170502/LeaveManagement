import { Component, OnInit } from '@angular/core';
import { leave } from 'src/app/models/leave.model';
import { EmployeService } from 'src/app/services/employe.service';

@Component({
  selector: 'app-employe-leave-list',
  templateUrl: './employe-leave-list.component.html',
  styleUrls: ['./employe-leave-list.component.css']
})
export class EmployeLeaveListComponent implements OnInit
{
  leaves : leave[] = []

  constructor(private employe : EmployeService)
  {

  }

  ngOnInit(): void {
    this.empLeave()
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

}
