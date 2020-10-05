import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BasketService } from 'src/app/basket/basket.service';

@Component({
  selector: 'app-checkout-review',
  templateUrl: './checkout-review.component.html',
  styleUrls: ['./checkout-review.component.scss'],
})
export class CheckoutReviewComponent implements OnInit {
  constructor(
    private baskeService: BasketService,
    private toastrService: ToastrService
  ) {}

  ngOnInit(): void {}

  createPaymentIntent = () =>
    this.baskeService.createPaymentIntent().subscribe(
      (pIntent) => this.toastrService.success('Payment intent created'),
      (err) => {
        console.log(err);
        this.toastrService.error(err.message);
      }
    );
}
