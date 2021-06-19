import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

import { environment } from '../../environments/environment';

import { AllProduct, SearchProduct } from '../domain/product.domain';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private productApiURL: string = environment.productApi;

  constructor(private http: HttpClient) 
  { 

  }

  getAllProducts(product: SearchProduct): Observable<any[]> {
    return this.http.post<any[]>(this.productApiURL + 'getallproducts'
                                ,{
                                  "PageNo": product.pageNo,
                                  "PageSize": product.pageSize,
                                  "CategoryTerm": product.category,
                                  "SearchTerm": product.searchTerm 
                              });
  }

  getAllCategory(): Observable<any[]> {
    return this.http.post<any[]>(this.productApiURL + 'getcategories'
                                ,{
                                  "PageNo": 0,
                                  "PageSize": 0 
                              });
  }
}
