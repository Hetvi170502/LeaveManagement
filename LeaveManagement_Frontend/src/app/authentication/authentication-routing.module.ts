import { RegisterComponent } from './register/register.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { NavbarComponent } from './navbar/navbar.component';

const routes: Routes = [

  {
    path:'',
    component:NavbarComponent,
    children: [
      {path:'',redirectTo: 'login' , pathMatch: 'full'},
      {path:'login' , component:LoginComponent},
      {path:'register' , component:RegisterComponent},
      {path:'**',redirectTo: 'login'}
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthenticationRoutingModule { }
