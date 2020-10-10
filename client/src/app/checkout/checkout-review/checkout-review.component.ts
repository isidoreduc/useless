import { Component, Input, OnInit } from '@angular/core';

import { BasketService } from 'src/app/basket/basket.service';
import { CdkStepper } from '@angular/cdk/stepper';
import { IBasket } from './../../shared/models/basket';
import { Observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-checkout-review',
  templateUrl: './checkout-review.component.html',
  styleUrls: ['./checkout-review.component.scss'],
})
export class CheckoutReviewComponent implements OnInit {
  basket$: Observable<IBasket>;
  @Input() stepperInput: CdkStepper;

  constructor(
    private baskeService: BasketService,
    private toastrService: ToastrService
  ) { }

  ngOnInit(): void { this.basket$ = this.baskeService.basket$; }

  createPaymentIntent = () =>
    this.baskeService.createPaymentIntent().subscribe(
      (pIntent) => {
        this.toastrService.success('Payment intent created');
        this.stepperInput.next();
      },
      (err) => {
        console.log(err);
        this.toastrService.error(err.message);
      }
    );
}
