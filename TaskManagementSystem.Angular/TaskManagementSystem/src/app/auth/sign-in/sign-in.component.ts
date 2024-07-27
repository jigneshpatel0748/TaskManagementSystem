import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LocalStorage } from 'src/app/shared/constants/local-storage.constant';
import { LoginRequest } from 'src/app/shared/models/user/loginRequest';
import { ApiHttpService } from 'src/app/shared/services/api-http.service';
import { LocalStorageService } from 'src/app/shared/services/local-storage.service';
import { ToasterService } from 'src/app/shared/services/toaster.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss']
})
export class SignInComponent implements OnInit {
  userLogin: LoginRequest = <LoginRequest>{};
  constructor(
    private _apiService: ApiHttpService,
    private _localStorageService: LocalStorageService,
    private _router: Router,
    private toastr: ToasterService
  ) { }

  ngOnInit(): void {
  }

  login() {
    let payload: LoginRequest = {
      email: this.userLogin.email,
      password: this.userLogin.password
    }
    this._apiService.post(`user/Login`, payload)
      .subscribe((res: any) => {
        if (res.isSuccess) {
          this._localStorageService.setData(LocalStorage.TOKEN, res.data.token);
          this._router.navigate([`home`]);
        } else {
          this.toastr.toastError("Error",res.message);
        }
      });
  }
}
