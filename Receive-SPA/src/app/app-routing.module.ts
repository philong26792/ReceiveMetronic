import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './_guards/auth.guard';
import { LoginGuard } from './_guards/login.guard';

const routes: Routes = [
  {path: '', loadChildren: () => import('./login/login.module').then(m => m.LoginModule), canActivate: [LoginGuard]},
  {path: 'page', loadChildren: () => import('./pages/pages.module').then(m => m.PagesModule), canActivate: [AuthGuard]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
