import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgxSpinnerModule } from "ngx-spinner";
import { CommonModule } from '@angular/common';
@NgModule({
   declarations: [	
      AppComponent,
   ],
   imports: [
      CommonModule,
      HttpClientModule,
      BrowserModule,
      FormsModule,
      ReactiveFormsModule,
      AppRoutingModule,
      BrowserAnimationsModule,
      NgxSpinnerModule,
      PaginationModule.forRoot(),
      ModalModule.forRoot()
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ],
   schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppModule { }
