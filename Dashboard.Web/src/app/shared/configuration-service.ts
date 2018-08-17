import { Injectable } from '@angular/core';
import { HttpClient} from '@angular/common/http';
import {Subject} from 'rxjs';
import 'rxjs/add/operator/toPromise';

import { GlobalSettings } from './global-settings';
import { BorrowingProfileSettings } from '../borrowing-profile/models/borrowing-profile-settings';

@Injectable()
export class ConfigurationService {
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;
	public loadingStateChanged = new Subject<boolean>();
	private readonly globalConfigUrl: string = 'api/home/dashboard-settings';
    private readonly borrowingProfileConfigUrl: string = 'api/borrowingprofile/borrowing-profile-settings';
    
	private globalConfigData: GlobalSettings;
    private borrowingProfileConfigData: BorrowingProfileSettings;
    
	constructor(
		private http: HttpClient
	) {}

    public loadGlobalConfigData(): Promise<any>{
        return this.loadConfigurationData(this.globalConfigUrl).then((data: GlobalSettings)=>{
            this.globalConfigData=data as GlobalSettings;
        });
    }
    public loadBorrowingProfileConfigData(): Promise<any>{
        return this.loadConfigurationData(this.borrowingProfileConfigUrl).then((data:BorrowingProfileSettings)=>{
            this.borrowingProfileConfigData = data as BorrowingProfileSettings;
        });
    }

	private loadConfigurationData(urlPath: string): Promise<any> {
		return this.http.get(urlPath)
			.toPromise()
			.then((response: Response) => {
				return response;
			})
			.catch(err => {
				return Promise.reject(err);
			});
	}

	// A helper property to return the config object
	get globalConfig(): GlobalSettings {
		return this.globalConfigData;
	}
	get borrowingProfileConfig(): BorrowingProfileSettings {
		return this.borrowingProfileConfigData;
	}

}
