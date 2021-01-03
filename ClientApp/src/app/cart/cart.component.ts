import { Component, OnInit, Inject } from '@angular/core';
import { CartService } from '../cart.service';
import { FormBuilder } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
  

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html'
})
export class CartComponent implements OnInit {
  items;
  checkoutForm;
  loggedIn;

  constructor(
    private cartService: CartService,
    private http: HttpClient,
    private formBuilder: FormBuilder,
    @Inject('BASE_URL') private baseUrl: string
  ) {
    this.checkoutForm = this.formBuilder.group({
      address: ''
    });
  }

  ngOnInit() {
    this.items = JSON.parse(this.cartService.getItems());
    this.loggedIn = localStorage.getItem('token');
  }

  onSubmit(customerData) {
    let order = {
      "Order": {
        userId: null,
        address: customerData.address,
        productIds: [],
        total: null
      },
      "UserToken": localStorage.getItem('token')
    };

    let products = JSON.parse(this.cartService.getItems());

    products.forEach(function (value, index) {
      order.Order.productIds.push(value.id)
      order.Order.total += value.price
    });

    // place order
    this.http.post<Order>(this.baseUrl + 'api/order/', order )
      .subscribe(result => {
        console.log(result)
      });

    // Process checkout data here
    this.items = this.cartService.clearCart();
    this.checkoutForm.reset();

    alert('Your order has been submitted');
  }

  clearCart() {
    this.items = this.cartService.clearCart();
    this.checkoutForm.reset();
    alert('Your cart is empty now!');
  }

}

interface Order {
  "userId": String,
  "productIds": String[],
  "address": String,
}
