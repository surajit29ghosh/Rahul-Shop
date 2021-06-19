import { Component, OnInit } from '@angular/core';

import { CartService } from '../../services/cart.service';
import { ProductService } from '../../services/product.service';

import { CartDomain } from '../../domain/cart.domain';
import { SearchProduct } from 'src/app/domain/product.domain';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  products: any[];
  category: any[];
  categorySearchTerm: string;
  productSearchTerm: string;
  currentPage: number;
  pageSize: number;

  constructor(private productService: ProductService, private cartService: CartService) { }

  ngOnInit(): void {
    this.categorySearchTerm = '';
    this.productSearchTerm = '';
    this.currentPage = 1;
    this.pageSize = environment.pageSize;
    this.getAllProducts();
    this.getAllCategory();
  }

  getAllProducts() {
    let getAll = new SearchProduct();

    getAll.pageNo = this.currentPage;
    getAll.pageSize = this.pageSize;
    getAll.category = this.categorySearchTerm;
    getAll.searchTerm = this.productSearchTerm;
    

    this.productService.getAllProducts(getAll)
        .subscribe(data => this.products = data);
  }

  getAllCategory() {
    this.productService.getAllCategory()
        .subscribe(data => this.category = data);
  }

  addToCart(product: any) {
    let newItem = new CartDomain();

    newItem.ProductId = product.ProductId;
    newItem.ProductName = product.ProductName;
    newItem.Units = 1;

    this.cartService.addToCart(newItem);
    
    return false;
  }

}
