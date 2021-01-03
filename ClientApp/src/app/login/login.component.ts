import { Component, OnInit, Inject } from '@angular/core';
import { CartService } from '../cart.service';
import { FormBuilder } from '@angular/forms';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {
  loginForm;
  authorized = null;

  constructor(
    private http: HttpClient,
    private formBuilder: FormBuilder,
    @Inject('BASE_URL') private baseUrl: string
  ) {
    this.loginForm = this.formBuilder.group({
      email: '',
      password: ''
    });
  }

  ngOnInit() {}

  onSubmit(accountData) {
    let user = {
      email: accountData.email,
      password: accountData.password
    };

     this.http.post<string>(this.baseUrl + 'api/login/', user)
      .subscribe(result => {

        if (result['message'] == "Unauthorized!") {
          this.authorized = 0;
        } else {
          localStorage.setItem('token', result["token"]);

          window.location.replace('/');

          this.loginForm.reset();

          alert('Login successful');
        }

      });
  }

}

