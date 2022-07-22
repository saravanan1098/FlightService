import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { concat } from 'rxjs/operators';
import { addairline } from '../models/ui-model/addairline';
import { addflight } from '../models/ui-model/addflight';
import { airline } from '../models/ui-model/airline';
import { flight } from '../models/ui-model/flight';
import { updateairline } from '../models/ui-model/updateairline';
import { updateflight } from '../models/ui-model/updateflight';

@Injectable({
  providedIn: 'root'
})


export class FlighService {

  public isUserLoggedIn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  public isAdmin: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  // private baseApiUrl='https://flightbookingapi.azure-api.net/flightservice';
  private baseApiUrl='https://localhost:44330';

  constructor(private httpClient:HttpClient)  {}

  getFlights():Observable<flight[]>{
    const httpOptions = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + localStorage.getItem('Token')
    });
    const Token = { headers: httpOptions };

    return this.httpClient.get<flight[]>(this.baseApiUrl+'/api/Flight',Token);

  }
  getFlight(flightId:string): Observable<flight>
  {
    const httpOptions = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + localStorage.getItem('Token')
    });
    const Token = { headers: httpOptions };

    return this.httpClient.get<flight>(this.baseApiUrl+'/api/Flight/'+flightId,Token)
  }
  updateFlight(flightId: string, flightrequest:flight): Observable<flight> {
    const updateflightmodel: updateflight={
      airlineId: flightrequest.airlineId,
      flightNumber: flightrequest.flightNumber,
      fromPlace: flightrequest.fromPlace,
      toPlace: flightrequest.toPlace,
      startDateTime: flightrequest.startDateTime,
      endDateTime: flightrequest.endDateTime,
      scheduledDays: flightrequest.scheduledDays,
      businessClassSeats: flightrequest.businessClassSeats,
      nonBusinessClassSeats: flightrequest.nonBusinessClassSeats,
      mealType: flightrequest.mealType,
      typeofTrip: flightrequest.typeofTrip,
      businessClassSeatTicketCost: flightrequest.businessClassSeatTicketCost,
      nonBusinessClassSeatTicketCost: flightrequest.nonBusinessClassSeatTicketCost
    }

          const httpOptions = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + localStorage.getItem('Token')
    });
    const Token = { headers: httpOptions };

    return this.httpClient.put<flight>(this.baseApiUrl + '/api/Flight/' + flightId, updateflightmodel,Token);
  }

  deleteFlight(flightId: string): Observable<flight> {

          const httpOptions = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + localStorage.getItem('Token')
    });
    const Token = { headers: httpOptions };
    return this.httpClient.delete<flight>(this.baseApiUrl + '/api/Flight/' + flightId,Token);
  }

  addFlight(flightrequest:flight): Observable<flight> {
    const addflightmodel: addflight={
      airlineId: flightrequest.airlineId,
      flightNumber: flightrequest.flightNumber,
      fromPlace: flightrequest.fromPlace,
      toPlace: flightrequest.toPlace,
      startDateTime: flightrequest.startDateTime,
      endDateTime: flightrequest.endDateTime,
      scheduledDays: flightrequest.scheduledDays,
      businessClassSeats: flightrequest.businessClassSeats,
      nonBusinessClassSeats: flightrequest.nonBusinessClassSeats,
      mealType: flightrequest.mealType,
      typeofTrip: flightrequest.typeofTrip,
      businessClassSeatTicketCost: flightrequest.businessClassSeatTicketCost,
      nonBusinessClassSeatTicketCost: flightrequest.nonBusinessClassSeatTicketCost
    }
       const httpOptions = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + localStorage.getItem('Token')
    });
   const Token = { headers: httpOptions };
   console.log("addfl")
   console.log(addflightmodel)
    return this.httpClient.post<flight>(this.baseApiUrl + '/api/Flight/add', addflightmodel,Token);
  }

  getairlines():Observable<airline[]>
  {
       const httpOptions = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + localStorage.getItem('Token')
    });
    const Token = { headers: httpOptions };
     return this.httpClient.get<airline[]>(this.baseApiUrl + '/api/Airline',Token);
  }


  getairline(airlinetId: string): Observable<airline> {

    const httpOptions = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + localStorage.getItem('Token')
    });
    const Token = { headers: httpOptions };
    return this.httpClient.get<airline>(this.baseApiUrl + '/api/airline/' + airlinetId,Token)
    }

  updateairline(airlineId: string, airlinerequest:airline): Observable<airline> {
    const updateairlinemodel: updateairline={
      airlineName: airlinerequest.airlineName,
      contactAddress: airlinerequest.contactAddress,
      contactNumber: airlinerequest.contactNumber,

    }
    const httpOptions = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + localStorage.getItem('Token')
    });
    const Token = { headers: httpOptions };

    return this.httpClient.put<airline>(this.baseApiUrl + '/api/airline/' + airlineId, updateairlinemodel,Token);
  }

  deleteairline(airlineId: string): Observable<airline> {
    const httpOptions = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + localStorage.getItem('Token')
    });
    const Token = { headers: httpOptions };
    return this.httpClient.delete<airline>(this.baseApiUrl + '/api/airline/' + airlineId,Token);
  }

    blockairline(airlineId: string): Observable<airline> {
    const httpOptions = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + localStorage.getItem('Token')
    });
    const Token = { headers: httpOptions };
    return this.httpClient.get<airline>(this.baseApiUrl + '/api/airline/block/' + airlineId,Token);
    }

    unblockairline(airlineId: string): Observable<airline> {
    const httpOptions = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + localStorage.getItem('Token')
    });
    const Token = { headers: httpOptions };
    return this.httpClient.get<airline>(this.baseApiUrl + '/api/airline/unblock/' + airlineId,Token);
  }

  addairline(airlinerequest:airline): Observable<airline> {
    const addairlinemodel: addairline={
      airlineName: airlinerequest.airlineName,
      contactAddress: airlinerequest.contactAddress,
      contactNumber: airlinerequest.contactNumber
    }

    const httpOptions = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + localStorage.getItem('Token')
    });
    const Token = { headers: httpOptions };

    return this.httpClient.post<airline>(this.baseApiUrl + '/api/airline/add/', addairlinemodel,Token);
  }

    uploadImage(flightId: string, file: File): Observable<any> {
    const formData = new FormData();
    formData.append("profileImage", file);


    return this.httpClient.post(this.baseApiUrl + '/api/airline/logo/' + flightId ,
      formData, {
      responseType: 'text'
    }
    );
  }
  getImagePath(relativePath: string) {
    return `${this.baseApiUrl}/${relativePath}`;
  }

    getbaseurl() {
    return `${this.baseApiUrl}/`;
  }
  }

