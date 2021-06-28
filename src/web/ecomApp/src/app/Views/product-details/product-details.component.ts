import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {

  productId: string;
  product: any;
  productQuantity: number;
  constructor(private route: ActivatedRoute, 
              private router: Router,
              private productService: ProductService) {
    this.route.queryParams.subscribe(params => {
      if(this.route.snapshot.paramMap.get('id'))
      {
        this.productId = params['id'];
        this.productQuantity = 1;
      }
      else
      {
        this.router.navigate(['home']);
      }
    });
   }

  ngOnInit(): void {
    this.getProductInfo();
  }

  getProductInfo() {
    this.productService.getProductDetails(this.productId)
        .subscribe(data => this.product = data);
  }

  plus() {
    //if(this.productQuantity < this.product.AvailableQuantity)
      this.productQuantity += 1;
  }

  minus() {
    if(this.productQuantity > 0)
      this.productQuantity -= 1;
  }



}
