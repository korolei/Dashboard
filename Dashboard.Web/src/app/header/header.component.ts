// src/app/header/header.component.ts
import { Component, OnInit, OnDestroy } from '@angular/core';
import 'rxjs/add/operator/filter';
import { ConfigurationService } from '../shared/configuration-service';
import { Subscription } from 'rxjs/internal/Subscription';
import { Ng4LoadingSpinnerService } from 'ng4-loading-spinner';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  appTitle: string;

  constructor(
    public configServices: ConfigurationService) { }

  ngOnInit() {
      this.appTitle = this.configServices.globalConfig.applicationName;
  }

}
