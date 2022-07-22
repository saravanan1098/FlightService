import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { FlighService } from '../../fligh.service';
import { airline } from '../../../models/ui-model/airline';

@Component({
  selector: 'app-airlines',
  templateUrl: './airlines.component.html',
  styleUrls: ['./airlines.component.css']
})
export class AirlinesComponent implements OnInit {

  airline: airline[] = [];
  displayProfileImageUrl = '';
  displayDefaultimage = '/assets/flight.png';

  displayedColumns: string[] = ['logo','airlineName','contactAddress','contactNumber','status', 'edit','block/unblock'];
  dataSource: MatTableDataSource<airline> = new MatTableDataSource<airline>();
  @ViewChild(MatPaginator) matPaginator!: MatPaginator;
  @ViewChild(MatSort) matSort!: MatSort;
  filterString = '';

  constructor(private flighservice: FlighService,
    private readonly route: ActivatedRoute,
    private snackbar: MatSnackBar,
    private router: Router) { }

  ngOnInit(): void {

    this.flighservice.getairlines()
      .subscribe(
        (successResponse) =>
        {
          this.airline = successResponse;
          this.dataSource = new MatTableDataSource<airline>(this.airline);
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
  filterAirlines() {
    this.dataSource.filter = this.filterString.trim().toLowerCase();
  }


  onBlock(value:string): void {
        this.flighservice.blockairline(value)
          .subscribe(
            (successResponse) => {
              console.log(successResponse)
              console.log(value)
              this.snackbar.open('Airline Blocked successfully', undefined, {
                duration: 2000
              });

              this.ngOnInit();
              //window.location.reload();

            },
            (errorResponse) => {
                this.snackbar.open('Blocking failed', undefined, {
                duration: 2000
              });
            }
          );
  };

    onUnblock(value:string): void {
        this.flighservice.unblockairline(value)
          .subscribe(
            (successResponse) => {
              console.log(successResponse)
              console.log(value)
              this.snackbar.open('Airline Unblocked successfully', undefined, {
                duration: 2000
              });
              this.ngOnInit();
                //window.location.reload();

            },
            (errorResponse) => {
                this.snackbar.open('Unblocking failed', undefined, {
                duration: 2000
              });
            }
          );
  };


}
