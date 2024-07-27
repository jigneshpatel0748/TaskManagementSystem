import {
  HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, of, throwError } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { LocalStorageService } from '../services/local-storage.service';
import { LocalStorage } from '../constants/local-storage.constant';
import { ToasterService } from '../services/toaster.service';

@Injectable()
export class AppInterceptor implements HttpInterceptor {

  constructor(private localStorageService: LocalStorageService,
    private route: Router,
    private _toasterService: ToasterService
  ) {

  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    let isToken = this.localStorageService.getData(LocalStorage.TOKEN) == null || this.localStorageService.getData(LocalStorage.TOKEN) == "" ? false : true;

    if (isToken) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${this.localStorageService.getData(LocalStorage.TOKEN)}`
        }
      });
    }

    return next.handle(request).pipe(
      map(result=>{
        return result;
      }),
      catchError((err: any) => {
        var messageDescription = "";
        if (err?.error) {
          for (var item in err?.error) {
            messageDescription += messageDescription == "" ? `${err?.error[item].propertyName}: ${err?.error[item].errorMessage}` : `<br/> ${err?.error[item].propertyName}: ${err?.error[item].errorMessage}`;
          }
          this._toasterService.toastError("Error",messageDescription);
        } else {
          this._toasterService.toastError("Error", err.error);
        }
        throw messageDescription;
      })
    );

  }

}
