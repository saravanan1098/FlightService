import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UrlSerializer } from '@angular/router';
import { Observable, observable } from 'rxjs';
import { user } from './models/user';
import { userdto } from './models/userdto';

@Injectable({
  providedIn: 'root'
})
export class AuthserviceService {
  // private baseApiUrl='https://localhost:44321';
  private baseApiUrl='https://flightbookingapi.azure-api.net/authenticationservice';

  constructor(private httpClient:HttpClient) { }

adduser(user:user):Observable<user>{
  return this.httpClient.post<user>(this.baseApiUrl + '/api/Login/register', user);

}
login(userdto:userdto):Observable<user>{
  return this.httpClient.post<user>(this.baseApiUrl + '/api/Login', userdto);

}

}
