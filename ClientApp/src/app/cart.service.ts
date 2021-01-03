import { Injectable } from "@angular/core";

@Injectable({
  providedIn: "root"
})

export class CartService {
  items = [];

  constructor() {
    let productsInLocalStorage = JSON.parse(localStorage.getItem('cart'));
    if (productsInLocalStorage) {
      let tempItems = [];
      productsInLocalStorage.forEach(function (value, index) {
        tempItems.push(value);
      });
      this.items = tempItems;
    }
  }

  addToCart(product) {
    this.items.push(product);
    localStorage.setItem('cart', JSON.stringify(this.items));
  }

  getItems() {
    return localStorage.getItem('cart');
  }

  clearCart() {
    this.items = [];
    localStorage.removeItem('cart');
    return this.items;
  }
}
