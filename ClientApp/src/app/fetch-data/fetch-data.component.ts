import { Component, Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { CartService } from '../cart.service';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public product: Product;
  public vendors: Vendor[];

  constructor(
    private route: ActivatedRoute, http: HttpClient,
    private cartService: CartService,
    @Inject('BASE_URL') baseUrl: string
  ) {
    let productId;

    this.route.paramMap.subscribe(params => {
      productId = params.get('productId')
    })

    http.get<Product>(baseUrl + 'api/product/' + productId)
        .subscribe(result => {
          this.product = result;
        });
   }
  addToCart(product) {
    this.cartService.addToCart(product);
    window.alert('Your product has been added to the cart!');
  }

}

interface Product {
  name: string;
  price: number;
  vendors: Vendor[];
}

interface Vendor {
    name: string
}
