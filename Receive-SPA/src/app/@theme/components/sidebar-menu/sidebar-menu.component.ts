import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sidebar-menu',
  templateUrl: './sidebar-menu.component.html',
  styleUrls: ['./sidebar-menu.component.css']
})
export class SidebarMenuComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit() {
  }
  routerPageProduct() {
    this.router.navigate(['/page/product']);
  }
}
