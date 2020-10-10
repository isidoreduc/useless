import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

import { BasketService } from 'src/app/basket/basket.service';
import { IBasketItem } from '../../models/basket';
import { IOrderItem } from './../../models/order';

@Component({
  selector: 'app-basket-summary',
  templateUrl: './basket-summary.component.html',
  styleUrls: ['./basket-summary.component.scss'],
})
export class BasketSummaryComponent implements OnInit {
  @Output() decrementItemQuant: EventEmitter<IBasketItem> = new EventEmitter<IBasketItem>();
  @Output() incrementItemQuant: EventEmitter<IBasketItem> = new EventEmitter<IBasketItem>();
  @Output() removeItem: EventEmitter<IBasketItem> = new EventEmitter<IBasketItem>();
  @Input() isBasket = true;
  @Input() items: IBasketItem[] | IOrderItem[] = [];
  @Input() isOrder = false;

  constructor() { }

  ngOnInit(): void { }

  removeBasketItem(item: IBasketItem): void {
    this.removeItem.emit(item);
  }

  incrementItemQuantity(item: IBasketItem): void {
    this.incrementItemQuant.emit(item);
  }

  decrementItemQuantity(item: IBasketItem): void {
    this.decrementItemQuant.emit(item);
  }
}
