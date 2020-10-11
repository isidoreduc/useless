import { Component, Input, OnInit } from '@angular/core';

import { AccountService } from 'src/app/account/account.service';
import { FormGroup } from '@angular/forms';
import { IAddress } from 'src/app/shared/models/address';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-checkout-address',
  templateUrl: './checkout-address.component.html',
  styleUrls: ['./checkout-address.component.scss']
})
export class CheckoutAddressComponent implements OnInit {
  @Input() checkoutFormInput: FormGroup;

  constructor(private accountService: AccountService, private toastr: ToastrService) { }

  ngOnInit() {
  }

  saveUserAddress() {
    this.accountService.updateUserAddress(this.checkoutFormInput.get('addressForm').value)
      .subscribe((address: IAddress) => {
        this.toastr.success('Address saved');
        this.checkoutFormInput.get('addressForm').reset(address);
      }, error => {
        this.toastr.error(error.message);
        console.log(error);
      });
  }

}
