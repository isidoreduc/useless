import { Injectable } from '@angular/core';
import { environment } from '../../../src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { IBasket } from '../shared/models/basket';

@Injectable({
  providedIn: 'root',
})
export class BasketService {
  baseUrl = environment.apiUrl;
  private basketSource = new BehaviorSubject<IBasket>(null);
  basket$ = this.basketSource.asObservable();

  constructor(private http: HttpClient) {}

  // get basket and set the observable value that was initialized to null(basketSource)
  getBasket(id: string): Observable<void> {
    return this.http.get(this.baseUrl + 'basket?id=' + id).pipe(
      map((basket: IBasket) => {
        this.basketSource.next(basket);
      })
    );
  }

  setBasket(basket: IBasket): void {
    this.http
      .post(this.baseUrl + 'basket', basket)
      .subscribe((response: IBasket) => {
        this.basketSource.next(response);
      });
  }

  getBasketValue(): IBasket {
    return this.basketSource.value;
  }
}
