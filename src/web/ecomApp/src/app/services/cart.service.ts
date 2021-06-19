import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { environment } from 'src/environments/environment';

import {CartDomain } from '../domain/cart.domain';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  private cartApiURL = environment.cartApi;
  private cartCount = new Subject<number>();
  private items: number = 0;
  cart: CartDomain[] = [];
  cartId: string = '';

  getCartCount(): Observable<number> {
    return this.cartCount.asObservable();
  }

  constructor(private http: HttpClient) {
    if(localStorage.getItem("cartId"))
    {
      this.cartId = localStorage.getItem("cartId");
      this.getCart();
    }
   }

  addToCart(c:CartDomain) {
    this.items += 1;
    this.cartCount.next(this.items);

    let cartData = {
      CartId: this.cartId,
      ProductId: c.ProductId,
      ProductName: c.ProductName,
      Units: c.Units
    };

    this.http.post<any>(this.cartApiURL + 'addtocart', cartData)
    .subscribe(data => 
      {
        localStorage.setItem("cartId", data.CartId);
        this.getCart();
      });
  }

  getCart(){
    if(this.cartId){
      this.http.post<any[]>(this.cartApiURL + 'getcart', { CartId: this.cartId})
                .subscribe(data => 
                  {
                    this.cart = data;
                    this.items = this.cart.length;
                    this.cartCount.next(this.items);
                  });
    }
  }
 
  removeFromCart(c: CartDomain) {
    if(this.cartId){
      let cartData = {
        CartId: this.cartId,
        ProductId: c.ProductId,
        ProductName: c.ProductName,
        Units: c.Units
      };

      this.http.post<any[]>(this.cartApiURL + 'removefromcart', cartData)
                .subscribe(data => 
                  {
                    this.getCart();
                  });
    }
  }
}
