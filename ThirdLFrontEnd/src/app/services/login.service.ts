import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {environment} from '../../environments/environment';
import {Observable} from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private httpClient: HttpClient) { }


  loginPatient(obj: any): Observable<any> {
    return this.httpClient.post(`${environment.serverUrl}authenticate`, obj);
  }

  loginDoctor(obj: any): Observable<any>  {
    return this.httpClient.post(`${environment.serverUrl}authenticate/doctor`, obj);
  }

}
