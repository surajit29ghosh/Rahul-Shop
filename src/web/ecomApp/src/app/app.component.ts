import { Component, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';

import { CartService } from './services/cart.service'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnDestroy {
  title = 'ecomApp';

  cartCount: number = 0;
  subscription: Subscription;

  constructor(private cartService: CartService) {
    this.subscription = this.cartService.getCartCount().subscribe(message => {
      if (message) {
        this.cartCount = message;
      } else {
        // clear messages when empty message received
        this.cartCount = 0;
      }
    });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }



}
