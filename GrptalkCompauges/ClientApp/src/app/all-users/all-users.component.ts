import { Component, OnInit, ViewChild, ViewContainerRef , ElementRef } from '@angular/core';
import { User } from '../shared/user.model';
import { UserService } from '../shared/user.service';

import { NgbModal, ModalDismissReasons, NgbModalRef} from '@ng-bootstrap/ng-bootstrap';
import { HttpErrorResponse } from '@angular/common/http';
import { DatePipe } from '@angular/common';
import { toasterService } from '../shared/toaster.service';
import { EncryDecry } from '../shared/EncryDecry.service';

@Component({
    selector: 'app-all-users',
    templateUrl: './all-users.component.html',
    styleUrls: ['./all-users.component.css']
})
/** AllUsers component*/
export class AllUsersComponent implements OnInit 
{
    /** AllUsers ctor */
  public isCollapsed = true;
  public userModal: any ;
  public userObj: any;
  closeResult: string;
  firstname: string;
  users: any;
  selectedIndex: any;
  actionindex: any;
  public modalButton: string = "Add";
  public modalUserHeader: string = "Add New User";
  public modalDeleteHeader: string = "Delete User";
  public SfromDate = "";
  public StoDate = "";
  public customFDate = "";
  public customTDate = "";
  public isFiltersShow = false;
  public modalRef: NgbModalRef;
  public userSearchInput = "";
  public selectedStage = "";
  //public autoCompleteClasses = "ng-autocomplete-dropdown";
  //public countries = [{ id: 1, name: 'Albania', }, { id: 2, name: 'Belgium', }, { id: 3, name: 'Denmark', }, { id: 4, name: 'Montenegro', }, { id: 5, name: 'Turkey', }, { id: 6, name: 'Ukraine', }, { id: 7, name: 'Macedonia', }, { id: 8, name: 'Slovenia', }, { id: 9, name: 'Georgia', }, { id: 10, name: 'India', }, { id: 11, name: 'Russia', }, { id: 12, name: 'Switzerland', }];
  
  results: Object;
  fromDate: {
    "year": 2018,
    "month": 12,
    "day": 14
  }
  toDate: {
    "year": 2018,
    "month": 12,
    "day": 14
  }
  //user: { firstName: string, secondName: string, email: string, mobilenumber: string, password: string, Confirmpassword : string    };
  AddUser = { firstName: "", secondName: "", email: "", mobilenumber: "", password: "", Confirmpassword: "", isChecked : "", cipherKey : "" };
  @ViewChild('adduser') private adduser;
  @ViewChild('deleteUser') private deleteUser;
  dateCBoxes = { LDayChecked: false, LWeekChecked: false, LMonthChecked: false, CustomDate: false };
  dateCDays = { LDayCheckedFDate: 1, LDayCheckedTDate: 1, LWeekCheckedFDate: 7, LWeekCheckedTDate: 0, LMonthCheckedFDate: 31, LMonthCheckedTDate: 0 };

  constructor(private modalService: NgbModal, private userService: UserService, private datePipe: DatePipe, public toastr: toasterService,  private encryDecry: EncryDecry  ) {
    
    
  }

  //open(adduser) {
  //  this.modalService.open(adduser);
  //}

  ngOnInit()
  {
    this.getAllUsers("", "", "");
    
  }
  selectAll() {


  }

  getAllUsers(searchData , fromDate, toDate )
  {
    console.log(fromDate);
    console.log(toDate);
    let searchString = "userSearch=" + searchData + "&fromDate=" + fromDate + "&toDate=" + toDate;
    this.userService.getAllUsers(searchString, "GetAllCompaugeUsers_Result").subscribe((data: any) => {
      this.users = data;
      console.log(this.users);
    },
    (err: HttpErrorResponse) => {
      console.error(err);
      this.toastr.showError();
    });

  }

  open(adduser) {
    this.modalUserHeader = "Add New User";
    this.modalButton = "Add";
    for ( var key in this.AddUser  )
    {
      this.AddUser[key] = "";
    }
    this.modalRef = this.modalService.open(adduser);
    this.modalRef.result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  } 

  openModal(modalViewChild) {
    this.modalRef = this.modalService.open(modalViewChild);
    this.modalRef.result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }

  AddOrEditValidation()
  {
    var isPass = 1;
    for (var key in this.AddUser) {
      // alert(  key  +"    " + this.AddUser[key]);

      if (this.AddUser[key] == "" && this.AddUser[key] == "isChecked") {
        this.toastr.showWarning("please fill " + key);
        isPass = 0;
      }
    }

    if (this.AddUser.password != this.AddUser.Confirmpassword) {
      this.toastr.showWarning("please make sure that password and confirm password ssame");
      isPass = 0;
    }
    var IsEmailValidated;
    IsEmailValidated = this.isEmail(this.AddUser.email);
    if (IsEmailValidated == false)
    {
      this.toastr.showWarning("Please Enter Valid Email Address");
      isPass = 0;
    }
    

    return isPass;
  }

  AddUserM()
  {
    console.log("Add User");
    var isvalidated = 1;

    isvalidated = this.AddOrEditValidation();
    
    if (isvalidated == 0) {
      return;
    }
    
    this.modalRef.close();
    var encrypted = this.encryDecry.set(this.AddUser["mobilenumber"] ,this.AddUser["password"]);
    var encrysha = this.encryDecry.sha3Encryption(this.AddUser["password"]);
    this.AddUser["password"] = encrysha;
    this.AddUser["cipherKey"] = encrypted;
    this.userService.addUser(this.AddUser, "ManageCompaugeUser").subscribe((data: any) => {
      this.getAllUsers("", "", "");
      this.toastr.showSuccess("user Added successfully");
    },
    (err: HttpErrorResponse) => {

      console.error(err);
      this.toastr.showError();
    });


    for (var key in this.AddUser) {
      // alert(  key  +"    " + this.AddUser[key]);
      this.AddUser[key] = "";
    }
  }

  selectedRow(id , isChecked)
  {
    console.log(id);
    console.log(isChecked);
    if (isChecked == false) {
      
      console.log(this.users.length);
      for (var i = 0; i < this.users.length; i++)
      {
        
        if (this.users[i].isChecked == true)
        {
          this.users[i].isChecked = false;
          console.log(this.users[i].id);
        }
      }
    }
    
  }

  toogleFilter()
  {
    console.log("before switch" + this.isFiltersShow);
    if (this.isFiltersShow == true) {
      this.isFiltersShow = false;
    }
    else
    {
      this.isFiltersShow = true;
    }
    console.log("after switch" + this.isFiltersShow);
  }
  editConfirm() {

    console.log("Edit User");
    var isvalidated = 1;

    isvalidated = this.AddOrEditValidation();

    if (isvalidated == 0) {
      return;
    }
    var selectedUser = this.users.filter(e => e.isChecked == true);
    console.log(selectedUser);
    this.modalRef.close();

    var encrypted = this.encryDecry.set(this.AddUser["mobilenumber"], this.AddUser["password"]);
    var encrysha = this.encryDecry.sha3Encryption(this.AddUser["password"]);
    

    var decrypted = this.encryDecry.get(this.AddUser["mobilenumber"], encrypted);

    
    this.AddUser["password"] = encrysha;
    this.AddUser["cipherKey"] = encrypted;


    this.userService.editUser(this.AddUser, "ManageCompaugeUser",selectedUser[0]["id"]).subscribe((data: any) => {
      this.toastr.showSuccess("Data Edited Successfully");
      this.getAllUsers("", "", "");
    },
      (err: HttpErrorResponse) => {

        console.error(err);
        this.toastr.showError();
      });

    for (var key in this.AddUser) {
      // alert(  key  +"    " + this.AddUser[key]);
      this.AddUser[key] = "";
    }
  }



  edit(user)
  {
    //var selectedUser = this.users.filter(e => e.isChecked == true);
    console.log(this.users);
    console.log("edit");
    console.log(user);
    if (user.isChecked == false)
    {
      this.toastr.showWarning("not equal");
      return;
    }
    //createdBy: 1
    //createdTime: "2018-11-25T16:11:10.3Z"
    //email: "surya@gmail.com"
    //id: 1
    //isActive: 1
    //mobile: "919493989108"
    //name: "surya"
    //password: "surya"
    //roleId: 1
    //updatedTime: "2018-11-25T16:11:10.3Z"
    //userName: "surya"
    //user: { firstName: string, secondName: string, email: string, mobilenumber: string, password: string, Confirmpassword : string    };
    this.AddUser.firstName = user.name;
    this.AddUser.secondName = user.name;
    this.AddUser.email = user.email;
    this.AddUser.mobilenumber = user.mobile;
    //this.AddUser.password = this.encryDecry.get(user.password, user.name );
    //this.AddUser.Confirmpassword = this.encryDecry.get(user.password, user.name);

    var decrypted = this.encryDecry.get(user["mobile"] ,user["password"] );
    var encrysha = this.encryDecry.sha3Encryption(this.AddUser["password"]);

    this.AddUser["password"] = decrypted;
    this.AddUser["cipherKey"] = encrysha;
    this.AddUser["Confirmpassword"] = decrypted;
    this.modalButton = "Edit";
    this.modalUserHeader = "Edit Details for " + this.AddUser.firstName;
      //= { firstName: "", secondName: "", email: "", mobilenumber: "", password: "", Confirmpassword: "" };
    this.openModal(this.adduser);
  }

  selectedDate(key  , isChecked)
  {
    
    if (isChecked == false) {
      
      for (var Jsonkey in this.dateCBoxes) {
        if (this.dateCBoxes[Jsonkey] == true) {
          this.dateCBoxes[Jsonkey] = false;
        }
      }

      if (key != 'CustomDate') {
        let Selectfrom = new Date();
        let Selectto = new Date();
        let fromcount = this.dateCDays[key + "FDate"];

        let tocount = this.dateCDays[key + "TDate"];


        Selectto.setDate(Selectto.getDate() - tocount);
        Selectfrom.setDate(Selectfrom.getDate() - fromcount);

        this.SfromDate = this.datePipe.transform(Selectfrom, 'yyyy-MM-dd');
        this.StoDate = this.datePipe.transform(Selectto, 'yyyy-MM-dd');
        
        this.getAllUsers(this.userSearchInput, this.SfromDate, this.StoDate);
      }
      else {
        console.log(this.fromDate);
        console.log(this.toDate);
        this.dateCBoxes["CustomDate"] = true;
        this.customDateSearch("" , "");
      }

    }
    else
    {
      this.SfromDate = "";
      this.StoDate = "";
      this.getAllUsers(this.userSearchInput, this.SfromDate, this.StoDate);
     
    }

    
  }
  customDateSearch(customDate , customDateKey )
  {
    console.log(customDate);
    console.log(customDateKey);
    console.log(this.dateCBoxes);

    if (customDateKey == "fromDate") {
      this.customFDate = customDate.year.toString() + "-" + customDate.month.toString() + "-" + customDate.day.toString();
    }
    else if (customDateKey == "toDate") {
      this.customTDate = customDate.year.toString() + "-" + customDate.month.toString() + "-" + customDate.day.toString();
    }

    if (this.dateCBoxes["CustomDate"] == true) {
      
      this.SfromDate = this.customFDate;
      this.StoDate = this.customTDate;
      if (this.customFDate != "" && this.customTDate != "") {
        this.getAllUsers(this.userSearchInput, this.customFDate, this.customTDate);
      }
      
    }
    else
    {
      this.toastr.showWarning("please select custom date filter");
      this.SfromDate = "";
      this.StoDate = "";
      return;
    }
  }

  delete(user)
  {
    if (user.isChecked == false) {
      this.toastr.showWarning("not equal");
      return;
    }
    else
    {
      this.modalDeleteHeader = "Delete User";
      this.modalDeleteHeader = this.modalDeleteHeader + " " + user["name"].toString() ;
      this.openModal(this.deleteUser);
    }
  }

  deleteConfirm()
  {
    var selectedUser = this.users.filter(e => e.isChecked == true);
    console.log(selectedUser);
    this.modalRef.close();
    this.userService.deleteUser( "ManageCompaugeUser", selectedUser[0]["id"]).subscribe((data: any) => {
      this.toastr.showSuccess("deleted success fully");
      this.getAllUsers("", "", "");
    },
      (err: HttpErrorResponse) => {

        console.error(err);
        this.toastr.showError();
      });

  }

  userSearchData(userSearch)
  {
    this.getAllUsers(userSearch, this.SfromDate, this.StoDate);
  }
  
  selectEvent(item) {
    // do something with selected item
    alert("selected");
  }

  onChangeSearch(search: string) {
    // fetch remote data from here
    // And reassign the 'data' which is binded to 'data' property.
    alert("changed");
  }

  onFocused(e) {
    // do something
    alert("focused");
  }

  mobilevalidator(event: any) {
    const pattern = /[0-9\+\-\ ]/;

    let inputChar = String.fromCharCode(event.charCode);
    if (event.keyCode != 8 && !pattern.test(inputChar)) {
      event.preventDefault();
    }
  }

  isEmail(search: string): boolean {
    var serchfind: boolean;

   var regexp = new RegExp(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);

    serchfind = regexp.test(search);

    console.log(serchfind)
    return serchfind
  }

// this.userService.getAutoCompleteCompUsers(this.searchTerm$)
//      .subscribe(results => {
//        this.results = results.results;
//      });
}
