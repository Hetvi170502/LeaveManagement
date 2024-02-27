import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EmployeRoutingModule } from './employe-routing.module';
import { LeaveAddComponent } from './leave-add/leave-add.component';
import { EmployeLeaveListComponent } from './employe-leave-list/employe-leave-list.component';


@NgModule({
  declarations: [
    LeaveAddComponent,
    EmployeLeaveListComponent
  ],
  imports: [
    CommonModule,
    EmployeRoutingModule
  ]
})
export class EmployeModule { }
