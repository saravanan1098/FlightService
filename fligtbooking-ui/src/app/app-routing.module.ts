import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './AuthenticationService/login/login.component';
import { RegisterComponent } from './AuthenticationService/register/register.component';
import { BookinghistoryComponent } from './BookingService/bookinghistory/bookinghistory.component';
import { BookticketComponent } from './BookingService/bookticket/bookticket.component';
import { ListflightsComponent } from './BookingService/listflights/listflights.component';
import { PnrsearchComponent } from './BookingService/pnrsearch/pnrsearch/pnrsearch.component';
import { AirlinesComponent } from './FlightService/airlines/airlines/airlines.component';
import { ViewAirlineComponent } from './FlightService/airlines/view-airline/view-airline.component';
import { FlightComponent } from './FlightService/flight/flight.component';
import { ViewFlightComponent } from './FlightService/view-flight/view-flight.component';
import { TopNavComponent } from './layout/top-nav/top-nav.component';

const routes: Routes = [
  {
    path:'',
    component:ListflightsComponent
  },

  {
    path:'flights',
    component:FlightComponent
  },
  {
    path:'getflight/:id',
    component:ViewFlightComponent
  },
  {
    path: 'airlines',
    component:AirlinesComponent
  },
       {
    path: 'getairline/:id',
    component:ViewAirlineComponent
  },
  {
    path: 'bookticket/:id',
    component:BookticketComponent
  },
  {
    path: 'searchall',
    component:ListflightsComponent
  },
  {
    path: 'register',
    component:RegisterComponent
  },
  {
    path: 'login',
    component:LoginComponent
  },
  {
    path: 'bookinghistory',
    component:BookinghistoryComponent
  },
  {
    path: 'pnrsearch',
    component:PnrsearchComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
