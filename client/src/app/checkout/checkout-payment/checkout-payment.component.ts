import {
  AfterViewInit,
  Component,
  ElementRef,
  Input,
  ViewChild,
} from '@angular/core';
import { FormGroup } from '@angular/forms';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasket } from 'src/app/shared/models/basket';
import { IOrderToCreate } from 'src/app/shared/models/order';
import { CheckoutService } from '../checkout.service';

declare var Stripe;

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss'],
})
export class CheckoutPaymentComponent implements AfterViewInit {
  @Input() checkoutFormInput: FormGroup;
  @ViewChild('cardNumber', { static: true }) cardNumberElement: ElementRef;
  @ViewChild('cardExpiry', { static: true }) cardExpiryElement: ElementRef;
  @ViewChild('cardCvc', { static: true }) cardCvcElement: ElementRef;
  stripe: any;
  cardNumber: any;
  cardExpiry: any;
  cardCvc: any;
  cardErrors: any;

  constructor(
    private checkoutService: CheckoutService,
    private basketService: BasketService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngAfterViewInit(): void {
    // get our publishable key in Stripe
    this.stripe = Stripe(
      'pk_test_51HY5wLLETbwHwiCXMCkkmcY5j9MT2OSVMnvtaBT4IUGbtBjwpVFRvwBtg0hY2SZKIN6PGZNK1chA5z6yHOnfjMgI002PeY1IQI'
    );
    const elements = this.stripe.elements();

    this.cardNumber = elements.create('cardNumber');
    this.cardNumber.mount(this.cardNumberElement.nativeElement);
    // this.cardNumber.addEventListener('change', this.cardHandler);

    this.cardExpiry = elements.create('cardExpiry');
    this.cardExpiry.mount(this.cardExpiryElement.nativeElement);
    // this.cardExpiry.addEventListener('change', this.cardHandler);

    this.cardCvc = elements.create('cardCvc');
    this.cardCvc.mount(this.cardCvcElement.nativeElement);
    // this.cardNumber.addEventListener('change', this.cardHandler);
  }

  ngOnDestroy() {
    this.cardNumber.destroy();
    this.cardExpiry.destroy();
    this.cardCvc.destroy();
  }

  // onChange({error}) {
  //   if (error) {
  //     this.cardErrors = error.message;
  //   } else {
  //     this.cardErrors = null;
  //   }
  // }	  

  submitOrder = () => {
    const basket = this.basketService.getCurrentBasketValue();
    const orderToCreate = this.getOrderToCreate(basket);
    this.checkoutService.createOrder(orderToCreate).subscribe(
      (order) => {
        this.toastr.success('Order submitted successfully');
        this.basketService.deleteLocalBasket(basket.id);
        const navigationExtras: NavigationExtras = { state: order };
        this.router.navigate(['checkout/success'], navigationExtras);
      },
      (error) => {
        this.toastr.error(error.message);
        console.log(error);
      }
    );
  };

  private getOrderToCreate(basket: IBasket): IOrderToCreate {
    return {
      basketId: basket.id,
      deliveryMethodId: +this.checkoutFormInput
        .get('deliveryForm')
        .get('deliveryMethod').value,
      shipToAddress: this.checkoutFormInput.get('addressForm').value,
    };
  }
}
