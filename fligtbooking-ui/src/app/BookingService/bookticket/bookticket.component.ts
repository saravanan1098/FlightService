import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { BookingserviceService } from '../bookingservice.service';
import { airline } from '../Models/airline';
import { bookingdtos } from '../Models/bookingdtos';
import { booking } from '../Models/Bookingfull';
//import { discount } from '../Models/discount';

import { flight } from '../Models/flight';
import { flightget } from '../Models/flightget';
import { seatnumber } from '../Models/seatnumber';

@Component({
  selector: 'app-bookticket',
  templateUrl: './bookticket.component.html',
  styleUrls: ['./bookticket.component.css']
})
export class BookticketComponent implements OnInit {

  Mailid = localStorage.getItem('MailId') as string;
    //Mailid = '';

 flightId: string | null | undefined;
  flight: flightget = {
    flightId: '',
    airlineId: '',
    flightNumber: '',
    fromPlace: '',
    toPlace: '',
    startDateTime: '',
    endDateTime: '',
    scheduledDays: '',
    businessClassSeats: '',
    nonBusinessClassSeats: '',
    mealType: '',
    typeofTrip: '',
    businessClassSeatTicketCost: '',
    nonBusinessClassSeatTicketCost: '',
    airlineName: '',
    logo: '',
    seatnumbers: []
  }

  booking: bookingdtos = {
    bookingName: '',
    mailId: '',
    flightNumber: '',
    //discontcode: '',
    numberofSeats: '',
    passengerDtos: []
  }

  airlines: airline[] = []
  //discounts:discount[]=[]
  isNewFlight = false;

  bookingForm: FormGroup;


  constructor(
    private readonly bookingService:BookingserviceService,
    private readonly route: ActivatedRoute,
    private snackbar: MatSnackBar,
    private router: Router,
    private fb:FormBuilder
  ) {

      this.bookingForm = this.fb.group({

      flightNumber: '',
      bookingName: '',
      mailId: this.Mailid,
      //discountcode: '',
      passengerDtos: this.fb.array([]),

    });


   }

   passengerDtos() : FormArray {
    return this.bookingForm.get("passengerDtos") as FormArray
  }

  newPassenger(): FormGroup {
    return this.fb.group({
      name: '',
      age: '',
      gender: '',
      mealType: '',
      //seatNumber: ''
    })
  }

  addPassenger() {

    this.passengerDtos().push(this.newPassenger());
  }

  removePassenger(i: number) {

    this.passengerDtos().removeAt(i);
  }



  ngOnInit(): void {

      //  this.bookingService.getdiscounts()
      // .subscribe(
      //   (successResponse) => {
      //    this.discounts=successResponse;
      //   },
      //   (errorResponse) => {
      //     // Log
      //   }
      // );

    this.route.paramMap.subscribe(
      (params) => {
        this.flightId = params.get('id');

        if (this.flightId) {
          this.isNewFlight = false;
          this.bookingService.getflight(this.flightId)
            .subscribe(
              (successResponse) => {
                this.flight = successResponse;
                console.log(this.flight)
               // console.log(this.flight.flightNumber)
                //console.log(this.discounts)
                //console.log(this.flight.seatnumbers)

      this.bookingForm = this.fb.group({
      flightNumber: this.flight.flightNumber,
      bookingName: '',
      mailId: this.Mailid,
      //discountcode: '',
      passengerDtos: this.fb.array([]) ,
    });


              }
            );
        }
      }
    );

  }

  onBook(): void {
    console.log(this.flight.flightNumber)
    console.log(this.bookingForm.value)
    this.bookingService.bookticket(this.bookingForm.value)
      .subscribe(
        (successResponse) => {
          this.snackbar.open('Flight Booked successfully-Transferring to Booking History', undefined, {
            duration: 2000
          });

          setTimeout(() => {
            this.router.navigateByUrl(`bookinghistory`);
          }, 2000);

        },
        (errorResponse) => {
          // Log
        }
    );

  }


  }

