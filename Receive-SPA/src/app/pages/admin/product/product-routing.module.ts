import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductMainComponent } from './product-main/product-main.component';
import { ProductChangeComponent } from './product-change/product-change.component';


const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: '',
        component: ProductMainComponent,
      },
      {
        path: 'change',
        component: ProductChangeComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductRoutingModule { }
