import { Component, OnInit, Inject } from '@angular/core';
import { CartService } from '../cart.service';
import { FormBuilder } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
  

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent implements OnInit {
  registerForm; 

  constructor(
    private http: HttpClient,
    private formBuilder: FormBuilder,
    @Inject('BASE_URL') private baseUrl: string
  ) {
    this.registerForm = this.formBuilder.group({
      email: '',
      password: '',
      firstName: '',
      lastName: ''
    });
  }

  ngOnInit() {}

  onSubmit(accountData) {
    let user = {
      email: accountData.email,
      password: accountData.password,
      firstName: accountData.firstName,
      lastName: accountData.lastName,
    };

    // register the new account
    this.http.post<Account>(this.baseUrl + 'api/user/', user)
      .subscribe(result => {
        
      });

    this.registerForm.reset();
    alert('Your account has been created');
  }

}

