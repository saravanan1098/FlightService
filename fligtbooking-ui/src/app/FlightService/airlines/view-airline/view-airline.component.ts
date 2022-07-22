import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { FlighService } from '../../fligh.service';
import { airline } from '../../../models/ui-model/airline';

@Component({
  selector: 'app-view-airline',
  templateUrl: './view-airline.component.html',
  styleUrls: ['./view-airline.component.css']
})
export class ViewAirlineComponent implements OnInit {

 airlineId: string | null | undefined;
  airline: airline = {
    airlineId: '',
    airlineName: '',
    logo: '',
    contactAddress: '',
    contactNumber: '',
    status: ''
  }

  isNewAirline = false;
  header = '';
  displayProfileImageUrl= "";



  constructor(
    private readonly flighService: FlighService,
    private readonly route: ActivatedRoute,
    private snackbar: MatSnackBar,
    private router: Router
  ) { }


  ngOnInit(): void {
    this.route.paramMap.subscribe(
      (params) => {
        this.airlineId = params.get('id');

        if (this.airlineId) {
          if (this.airlineId.toLowerCase() === 'Add'.toLowerCase()) {
            // -> new airline Functionality
            this.isNewAirline = true;
            this.header = 'Add Airline';
            this.setImage();
          } else {
            // -> Existing airline Functionality
            this.isNewAirline = false;
            this.header = 'Edit Airline';
            this.flighService.getairline(this.airlineId)
              .subscribe(
                (successResponse) => {
                  this.airline = successResponse;
                  this.setImage();
                },
                  (errorResponse) => {
                  this.setImage();
                }
              );
          }
        }
      }
    );
  }

  onUpdate(): void {
    this.flighService.updateairline(this.airline.airlineId, this.airline)
      .subscribe(
        (successResponse) => {
          // Show a notification
          this.snackbar.open('airline updated successfully', undefined, {
            duration: 2000
          });
        },
        (errorResponse) => {
          // Log it
        }
      );
  }
  onDelete(): void {
    this.flighService.deleteairline(this.airline.airlineId)
      .subscribe(
        (successResponse) => {
          this.snackbar.open('airline-Schedule deleted successfully', undefined, {
            duration: 2000
          });

          setTimeout(() => {
            this.router.navigateByUrl('airlines');
          }, 2000);
        },
        (errorResponse) => {
          // Log
        }
      );
  }

  onAdd(): void {
    this.flighService.addairline(this.airline)
      .subscribe(
        (successResponse) => {
          this.snackbar.open('airline added successfully', undefined, {
            duration: 2000
          });

          setTimeout(() => {
            this.router.navigateByUrl(`getairline/${successResponse.airlineId}`);
          }, 2000);

        },
        (errorResponse) => {
          // Log
        }
      );

  }

    uploadImage(event: any): void {
    if (this.airlineId) {
      const file: File = event.target.files[0];
      this.flighService.uploadImage(this.airline.airlineId, file)
        .subscribe(
          (successResponse) => {
            this.airline.logo = successResponse;
            this.setImage();

            // Show a notification
            this.snackbar.open('Profile Image Updated', undefined, {
              duration: 2000
            });

          },
          (errorResponse) => {

          }
        );

    }

  }

  private setImage(): void {
    if (this.airline.logo) {
     this.displayProfileImageUrl = this.flighService.getImagePath(this.airline.logo);
    } else {
      // Display a default
     this.displayProfileImageUrl = '/assets/flight.png';
    }
  }



}
