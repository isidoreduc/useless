import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { BasketService } from 'src/app/basket/basket.service';
import { IDelivery } from 'src/app/shared/models/delivery';
import { CheckoutService } from '../checkout.service';

@Component({
  selector: 'app-checkout-delivery',
  templateUrl: './checkout-delivery.component.html',
  styleUrls: ['./checkout-delivery.component.scss'],
})
export class CheckoutDeliveryComponent implements OnInit {
  @Input() checkoutFormInput: FormGroup;
  deliveryMethods: IDelivery[];

  constructor(
    private checkoutService: CheckoutService,
    private basketService: BasketService
  ) {}

  ngOnInit(): void {
    this.checkoutService.getDeliveryMethods().subscribe(
      (dm: IDelivery[]) => (this.deliveryMethods = dm),
      (error) => console.log(error)
    );
  }

  setShippingPrice = (delivery: IDelivery) =>
    this.basketService.setShippingPrice(delivery);
}
