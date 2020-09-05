import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login.component';
import { LoginGuard } from '../_guards/login.guard';


const routes: Routes = [
  {
    path: "",
    children: [
      {
        path: "",
        component: LoginComponent,
        data: {title: "Main"},
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LoginRoutingModule { }
