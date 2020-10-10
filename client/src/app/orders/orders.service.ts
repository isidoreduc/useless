import { HttpClient } from '@angular/common/http';
import { IOrder } from '../shared/models/order';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class OrdersService {
  baseUrl = environment.apiUrl;
  constructor(private httpClient: HttpClient) {}

  getOrders = () => this.httpClient.get(this.baseUrl + 'orders');

  getOrder = (id: number) =>
    this.httpClient.get<IOrder>(this.baseUrl + 'orders/' + id)
}
