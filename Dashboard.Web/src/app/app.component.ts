import { Component, OnInit, OnDestroy } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/fromEvent';
import 'rxjs/add/operator/debounceTime';

import { CollectionView } from 'wijmo/wijmo';
import { ConfigurationService } from './shared/configuration-service';
import { Ng4LoadingSpinnerService } from 'ng4-loading-spinner';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit, OnDestroy {
  isLoading = false;
  loadingSubs: Subscription;
  data: CollectionView;
  navOpen: boolean;
  minHeight: string;
  private _initWinHeight = 0;

  constructor(public title: Title, public configService: ConfigurationService,
    public spinnerService: Ng4LoadingSpinnerService) { }

  ngOnInit(): void {
    Observable.fromEvent(window, 'resize')
      .debounceTime(200)
      .subscribe((event) => this._resizeFn(event));

  this.title.setTitle(this.configService.globalConfig.applicationName);
  this._initWinHeight = this.configService.globalConfig.canvasHeight;
    this._resizeFn(null);

    this.loadingSubs = this.configService.loadingStateChanged.subscribe(isLoading=>{
      if(isLoading){
        this.spinnerService.show();
      }
      else{
        this.spinnerService.hide();
      }
    })
  }

  ngOnDestroy(): void {
    this.loadingSubs.unsubscribe();
  }

  navToggledHandler(e: boolean) {
    this.navOpen = e;
  }

  private _resizeFn(e) {
    const winHeight: number = e ? e.target.innerHeight : this._initWinHeight;
    this.minHeight = `${winHeight}px`;
  }

}
