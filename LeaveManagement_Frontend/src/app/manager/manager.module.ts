import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';

import { ManagerRoutingModule } from './manager-routing.module';
import { LeaveListComponent } from './leave-list/leave-list.component';
import { DashboardComponent } from './dashboard/dashboard.component';


@NgModule({
  declarations: [
    LeaveListComponent,
    DashboardComponent
  ],
  imports: [
    CommonModule,
    ManagerRoutingModule
  ],
  providers: [
    DatePipe, // Provide DatePipe here
  ],
})
export class ManagerModule { }
