import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { airline } from './Models/airline';
import { bookingdtos } from './Models/bookingdtos';
import { booking } from './Models/Bookingfull';
//import { discount } from './Models/discount';
import { flight } from './Models/flight';
import { flightget } from './Models/flightget';

@Injectable({
  providedIn: 'root'
})
export class BookingserviceService {
  allbooking() {
    throw new Error('Method not implemented.');
  }

    private baseapiurl= 'https://localhost:44392';
    // private baseapiurl= 'https://flightbookingapi.azure-api.net/bookingservice';

  constructor(private httpclient: HttpClient) { }
  getairlines():Observable<airline[]>
    {
     return this.httpclient.get<airline[]>(this.baseapiurl + '/api/BookingService/searchall');
  }

    getflights():Observable<flight[]>
    {
     return this.httpclient.get<flight[]>(this.baseapiurl + '/api/BookingService/searchall');
    }

  // getdiscounts():Observable<discount[]>
  //   {
  //    return this.httpclient.get<discount[]>(this.baseapiurl + '/discounts');
  // }
    getflight(flighttId: string): Observable<flightget> {

    return this.httpclient.get<flightget>(this.baseapiurl + '/api/BookingService/getflight/' + flighttId)
    }

   bookticket(booking:bookingdtos): Observable<booking> {

    return this.httpclient.post<booking>(this.baseapiurl + '/api/BookingService/bookticket/', booking);
  }


      pnrsearch(pnr: string): Observable<booking> {

    return this.httpclient.get<booking>(this.baseapiurl + '/api/BookingService/pnr/' + pnr)
      }



     mailsearch(mailId: string|null): Observable<booking[]> {

    return this.httpclient.get<booking[]>(this.baseapiurl + '/api/BookingService/mail/' + mailId)
    }
    allbookings(): Observable<booking[]> {

      return this.httpclient.get<booking[]>(this.baseapiurl + '/api/BookingService/allbookings');
      }

    cancelbooking(pnr: string|null): Observable<boolean> {

    return this.httpclient.get<boolean>(this.baseapiurl + '/api/BookingService/cancel/' + pnr);
  }

      getbaseurl() {
    return `${this.baseapiurl}/`;
  }

}
