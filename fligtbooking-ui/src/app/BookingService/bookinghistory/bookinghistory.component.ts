import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { BookingserviceService } from '../bookingservice.service';
import { booking } from '../Models/Bookingfull';

@Component({
  selector: 'app-bookinghistory',
  templateUrl: './bookinghistory.component.html',
  styleUrls: ['./bookinghistory.component.css']
})
export class BookinghistoryComponent implements OnInit {


  mailidls = localStorage.getItem('MailId')
  role=localStorage.getItem('Role')


  booking: booking[] = [];


  displayedColumns: string[] = ['pnr', 'flightNumber', 'bookingDateTime', 'numberofSeats', 'status', 'Cancel', 'pdf'];
  dataSource: MatTableDataSource<booking> = new MatTableDataSource<booking>();
  @ViewChild(MatPaginator) matPaginator!: MatPaginator;
  @ViewChild(MatSort) matSort!: MatSort;
  filterString = '';
  value1: string | null = '';
  URL = ''

  constructor(private bookingservice: BookingserviceService,
    private readonly route: ActivatedRoute,
    private snackbar: MatSnackBar,
    private router: Router) { }

  ngOnInit(): void {
    if(this.role==="admin"){
      this.bookingservice.allbookings()
      .subscribe(
        (successResponse) => {
          this.booking = successResponse;
          this.dataSource = new MatTableDataSource<booking>(this.booking);

          if (this.matPaginator) {
            this.dataSource.paginator = this.matPaginator;
          }

          if (this.matSort) {
            this.dataSource.sort = this.matSort;
          }
        },
        (errorResponse) => {
          console.log(errorResponse);
        }

      );
      }
      else{
    this.bookingservice.mailsearch(this.mailidls)
      .subscribe(
        (successResponse) => {
          this.booking = successResponse;
          this.dataSource = new MatTableDataSource<booking>(this.booking);

          if (this.matPaginator) {
            this.dataSource.paginator = this.matPaginator;
          }

          if (this.matSort) {
            this.dataSource.sort = this.matSort;
          }
        },
        (errorResponse) => {
          console.log(errorResponse);
        }

      );
      }


  }

  onDelete(value: string): void {


    this.bookingservice.cancelbooking(value)
      .subscribe(
        (successResponse) => {
          console.log(successResponse)
          console.log(value)
          this.snackbar.open('Booking Cancelled successfully', undefined, {
            duration: 3000
          });
          this.ngOnInit();
          // window.location.reload();

        },
        (errorResponse) => {
          this.snackbar.open('Cancellation Time Period Ended', undefined, {
            duration: 2000
          });
        }
      );
  };

  onGenerate(value: string): void {

    var URL = this.bookingservice.getbaseurl() + "api/BookingService/generatepdf/pnr/" + value;
    window.open(URL);

    this.snackbar.open('PDF Generated successfully', undefined, {
      duration: 3000
    });


  }
}

