import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { IDelivery } from '../shared/models/delivery';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class CheckoutService {
  baseUrl = environment.apiUrl;

  constructor (private httpClient: HttpClient) { }

  // gets delivery methods sorted by price, highest price first
  getDeliveryMethods = (): Observable<IDelivery[]> =>
    this.httpClient
      .get(this.baseUrl + 'orders/deliveryMethods')
      .pipe(map((dm: IDelivery[]) => dm.sort((a, b) => a.price - b.price)));
}
