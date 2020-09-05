import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable, BehaviorSubject } from 'rxjs';
import { ProductModel } from '../_models/product-model';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';
import { Category } from '../_models/category';
import { Product } from '../_models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  baseUrl = environment.apiUrl;
  flagSource = new BehaviorSubject<string>('0');
  currentFlag = this.flagSource.asObservable();
  productSource = new BehaviorSubject<object>({});
  currentProduct = this.productSource.asObservable();
  constructor(private http: HttpClient) { }
  getListAll(page?, itemsPerPage?): Observable<PaginatedResult<ProductModel[]>> {
    const paginatedResult: PaginatedResult<ProductModel[]> = new PaginatedResult<ProductModel[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }
    return this.http.get<ProductModel[]>(this.baseUrl + 'product/getProducts', { observe: 'response', params })
      .pipe(
        map(response => {
          console.log(response);
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination') != null) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
          }
          return paginatedResult;
        }),
      );
  }
  getAllCategories(): Observable<Category[]> {
    return this.http.get<Category[]>(this.baseUrl + 'product/categorys/', {});
  }
  remove(id: any): Observable<any> {
    return this.http.get<any>(this.baseUrl + 'product/remove/' + id, {});
  }
  add(product: Product): Observable<any> {
    return this.http.post<any>(this.baseUrl + 'product/add/', product, {});
  }
  update(product: Product): Observable<any> {
    return this.http.post<any>(this.baseUrl + 'product/update/', product, {});
  }
  changeFlag(flag: string) {
    this.flagSource.next(flag);
  }
  changProduct(product: any){
    this.productSource.next(product);
  }
}
