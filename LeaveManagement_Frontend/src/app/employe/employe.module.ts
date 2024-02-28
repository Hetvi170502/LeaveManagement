import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { EmployeRoutingModule } from './employe-routing.module';
import { LeaveAddComponent } from './leave-add/leave-add.component';
import { EmployeLeaveListComponent } from './employe-leave-list/employe-leave-list.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    LeaveAddComponent,
    EmployeLeaveListComponent,
    DashboardComponent
  ],
  imports: [
    CommonModule,
    EmployeRoutingModule,
    MatDialogModule,
    ReactiveFormsModule
  ]
})
export class EmployeModule { }
