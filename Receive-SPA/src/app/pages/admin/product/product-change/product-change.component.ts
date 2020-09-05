import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ProductService } from 'src/app/@core/_services/product.service';
import { notifySuccess, notifyError} from './../../../../app.helper';
@Component({
  selector: 'app-product-change',
  templateUrl: './product-change.component.html',
  styleUrls: ['./product-change.component.scss']
})
export class ProductChangeComponent implements OnInit {
  flag: string;
  constructor(private router: Router,
              private productService: ProductService) { }

  ngOnInit() {
    this.productService.currentFlag.subscribe(res => this.flag = res);
  }
  back() {
    this.router.navigate(['/page/product']);
  }
}
