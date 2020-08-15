import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IPagination } from './../shared/models/pagination';
import { Observable } from 'rxjs';
import { IType } from '../shared/models/type';
import { IBrand } from '../shared/models/brand';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/';

  constructor(private http: HttpClient) {}

  getProducts(): Observable<IPagination> {
    return this.http.get<IPagination>(this.baseUrl + 'products');
  }

  getTypes(): Observable<IType[]>{
    return this.http.get<IType[]>(this.baseUrl + 'products/types');
  }

  getBrands(): Observable<IBrand[]>{
    return this.http.get<IBrand[]>(this.baseUrl + 'products/brands');
  }
}
