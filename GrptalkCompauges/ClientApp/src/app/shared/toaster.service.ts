import { ToastrManager } from 'ng6-toastr-notifications';
import { Injectable } from '@angular/core';


@Injectable()
export class toasterService {
  constructor(public toastr: ToastrManager) { }
  showSuccess(message) {
    this.toastr.successToastr(message, 'Success!', { showCloseButton : true});
  }
  showError() {
    this.toastr.errorToastr('Some Thing went wrong', 'Oops!', { showCloseButton: true });
  }
  showWarning(message) {
    this.toastr.warningToastr(message, 'Alert!', { showCloseButton: true });
  }

}
