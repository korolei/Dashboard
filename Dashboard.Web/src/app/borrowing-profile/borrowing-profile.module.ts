import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BorrowingProfileComponent } from './borrowing-profile.component';

import { WjCoreModule } from 'wijmo/wijmo.angular2.core';
import { WjGridModule } from 'wijmo/wijmo.angular2.grid';
import { WjChartModule } from 'wijmo/wijmo.angular2.chart';
import { WjInputModule } from 'wijmo/wijmo.angular2.input';
import { WjGridDetailModule } from 'wijmo/wijmo.angular2.grid.detail';

const routes: Routes = [
  { path: "", component: BorrowingProfileComponent}
];

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    WjCoreModule,
    WjGridModule,
    WjChartModule,
    WjInputModule,
    WjGridDetailModule,
    RouterModule.forChild(routes),
  ],
  exports: [RouterModule],
  declarations: [
    BorrowingProfileComponent
  ]
})
export class BorrowingProfileModule { }
