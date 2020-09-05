import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ProductRoutingModule } from './product-routing.module';
import { ProductMainComponent } from './product-main/product-main.component';
import { ProductChangeComponent } from './product-change/product-change.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ModalModule } from 'ngx-bootstrap/modal';
@NgModule({
  declarations: [
    ProductMainComponent,
    ProductChangeComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    ProductRoutingModule,
    PaginationModule.forRoot(),
    ModalModule.forRoot()
  ],
  schemas: [
    NO_ERRORS_SCHEMA
              ]
})
export class ProductModule { }
