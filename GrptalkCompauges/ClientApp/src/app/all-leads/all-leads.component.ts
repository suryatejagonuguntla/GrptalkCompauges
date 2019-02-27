import { Component , OnInit} from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { UserService } from '../shared/user.service';
import { DataService } from '../shared/test.service';
import { toasterService } from '../shared/toaster.service';
import { DatePipe } from '@angular/common';
import { Observable } from 'rxjs';
import { NgbModal, ModalDismissReasons, NgbModalRef, NgbModule} from '@ng-bootstrap/ng-bootstrap';

@Component({
    selector: 'app-all-leads',
    templateUrl: './all-leads.component.html',
    styleUrls: ['./all-leads.component.css']
})
/** all-leads component*/
export class AllLeadsComponent implements OnInit
{
    /** all-leads ctor */
  public isDateCollapsed = true;
  public isRsNamesCollapsed = true;
  public isLeadStageCollapsed = true;
  public isAllDealsCollapsed = true;
  public isFiltersShow = true;
  public isDetailsShow = false;
  public modalRef: NgbModalRef;
  public closeResult: string;
  public Leads: any;
  public isLeadShow: any;
  public searchInfo: any;
  public LeadSummary: any = { "totalLeadsCreated": 0, "businessDemo": 0, "businessLeads": 0, "notSatisfiedLeads" :0 };
  public entireLeads: any;
  public LeadEdit: any = { address: "", comments: "", company: "", email: "", fullName: "", isChecked: 0, leadIn: "", mobile: "", stage: "", leadId: 0, stageId: 0 };
  public LeadAdd: any = { address: "", comments: "", company: "", email: "", fullName: "", leadIn: "", mobile: "", stageId: 0 , CreatedAt : "" };
  public isShowLeadStageDropDown = false;
  public isDateShow: any;
  public isResellerShow: any;
  public SfromDate = "";
  public StoDate = "";
  public customFDate = "";
  public customTDate = "";
  public searchLeads = '';
  public searchReseller = '';
  public leadSearchInput = "";
  public selectedStage = 0;
  public CreatedDateForLead = "";

  LeadStages : Observable<any[]>;
  selectedLeads = [];

  Resellers: Observable<any[]>;
  selectedResellers = [];

  
 
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
  dateCBoxes = { LDayChecked: false, LWeekChecked: false, LMonthChecked: false, CustomDate: false };
  dateCDays = { LDayCheckedFDate: 1, LDayCheckedTDate: 1, LWeekCheckedFDate: 7, LWeekCheckedTDate: 0, LMonthCheckedFDate: 31, LMonthCheckedTDate: 0 };

  constructor(private modalService: NgbModal, private dataService: DataService, private userService: UserService, public toastr: toasterService, private datePipe: DatePipe) {
      
  }

  ngOnInit()
  {

    this.getLeadStagesAndReseller();
    
    //this.LeadStages = this.dataService.getLeadStages(this.searchInfo);
    //this.Resellers = this.dataService.getResellerNames(this.searchInfo);

    this.getAllleads("", "", "", "", "", "");
    //console.log(this.searchInfo);
    //console.log("sample");
    //console.log(this.Leads);
    
    //console.log(this.LeadStages);
  }

  toogleFilter() {
    console.log("before switch" + this.isFiltersShow);
    if (this.isFiltersShow == true) {
      this.isFiltersShow = false;
    }
    else {
      this.isFiltersShow = true;
    }
    console.log("after switch" + this.isFiltersShow);
  }
  detailsBar(seletedLead)
  {
    //this.LeadEdit = seletedLead;
    this.isShowLeadStageDropDown = false;
    var leadId = this.LeadEdit["leadId"];
    for (var key in this.LeadEdit )
    {
       this.LeadEdit[key] = seletedLead[key];
    }

    //console.log(this.LeadEdit);
    //console.log(seletedLead);
    console.log(this.LeadEdit["leadId"]);
    console.log();
    if (leadId == this.LeadEdit["leadId"] || (this.LeadEdit["leadId"] != 0 && this.isDetailsShow == false)   )
    {
      console.log("test");
      this.isDetailsShow = !this.isDetailsShow;
    }
    
  }

  open(addLead) {
    this.modalRef = this.modalService.open(addLead);
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

  private getLeadStagesAndReseller()
  {

    var ApiSearchData: any;
    //console.log(fromDate);
    //console.log(toDate);
    //let searchString = "SearchInput=" + searchData + "&Deals=" + Deals + "&leadStages=" + leadStages + "&fromDate=" + fromDate + "&toDate=" + toDate + "&resellerNames=" + resellerNames;
    this.userService.getLdStageAndReseller("", "AllLeads").subscribe((data: any) => {
      console.log("test");
      console.log(data);
      this.searchInfo = data;
      console.log(this.searchInfo);

      this.LeadStages = this.dataService.getLeadStages(this.searchInfo);
      this.Resellers = this.dataService.getResellerNames(this.searchInfo);

    },
      (err: HttpErrorResponse) => {
        console.error(err);
        this.toastr.showError();
      });

    return ApiSearchData;
  }
  private getAllleads(searchData, Deals, leadStages, fromDate, toDate, resellerNames) {

    console.log(fromDate);
    console.log(toDate);
    let searchString = "SearchInput=" + searchData + "&Deals=" + Deals + "&leadStages=" + leadStages + "&fromDate=" + fromDate + "&toDate=" + toDate + "&resellerNames=" + resellerNames;
    this.userService.getAllLeads(searchString, "AllLeads").subscribe((data: any) => {

      this.Leads = data[0];
      this.LeadSummary = data[1][0];
      console.log(this.Leads);
      console.log(this.LeadSummary);
    },
      (err: HttpErrorResponse) => {
        console.error(err);
        this.toastr.showError();
      });


  }
  isShowLeadDropDown() {
    if (this.isShowLeadStageDropDown == false)
    {
       this.isShowLeadStageDropDown =  true;
    }
    else if (this.isShowLeadStageDropDown == true) {
       this.isShowLeadStageDropDown =  false;
    }
    
    console.log(this.isShowLeadStageDropDown);
  }
  AddLeads()
  {
    var isPass = this.validateAddlead();
    if (isPass != 1)
    {
      console.log(this.LeadAdd);
      this.userService.AddLead(this.LeadAdd, "AllLeads").subscribe((data: any) => {
        this.toastr.showSuccess("Lead Added Successfully");
        this.modalRef.close();
        for (var key in this.LeadAdd) {
          this.LeadAdd[key] = "";
        }
        this.LeadAdd["stageId"] = 0;
        //this.getAllleads(this.leadSearchInput, "", this.searchLeads, this.SfromDate, this.StoDate, this.searchReseller);

      },
        (err: HttpErrorResponse) => {
          console.error(err);
          this.toastr.showError();
        });
    }

  }
  updateLeadDetails()
  {
    if (this.isShowLeadStageDropDown == true) {
      this.LeadEdit["stageID"] = this.selectedStage
      //alert(this.selectedStage);
    }
    else
    {
      this.LeadEdit["stageID"] = this.LeadEdit["stageId"];
    }
    console.log(this.LeadEdit);
    
    this.userService.updateSingleLead(this.LeadEdit, "AllLeads").subscribe((data: any) => {
      this.toastr.showSuccess("Data Edited Successfully");
      this.isDetailsShow = false;
      this.getAllleads(this.leadSearchInput, "", this.searchLeads, this.SfromDate, this.StoDate, this.searchReseller);
      
    },
      (err: HttpErrorResponse) => {
        console.error(err);
        this.toastr.showError();
      });
  }

  edit(selectedLead)
  {
    this.LeadEdit = selectedLead;
    this.isDetailsShow = true;
  }

  delete(deleteLead)
  {

    if (deleteLead.isChecked == true) {
      this.userService.deleteLead(deleteLead.leadId, "AllLeads").subscribe((data: any) => {
        this.toastr.showSuccess("Lead Deleted Successfully");
        this.isDetailsShow = false;
        this.getAllleads(this.leadSearchInput, "", this.searchLeads, this.SfromDate, this.StoDate, this.searchReseller);

      },
        (err: HttpErrorResponse) => {
          console.error(err);
          this.toastr.showError();
        });
    }
    else
    {
      this.toastr.showWarning("Please Select Check box");
    }
    
  }

  selectedDate(key, isChecked) {

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

        this.getAllleads(this.leadSearchInput, "", this.searchLeads, this.SfromDate, this.StoDate, this.searchReseller);
        //this.getAllUsers(this.userSearchInput, this.SfromDate, this.StoDate);
      }
      else {
        console.log(this.fromDate);
        console.log(this.toDate);
        this.dateCBoxes["CustomDate"] = true;
        this.customDateSearch("", "");
      }

    }
    else {
      this.SfromDate = "";
      this.StoDate = "";
      this.getAllleads(this.leadSearchInput, "", this.searchLeads, this.SfromDate, this.StoDate, this.searchReseller);
      //this.getAllUsers(this.userSearchInput, this.SfromDate, this.StoDate);

    }
  }
  leadSelected()
  {
    
    if (this.selectedLeads.length > 0 || this.searchLeads != "")
    {
      this.searchLeads = "";
      for (var i = 0; i < this.selectedLeads.length; i++) {
        this.searchLeads += this.selectedLeads[i].id + ','
      }

      console.log(this.searchLeads);
      console.log(this.selectedLeads);
      this.getAllleads(this.leadSearchInput, "", this.searchLeads, this.SfromDate, this.StoDate, this.searchReseller);
    }
    
  }
  
  resellerSelected()
  {
    
    if (this.selectedResellers.length > 0 || this.searchReseller != "")
    {
      this.searchReseller = "";
      for (var i = 0; i < this.selectedResellers.length; i++) {
        this.searchReseller += this.selectedResellers[i].id + ','
      }
      //this.getAllleads(searchData, Deals, leadStages, fromDate, toDate, resellerNames);
      this.getAllleads(this.leadSearchInput, "", this.searchLeads, this.SfromDate, this.StoDate, this.searchReseller);
      console.log(this.searchReseller);
      console.log(this.selectedResellers);
    }
  }
  customDateSearch(customDate, customDateKey) {
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
        this.getAllleads(this.leadSearchInput, "", this.searchLeads, this.SfromDate, this.StoDate, this.searchReseller);
      }

    }
    else {
      this.toastr.showWarning("please select custom date filter");
      this.SfromDate = "";
      this.StoDate = "";
      return;
    }
  }

  leadSearchData(LeadSearch)
  {
    this.getAllleads(LeadSearch, "", this.searchLeads, this.SfromDate, this.StoDate, this.searchReseller);
  }

  selectedRow( isChecked) {
    
    console.log(isChecked);
    if (isChecked == false) {

      console.log(this.Leads.length);
      for (var i = 0; i < this.Leads.length; i++) {

        if (this.Leads[i].isChecked == true) {
          this.Leads[i].isChecked = false;
          
        }
      }
    }

  }

  validateAddlead()
  {
    var isValidated = 0;
    console.log(this.LeadAdd);
    for (var key in this.LeadAdd)
    {
      if (this.LeadAdd[key] == "" || this.LeadAdd[key] == 0)
      {
        isValidated = 1;
        this.toastr.showWarning("please fill " + key);
      }

    }
    return isValidated;
  }
}
