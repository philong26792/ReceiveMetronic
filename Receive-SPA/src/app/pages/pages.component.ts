import { Component, OnInit} from '@angular/core';
import {scriptMain} from '../../app/app.helper';
@Component({
  selector: 'app-pages',
  templateUrl: './pages.component.html',
  styleUrls: ['./pages.component.css'],
})
export class PagesComponent implements OnInit {
  constructor() {}
  ngOnInit() {}

  ngAfterViewInit() {
    scriptMain();
  }
}
