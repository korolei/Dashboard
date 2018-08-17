import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER} from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { Ng4LoadingSpinnerModule } from 'ng4-loading-spinner';

import { AppRoutingModule } from './app-routing.module';
import { AppService } from './app.service';
import { AppComponent } from './app.component';
import { HeaderComponent } from './header/header.component';
import { FooterComponent } from './footer/footer.component';
import { HttpClientModule } from '@angular/common/http'; 
import { ConfigurationService } from './shared/configuration-service';
import { DashboardItemsComponent } from './dashboard-items/dashboard-items.component';

// apply Wijmo license key
//import { setLicenseKey } from 'wijmo/wijmo';
//setLicenseKey('your license key goes here');

@NgModule({
  declarations: [
    AppComponent,
    HeaderComponent,
    FooterComponent,
    DashboardItemsComponent
  ],
  imports: [
    BrowserModule,
    NgbModule.forRoot(),
    Ng4LoadingSpinnerModule.forRoot(),
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule
  ],
  providers: [
    AppService,
   // Register our config service as a provided service that
    // components may request.
    ConfigurationService,
    {
      // Here we request that configuration loading be done at app-
      // initialization time (prior to rendering)
      provide: APP_INITIALIZER,
     useFactory: (configService: ConfigurationService) =>
       () => configService.loadGlobalConfigData(),
     deps: [ConfigurationService],
     multi: true
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
