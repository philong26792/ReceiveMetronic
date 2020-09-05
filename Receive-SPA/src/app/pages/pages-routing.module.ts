import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { PagesComponent } from './pages.component';
import { AuthGuard } from '../_guards/auth.guard';


const routes: Routes = [
    {
      path: '', 
      component: PagesComponent,
      canActivate: [AuthGuard],
      children: [
        {path: 'home', component: HomeComponent},
        {path: 'product', loadChildren: () => import('./admin/product/product.module').then(m => m.ProductModule)}
      ]
    }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PagesRoutingModule { }
