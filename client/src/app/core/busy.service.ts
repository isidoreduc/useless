import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root',
})
export class BusyService {
  busyReqCount = 0;
  constructor(private spinnerService: NgxSpinnerService) { }

  busy(): void {
    this.busyReqCount++;
    this.spinnerService.show(undefined,
      {
        type: 'ball-fussion',
        bdColor: 'rgba(255,255,255,0.7)',
        color: '#333333',
      }
    );
  }

  idle(): void {
    this.busyReqCount--;
    if (this.busyReqCount <= 0) {
      this.busyReqCount = 0;
    }
    this.spinnerService.hide();
  }
}
