import { EmployeLeaveListComponent } from './employe-leave-list/employe-leave-list.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LeaveAddComponent } from './leave-add/leave-add.component';
import { DashboardComponent } from './dashboard/dashboard.component';

const routes: Routes = [

  {
    path:'',
    component:DashboardComponent,
    children: [
      {path: 'leave-add' , component:LeaveAddComponent},
      {path: 'employeLeave' , component:EmployeLeaveListComponent}
    ],
  }


];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EmployeRoutingModule { }
