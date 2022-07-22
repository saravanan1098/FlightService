import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router, RouterLink } from '@angular/router';
import { FlighService } from 'src/app/FlightService/fligh.service';
import { BookingserviceService } from '../bookingservice.service';
import { airline } from '../Models/airline';
import { flight } from '../Models/flight';

@Component({
  selector: 'app-listflights',
  templateUrl: './listflights.component.html',
  styleUrls: ['./listflights.component.css']
})
export class ListflightsComponent implements OnInit {


  Token = localStorage.getItem('Token')
  Role = localStorage.getItem('role')
  isUserLoggedIn = false;

  airline: airline[] = [];
  flight: flight[] = [];

  filterStringfromplace = '';
  filterStringtoplace = '';
  filterStringdate = '';
  filterStringtype = '';

  displayedColumns: string[] = ['logo','airlineName','flightnumber', 'fromPlace','toPlace', 'startDateTime', 'endDateTime','typeofTrip', 'book'];
  dataSource: MatTableDataSource<flight> = new MatTableDataSource<flight>();
  @ViewChild(MatPaginator) matPaginator!: MatPaginator;
  @ViewChild(MatSort) matSort!: MatSort;
  filterString = '';
  displayProfileImageUrl = '';
  displayDefaultimage = '/assets/flight.png';
  constructor(private bookingservice: BookingserviceService,
  private flighservice:FlighService,
  private router: Router) { }

  ngOnInit(): void {

         if(this.Token!=null)
    {
       this.isUserLoggedIn = true;
    }else{
        this.isUserLoggedIn = false;
    }


    this.bookingservice.getflights()
      .subscribe(
        (successResponse) =>
        {
          this.flight = successResponse;

          console.log(this.flight);
          this.dataSource = new MatTableDataSource<flight>(this.flight);
          this.displayProfileImageUrl = this.flighservice.getbaseurl();
          this.displayDefaultimage;
          if (this.matPaginator) {
            this.dataSource.paginator = this.matPaginator;
          }

          if (this.matSort) {
            this.dataSource.sort = this.matSort;
          }
        },
        (errorResponse) =>
        {
          console.log(errorResponse);
        }

    );

  }
  filterFlights() {
    this.dataSource.filter =
       this.filterStringfromplace.trim().toLowerCase()
      && this.filterStringtoplace.trim().toLowerCase()
      && this.filterStringdate.trim().toLowerCase()
      && this.filterStringtype.trim().toLowerCase();
  }
  // bookingroute(){
  //   if(this.isUserLoggedIn){
  //     this.router.navigateByUrl('/bookticket/');
  //   }
  //   else{
  //   this.router.navigateByUrl('/login')

  //   }

  }



