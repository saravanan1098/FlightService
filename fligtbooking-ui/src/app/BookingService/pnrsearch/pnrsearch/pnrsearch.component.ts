import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { BookingserviceService } from '../../bookingservice.service';
import { booking } from '../../Models/Bookingfull';

@Component({
  selector: 'app-pnrsearch',
  templateUrl: './pnrsearch.component.html',
  styleUrls: ['./pnrsearch.component.css']
})
export class PnrsearchComponent implements OnInit {


   filterString = '';
  booking: booking = {
    bookingId: '',
    bookingName: '',
    mailId: '',
    pnr: '',
    bookingDateTime: '',
    flightNumber: '',
    numberofSeats: '',
    totalCost: '',
    status: '',
    passengers: []
  }



  constructor(private bookingservice: BookingserviceService) { }

  ngOnInit(): void {
  }

 findPNR(): void {
    this.bookingservice.pnrsearch(this.filterString)
      .subscribe(
        (successResponse) => {

         this.booking = successResponse;

          console.log(this.booking);

        },
        (errorResponse) => {
          // Log
        }
      );

 }



  clear(): void {
    this.filterString = '';
  window.location.reload();
}


}
