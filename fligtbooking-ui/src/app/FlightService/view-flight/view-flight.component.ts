import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { airline } from 'src/app/models/ui-model/airline';
import { flight } from 'src/app/models/ui-model/flight';
import { FlighService } from '../fligh.service';

@Component({
  selector: 'app-view-flight',
  templateUrl: './view-flight.component.html',
  styleUrls: ['./view-flight.component.css']
})
export class ViewFlightComponent implements OnInit {
  flightId: string|null|undefined;
  flight: flight = {
    flightId:'',
    airlineId: '',
    flightNumber: '',
    fromPlace: '',
    toPlace: '',
    startDateTime: '',
    endDateTime: '',
    scheduledDays: '',
    instrumentUsed: '',
    businessClassSeats: '',
    nonBusinessClassSeats: '',
    numberofRows: '',
    mealType: '',
    typeofTrip: '',
    nonBusinessClassSeatTicketCost: '',
    businessClassSeatTicketCost: ''
  }
  airlines:airline[]=[]
  isNewFlight = false;
  header = '';
  dataSource: any;

  constructor(private readonly flighService:FlighService,
    private readonly route: ActivatedRoute,
    private snackbar: MatSnackBar,
    private router: Router
    ) { }

    ngOnInit(): void {
      this.flighService.getairlines()
      .subscribe(
        (successResponse) =>
        {
          this.airlines = successResponse;

        },
        (errorResponse) =>
        {
          console.log(errorResponse);
        }

    );
      this.route.paramMap.subscribe(
        (params) => {
          this.flightId = params.get('id');

          if (this.flightId) {
            if (this.flightId.toLowerCase() === 'Add'.toLowerCase()) {
              // -> new flight Functionality
              this.isNewFlight = true;
              this.header = 'Add New Flight';
            } else {
              // -> Existing flight Functionality
              this.isNewFlight = false;
              this.header = 'Edit Flight';
              this.flighService.getFlight(this.flightId)
                .subscribe(
                  (successResponse) => {
                    this.flight = successResponse;
                  }
                );
            }
          }
        }
      );

        //   this.flighService.getairlines()
        // .subscribe(
        //   (successResponse) => {
        //    this.airlines=successResponse;
        //   },
        //   (errorResponse) => {
        //     // Log
        //   }
        // );
    }
  onUpdate(): void {
    this.flighService.updateFlight(this.flight.flightId, this.flight)
      .subscribe(
        (successResponse) => {
          // Show a notification
          this.snackbar.open('Flight updated successfully', undefined, {
            duration: 2000
          });
          setTimeout(() => {
            this.router.navigateByUrl('flights');
          }, 2000);
        },
        (errorResponse) => {
          // Log it
        }
      );
  }
  onDelete(): void {
    //console.log(this.flight)
    this.flighService.deleteFlight(this.flight.flightId)
      .subscribe(
        (successResponse) => {

          this.snackbar.open('Flight-Schedule deleted successfully', undefined, {
            duration: 2000
          });

          setTimeout(() => {
             this.router.navigateByUrl('/flights');
           }, 2000);
        },
        (errorResponse) => {
          // Log
        }
      );
  }

  onAdd(): void {


    this.flighService.addFlight(this.flight)

      .subscribe(

        (successResponse) => {
          this.snackbar.open('Flight added successfully', undefined, {
            duration: 2000
          });

          setTimeout(() => {
            this.router.navigateByUrl(`getflight/${successResponse.flightId}`);
          }, 2000);

        },
        (errorResponse) => {
          // Log
        }
    );



  }


}
