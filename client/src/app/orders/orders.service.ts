import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { map } from 'rxjs/operators';
import { IOrder } from '../shared/models/order';

@Injectable({ providedIn: 'root' })
export class OrdersService {
  baseUrl = environment.apiUrl;
  constructor(private httpClient: HttpClient) {}

  getOrders = () =>
    this.httpClient
      .get(this.baseUrl + 'orders')
      .pipe(map((orders: IOrder[]) => orders.sort((a, b) => a.id - b.id)));
}
