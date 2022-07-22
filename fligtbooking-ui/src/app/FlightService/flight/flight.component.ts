import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { flight } from '../../models/ui-model/flight';
import { FlighService } from '../fligh.service';

@Component({
  selector: 'app-flight',
  templateUrl: './flight.component.html',
  styleUrls: ['./flight.component.css']
})
export class FlightComponent implements OnInit {
  flight: flight[] =[];
  displayedColumns: string[]=['flightNumber', 'fromPlace', 'toPlace', 'startDateTime',
   'endDateTime','businessClassSeats','nonBusinessClassSeats','edit'
  ];
  dataSource:MatTableDataSource<flight> = new MatTableDataSource<flight>();
  @ViewChild(MatPaginator) matPaginator!: MatPaginator;
  @ViewChild(MatSort) matSort!: MatSort;
  filterString='';
  constructor(private flightService:FlighService)  { }

  ngOnInit(): void {
    this.flightService.getFlights()
    .subscribe(
      (successResponse) => {
        this.flight = successResponse;
        this.dataSource= new MatTableDataSource<flight>(this.flight)
        if(this.matPaginator)
        {this.dataSource.paginator =this.matPaginator;
        }
        if(this.matSort){
          this.dataSource.sort=this.matSort;
        }

      },
        (errorResponse) =>{
          console.log(errorResponse);
        }

    );
  }
  filterFlights() {
    this.dataSource.filter = this.filterString.trim().toLowerCase();
  }
}
