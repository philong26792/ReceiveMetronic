import { Component, OnInit, ViewChild, TemplateRef } from '@angular/core';
import { ProductService } from 'src/app/@core/_services/product.service';
import { ProductModel } from 'src/app/@core/_models/product-model';
import { Pagination, PaginatedResult } from 'src/app/@core/_models/pagination';
import { showLoading, hideLoading, notifySuccess, notifyError} from './../../../../app.helper';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Router } from '@angular/router';
@Component({
  selector: 'app-product-main',
  templateUrl: './product-main.component.html',
  styleUrls: ['./product-main.component.scss']
})
export class ProductMainComponent implements OnInit {
  products: ProductModel[];
  modalRef: BsModalRef;
  productDelete: any;
  @ViewChild('templateDelete', {static: true}) modalDelete: TemplateRef<any>;
  pagination: Pagination = {
    currentPage: 1,
    itemsPerPage: 8,
    totalItems: 1,
    totalPages: 1,
  };
  constructor(private productService: ProductService,
    private modalService: BsModalService,
    private router: Router) { }

  ngOnInit() {
    this.loadData();
  }
  loadData() {
    showLoading();
    this.productService.getListAll(this.pagination.currentPage, this.pagination.itemsPerPage)
    .subscribe((res: PaginatedResult<ProductModel[]>) => {
      setTimeout(() => {
        hideLoading();
        this.products = res.result;
        this.pagination = res.pagination;
      }, 1000);
    }, (error) => {
      notifyError(error);
    });
  }
  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadData();
  }
  deleteProduct(product: ProductModel) {
    this.productDelete = product;
    this.modalRef = this.modalService.show(this.modalDelete, {class: 'modal-sm'})
  }
  decline(): void {
    this.productDelete = {};
    this.modalRef.hide();
  }
  confirmDelete() {
    this.productService.remove(this.productDelete.id).subscribe(res => {
      this.loadData();
      notifySuccess('Delete Product successed!');
      this.modalRef.hide();
    }, error => {
      notifyError(error);
    });
  }
  pageEdit(product: ProductModel) {
    this.productService.changProduct(product);
    this.productService.changeFlag('1');
    this.router.navigate(['/page/product/change']);
  }
  pageAdd() {
    this.productService.changProduct({});
    this.productService.changeFlag('0');
    this.router.navigate(['/page/product/change']);
  }
}
