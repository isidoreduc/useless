import { NgModule } from '@angular/core';
import { OrderDetailsComponent } from './order-details/order-details.component';
import { OrdersComponent } from './orders.component';
import { RouterModule } from '@angular/router';

const routes = [
  { path: '', component: OrdersComponent },
  {
    path: ':id',
    component: OrderDetailsComponent,
    data: { breadcrumb: { alias: 'orderDetails' } },
  },
];

@NgModule({
  declarations: [],
  imports: [ RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class OrdersRoutingModule {}
