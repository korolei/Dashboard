import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppCustomPreloader } from './shared/app-routing-loader';
import { DashboardItemsComponent } from './dashboard-items/dashboard-items.component';

export const routes: Routes = [
  { path: '', component: DashboardItemsComponent},
  { path:'borrowing-profile', loadChildren:'./borrowing-profile/borrowing-profile.module#BorrowingProfileModule'},
  { path: '**', redirectTo: 'app-dashboard-items' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true})],
  exports: [RouterModule],
  providers:[AppCustomPreloader]
})
export class AppRoutingModule { }
