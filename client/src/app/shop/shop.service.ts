import { HttpClient, HttpParams } from '@angular/common/http';
import { IPagination, Pagination } from './../shared/models/pagination';
import { Observable, of } from 'rxjs';

import { IBrand } from '../shared/models/brand';
import { IProduct } from '../shared/models/product';
import { IType } from '../shared/models/type';
import { Injectable } from '@angular/core';
import { ShopParams } from '../shared/models/shopParams';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = environment.apiUrl;
  products: IProduct[] = [];
  brands: IBrand[] = [];
  types: IType[] = [];
  pagination = new Pagination();
  shopParams = new ShopParams();

  constructor(private http: HttpClient) { }

  getProducts(useCache: boolean): Observable<IPagination> {
    // cache is false, get data from server
    if (useCache === false) {
      this.products = [];
    }

    // if products present and cache true, use the cache
    if (this.products.length > 0 && useCache === true) {
      const pagesReceived = Math.ceil(this.products.length / this.shopParams.pageSize);
      // show from first item on that page to last (slice (first, last))
      if (this.shopParams.pageNumber <= pagesReceived) {
        this.pagination.data =
          this.products.slice((this.shopParams.pageNumber - 1) * this.shopParams.pageSize,
            this.shopParams.pageNumber * this.shopParams.pageSize);
        return of(this.pagination);
      }
    }
    
    let params = new HttpParams();

    if (this.shopParams.brandId !== 0) {
      params = params.append('brandId', this.shopParams.brandId.toString());
    }
    if (this.shopParams.typeId !== 0) {
      params = params.append('typeId', this.shopParams.typeId.toString());
    }
    if (this.shopParams.search) {
      params = params.append('search', this.shopParams.search);
    }
    params = params.append('sort', this.shopParams.sort);
    params = params.append('pageIndex', this.shopParams.pageNumber.toString());
    params = params.append('pageSize', this.shopParams.pageSize.toString());

    return this.http.get<IPagination>(this.baseUrl + 'products', { observe: 'response', params, })
      .pipe(map((response) => {
        this.products = [...this.products, ...response.body.data];
        this.pagination = response.body;
        return this.pagination;
      })
      );
  }

  setShopParams = (params: ShopParams) => this.shopParams = params;
  getShopParams = (): ShopParams => this.shopParams;

  getProduct(id: number): Observable<IProduct> {
    const product = this.products.find(p => p.id === id);
    if (product) return of(product); // <of> operator returns an observable of the type of parameter - Observable<IProduct>
    return this.http.get<IProduct>(this.baseUrl + 'products/' + id);
  }

  getTypes(): Observable<IType[]> {
    if (this.types.length > 0) return of(this.types);
    return this.http.get<IType[]>(this.baseUrl + 'products/types')
      .pipe(map(response => this.types = response));
  }

  getBrands(): Observable<IBrand[]> {
    if (this.brands.length > 0) return of(this.brands);
    return this.http.get<IBrand[]>(this.baseUrl + 'products/brands')
      .pipe(map(response => this.brands = response));
  }
}
