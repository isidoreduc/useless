import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Observable } from 'rxjs';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasket, IBasketItem } from '../../models/basket';

@Component({
  selector: 'app-basket-summary',
  templateUrl: './basket-summary.component.html',
  styleUrls: ['./basket-summary.component.scss'],
})
export class BasketSummaryComponent implements OnInit {
  basket$: Observable<IBasket>;
  @Output() decrementItemQuant: EventEmitter<IBasketItem> = new EventEmitter<IBasketItem>();
  @Output() incrementItemQuant: EventEmitter<IBasketItem> = new EventEmitter<IBasketItem>();
  @Output() removeItem: EventEmitter<IBasketItem> = new EventEmitter<IBasketItem>();
  @Input() isBasket = true;
 
  constructor(private basketService: BasketService) {}

  ngOnInit(): void {
    this.basket$ = this.basketService.basket$;
  }

  removeBasketItem(item: IBasketItem) {
    this.removeItem.emit(item);
  }

  incrementItemQuantity(item: IBasketItem) {
    this.incrementItemQuant.emit(item);
  }

  decrementItemQuantity(item: IBasketItem) {
    this.decrementItemQuant.emit(item);
  }
}
