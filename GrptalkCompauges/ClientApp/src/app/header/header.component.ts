import { Component } from '@angular/core';
//import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { ViewChild, ElementRef } from '@angular/core';

//import { ImageCroppedEvent } from './image-cropper/interfaces/image-cropped-event.interface';
//import { ImageCropperComponent } from './image-cropper/component/image-cropper.component';
import { ImageCroppedEvent } from 'ngx-image-cropper';
import { UserService } from '../shared/user.service';
import { NgbModal, ModalDismissReasons, NgbModalRef, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpErrorResponse } from '@angular/common/http';
import { toasterService } from '../shared/toaster.service';
import { Router } from '@angular/router';



@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.css']
})
/** header component*/
export class HeaderComponent implements OnInit {
    /** header ctor */
  public modalRef: NgbModalRef;
  ImageName: string;
  closeResult: string;
  imageChangedEvent: any = '';
  croppedImage: any = '';
  imageCropEvent: any;
  UserRole: string;
  isDropDownShow = false;
  private base64textString: String = "";
  @ViewChild('firstNameInput') nameInputRef: ElementRef;
  
  constructor(private modalService: NgbModal, private userService: UserService, public toastr: toasterService, private route : Router) {

  }

  ngOnInit() {
    this.UserRole = localStorage.getItem("UserRole");
  }

  click()
  {
    alert("clicked");
  }


  open(imageUpload) {

    this.modalRef = this.modalService.open(imageUpload);
    this.modalRef.result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    });
  }

  fileChangeEvent(event: any): void {
    this.imageChangedEvent = event;
    console.log(event.target.files.length);
    if (event.target.files.length >= 1)
    {
      this.ImageName = this.imageChangedEvent.target.files[0].name;
    }
    
  }
  imageCropped(event: ImageCroppedEvent) {
    //console.log(event);
    this.imageCropEvent = event;
    this.croppedImage = event.base64;
  }
  imageLoaded() {
    // show cropper
  }
  cropperReady() {
    // cropper ready
  }
  loadImageFailed() {
    // show message
  }
  uploadImage()
  {
    
    this.userService.uploadProfilePic(this.croppedImage, this.ImageName , "fileSave").subscribe((data: any) => {
      console.log(data);
      
      this.modalRef.close();
      this.toastr.showSuccess("Image Uploaded Successfuly");
    },
      (err: HttpErrorResponse) => {
        this.toastr.showError();
      });
  }
  ShowDropDown()
  {
    if (this.isDropDownShow == true) {
      this.isDropDownShow = false;
    }
    else
    {
      this.isDropDownShow = true;
    }

  }


  logOut()
  {
    localStorage.clear();
    this.toastr.showSuccess("Log out was Successfull");
    this.route.navigate(["/login"]);
    

  }

  //handleFileSelect(evt) {
  //  var files = evt.target.files;
  //  var file = files[0];

  //  if (files && file) {
  //    var reader = new FileReader();

  //    reader.onload = this._handleReaderLoaded.bind(this);

  //    reader.readAsBinaryString(file);
  //  }
  //}

  //_handleReaderLoaded(readerEvt) {
  //  var binaryString = readerEvt.target.result;
  //  this.base64textString = btoa(binaryString);
  //  console.log(btoa(binaryString));
  //}
}
