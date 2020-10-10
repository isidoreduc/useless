import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { AccountService } from 'src/app/account/account.service';

@Component({
  selector: 'app-checkout-address',
  templateUrl: './checkout-address.component.html',
  styleUrls: ['./checkout-address.component.scss'],
})
export class CheckoutAddressComponent implements OnInit {
  @Input() checkoutFormInput: FormGroup;
  constructor(private accountService: AccountService) {}

  ngOnInit(): void {}

  saveFormUserAddress = () => {
    this.accountService
      .updateUserAddress(this.checkoutFormInput.get('addressForm').value)
      .subscribe(
        () => console.log('Success'),
        (error) => console.log(error)
      );
  }
}
