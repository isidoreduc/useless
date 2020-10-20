import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest
} from '@angular/common/http';
import { delay, finalize } from 'rxjs/operators';

import { BusyService } from '../busy.service';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {
  constructor(private busyService: BusyService) { }
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    if (req.method === 'POST' && req.url.includes('orders')) {
      return next.handle(req);
    }

    if (req.url.includes('emailexists')) {
      return next.handle(req);
    }
    this.busyService.busy();
    return next.handle(req).pipe(
      delay(1000), // FOR TESTING
      finalize(() => this.busyService.idle())
    );
  }
}
