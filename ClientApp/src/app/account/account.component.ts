import { Component } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html'
})
export class AccountComponent {
  public user: User[];
  public orders;

  constructor(
    private route: ActivatedRoute, http: HttpClient
  ) {
  
    let users;
    let userId;
    this.route.paramMap.subscribe(params => {
      userId = params.get('userId');
    }, error => console.error(error));

    http.get('http://localhost:61442/api/order/byuser/' + userId)
      .subscribe(result => {
        this.orders = result
      });

    http.get<User[]>('http://localhost:61442/api/user/')
      .subscribe(result => {
        var that = this;
        users = result;
        users.forEach(function (item, value) {
          if (item.myToken == userId) {
            that.user = item;
          }
        })
      });

    }
}

interface User {
  email: string;
  firstName: string;
  lastName: string;
}
