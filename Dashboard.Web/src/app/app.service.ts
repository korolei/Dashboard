import 'rxjs/add/observable/fromPromise';
import 'rxjs/add/observable/of';
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/mergeMap';
import 'rxjs/add/operator/catch';

import { Observable } from 'rxjs/Observable';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpResponseBase } from '@angular/common/http';
import { ConfigurationService } from './shared/configuration-service';
import { SwaggerException } from './shared/swagger-exception';

@Injectable()
export class AppService {
  protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

  constructor(public http: HttpClient, private configService: ConfigurationService) {}
    
    public post(filter: any, url:string): Observable<any> {
        let url_ = url.replace(/[?&]$/, "");

        const content_ = JSON.stringify(filter);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json", 
                "Accept": "*/*"
            })
        };
        this.configService.loadingStateChanged.next(true);
        return this.http.request("post", url_, options_).flatMap((response_ : any) => {
            return this.processPost(response_);
        }).catch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processPost(<any>response_);
                } catch (e) {
                    return <Observable<any>><any>Observable.throw(e);
                }
            } else
                return <Observable<any>><any>Observable.throw(response_);
        });
    }

    protected processPost(response: HttpResponseBase): Observable<any> {
        const status = response.status;
        const responseBlob = 
            response instanceof HttpResponse ? response.body : 
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;
            
        this.configService.loadingStateChanged.next(false);

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }};
        if (status === 200) {
            return blobToText(responseBlob).flatMap(_responseText => {
            let result200: any = null;
            result200 = _responseText === "" ? null : <any>JSON.parse(_responseText, this.jsonParseReviver);
            return Observable.of(result200);
            });
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).flatMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            });
        }
        
        return Observable.of<any>(<any>null);
    }
}

function blobToText(blob: any): Observable<string> {
    return new Observable<string>((observer: any) => {
        if (!blob) {
            observer.next("");
            observer.complete();
        } else {
            let reader = new FileReader(); 
            reader.onload = function() { 
                observer.next(this.result);
                observer.complete();
            }
            reader.readAsText(blob); 
        }
    });
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): Observable<any> {
  if(result !== null && result !== undefined)
      return Observable.throw(result);
  else
      return Observable.throw(new SwaggerException(message, status, response, headers, null));
}