import { CdkStepper } from '@angular/cdk/stepper';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-stepper',
  templateUrl: './stepper.component.html',
  styleUrls: ['./stepper.component.scss'],
  providers: [{ provide: CdkStepper, useExisting: StepperComponent }],
})
export class StepperComponent extends CdkStepper implements OnInit {
  // flag received from client, whether or not we want the stepper linear mode on/off
  @Input() linearModeSelected: boolean;
  ngOnInit(): void {
    // extending CdkStepper, we have access to its properties
    this.linear = this.linearModeSelected;
  }

  onClick = (index: number) => {
    this.selectedIndex = index;
    console.log(this.selectedIndex);
  }
}
