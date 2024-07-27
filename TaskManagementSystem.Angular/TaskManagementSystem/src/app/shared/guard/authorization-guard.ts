import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import { AuthenticationService } from '../services/authentication.service';

@Injectable()
export class AuthGuard implements CanActivate {

  private _unsubscribeAll: Subject<any> = new Subject<any>();
  navigation: any = [];

  constructor(public _router: Router,
    private _authenticationService: AuthenticationService
  ) { }

  async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<boolean> {
    return this._check(state.url, route.data);
  }

  canActivateChild(childRoute: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return this._check(state.url, childRoute.data);
  }

  private async _check(redirectURL: string, data: any) {
    let isValisUser = this._authenticationService.isAuthenticated();
    if (!isValisUser) {
      this._router.navigate(['/sign-in']);
      return false;
    }
    else {
      let user = this._authenticationService.getCurrentUser();
      if (data.role && data.role.indexOf(user.Role) === -1) {
        this._router.navigate(['/home']);
        return false;
      }
      return true;
    }

    return true;
  }
}

