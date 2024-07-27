import { Injectable } from '@angular/core';
import { LocalStorage } from '../constants/local-storage.constant';
import { LocalStorageService } from './local-storage.service';
import { JwtPayload, jwtDecode } from "jwt-decode";


@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(
    private localStorageService: LocalStorageService) {

  }

  public isAuthenticated(): boolean {
    const token = this.localStorageService.getData(LocalStorage.TOKEN);
    if (token == null || token == "") {
      return false;
    }
    let claims = this.getDecodedAccessToken(token);
    if (claims == null) {
      return false;
    }
    return !this.tokenExpired(claims);
  }

  getDecodedAccessToken(token: string): any {
    try {
      return jwtDecode<JwtPayload>(token);
    } catch (Error) {
      return null;
    }
  }

  private tokenExpired(claims: any) {
    let expiry = claims?.exp;
    if (expiry) {
      return (Math.floor((new Date).getTime() / 1000)) >= expiry;
    } else {
      return false;
    }
  }


  public getCurrentUser(): any {
    const token = this.localStorageService.getData(LocalStorage.TOKEN);
    if (token === null) {
      return undefined;
    }
    const decodedToken = this.getDecodedAccessToken(token);
    return decodedToken;
  }

}
