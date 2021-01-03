import { Component } from '@angular/core';
import { HttpClient } from "@angular/common/http";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  products;

  constructor(
    private http: HttpClient
  ) { }

  ngOnInit() {
    this.products = this.http.get('http://localhost:61442/api/product');
  }
}
