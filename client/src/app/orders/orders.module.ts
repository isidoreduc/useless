import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { OrdersRoutingModule } from './orders-routing.module';
import { OrdersComponent } from './orders.component';
import { OrderDetailsComponent } from './order-details/order-details.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [OrdersComponent, OrderDetailsComponent],
  imports: [OrdersRoutingModule, CommonModule, SharedModule],
})
export class OrdersModule {}
