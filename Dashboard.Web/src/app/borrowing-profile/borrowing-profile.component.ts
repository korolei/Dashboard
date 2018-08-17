
import { Component, OnInit} from '@angular/core';
import { Title } from '@angular/platform-browser';
import 'rxjs/add/observable/fromEvent';
import 'rxjs/add/operator/debounceTime';
import { FormGroup, FormBuilder } from '@angular/forms';
import { 
          BorrowingProfileViewModel, 
          BorrowingProfileFilter, 
          DisplayType, 
          ProfileDataModel, 
          enumSelector,
          ProfileDetailsModel,
          ProfileDealsModel
        } from './models/borrowing-profile-view-model';

import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import * as wjcGrid from 'wijmo/wijmo.grid';
import { FlexPie } from 'wijmo/wijmo.chart';
import { ConfigurationService } from '../shared/configuration-service';
import { AppService } from '../app.service';

@Component({
  selector: 'app-borrowing-profile',
  templateUrl: './borrowing-profile.component.html'
})
export class BorrowingProfileComponent implements OnInit{
  borrowingProfileApi = "/api/BorrowingProfile/";
  profileDetailsUrl = this.borrowingProfileApi + 'profile-trades/';
  profileDealsUrl = this.borrowingProfileApi + 'profile-deals/';

  data_vm: BorrowingProfileViewModel;
  profileDeals: ProfileDealsModel;
  filter_vm: BorrowingProfileFilter;
  displayType: DisplayType = DisplayType.Grid;
  profileDetails: ProfileDetailsModel;
  displayTypes = enumSelector(DisplayType);
  todayDate = new Date();
  pageTitle = 'Borrowing Profile';
  filterFormGroup: FormGroup;
  profileName: string;

  constructor(
    private title: Title,
    private modalService: NgbModal,
    private formBuilder: FormBuilder,
    public service: AppService,
    public configService: ConfigurationService) {}

   ngOnInit() {
     //loading config data first, then rest of the data
     this.configService.loadBorrowingProfileConfigData().then(()=> this.initComponent());
    }

  initComponent(): any {
    this.title.setTitle(this.configService.borrowingProfileConfig.applicationName);

     this.service.post(null,this.borrowingProfileApi).subscribe(data => {
       this.data_vm = data as BorrowingProfileViewModel;
       this.initForm();
      });
  }
    initForm(): void {
      this.filterFormGroup = this.formBuilder.group({
        display: [this.data_vm.filter.display],
        includeCpp: [this.data_vm.filter.includeCpp],
        includeOiic: [this.data_vm.filter.includeOiic],
        startFiscalYear: [this.data_vm.filter.startFiscalYear],
        viewMode:[this.data_vm.filter.viewMode],
        profileName:[this.data_vm.filter.profileName]
      });
    }

    getTrades (profileName:string, dealId:number, modalName: string){
      this.filterFormGroup.value.profileName = profileName;
      this.service.post(this.filterFormGroup.value, this.profileDetailsUrl + dealId).subscribe(data =>{
        this.profileDetails = data as ProfileDetailsModel;
        this.openModal(modalName);
       })
   }

   getProfileDeals (profileName: string, modalName){
    this.filterFormGroup.value.profileName = profileName;
    this.service.post(this.filterFormGroup.value, this.profileDealsUrl).subscribe(data =>{
      this.profileDeals = data as ProfileDealsModel;
      this.profileName=profileName;
      this.openModal(modalName);
     })
   }

   postFilterData(){
     this.service.post(this.filterFormGroup.value, this.borrowingProfileApi).subscribe(data =>{
      this.data_vm = data as BorrowingProfileViewModel;
     })
   }

   selectionChanged(s:FlexPie, modalName){
    let rowData = s.collectionView.currentItem as ProfileDataModel;
    this.getTrades(rowData.profileName,0, modalName)
   }

   openModal(modalContext){
      this.modalService.open(modalContext, { size: 'lg', centered: true });
   }

   isAheadOfPace(amount:number, profileName: string){
     if(amount === 0 || !profileName.startsWith('Pace')){
       return {'background-color':''};
     }
       return {'background-color': amount > this.data_vm.elapsedTime ? 'rgb(90,235,80)' : 'rgb(235,80,90)'};
   }

   // add a footer row to display column aggregates below the data
   addFooterRow(flexGrid: wjcGrid.FlexGrid) {
    var row = new wjcGrid.GroupRow(); // create a GroupRow to show aggregates
    flexGrid.columnFooters.rows.push(row); // add the row to the column footer panel
    flexGrid.columnFooters.setCellData(0, 0, 'Total Actual Borrowing:'); // sigma on the header
  }
}
