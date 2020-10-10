import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import {
  HttpInterceptor,
  HttpEvent,
  HttpHandler,
  HttpRequest,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private router: Router, private toastr: ToastrService) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((err) => {
        if (err) {
          if (err.status === 400) {
            this.toastr.error(err.error, err.error.statusCode);
          }
          if (err.status === 401) {
            this.toastr.error(err.error, err.error.statusCode);
          }
          if (err.status === 404) { this.router.navigateByUrl('/not-found'); }
          if (err.status === 500) { this.router.navigateByUrl('/server-error'); }
        }
        return throwError(err);
      })
    );
  }
}
