import { NgModule } from '@angular/core';
import { Routes, Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { CheckoutComponent } from './checkout.component';

const routes: Routes = [{ path: '', component: CheckoutComponent }];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class CheckoutRoutingModule {}
