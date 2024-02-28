import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LeaveListComponent } from './leave-list/leave-list.component';
import { DashboardComponent } from './dashboard/dashboard.component';

const routes: Routes = [

  {
    path:'',
    component:DashboardComponent,
    children: [
      { path: 'leave-list' , component : LeaveListComponent}
    ],
  }


];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ManagerRoutingModule { }
