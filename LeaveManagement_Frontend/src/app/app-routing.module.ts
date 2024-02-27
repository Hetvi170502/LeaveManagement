import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [

  {path: 'auth',
  loadChildren: () =>
    import('./authentication/authentication.module').then(
      (m) => m.AuthenticationModule
    ),
  },
  {path: 'manager',
  loadChildren: () =>
    import('./manager/manager.module').then(
      (m) => m.ManagerModule
    ),
  },
  {path: 'employe',
  loadChildren: () =>
    import('./employe/employe.module').then(
      (m) => m.EmployeModule
    ),
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
