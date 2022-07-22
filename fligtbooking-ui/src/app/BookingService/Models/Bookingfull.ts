import { passenger } from "./Passenger";

export interface booking{

  bookingId: string,
  bookingName: string,
  mailId: string,
  pnr: string,
  bookingDateTime: string,
  flightNumber: string,
  numberofSeats: string,
  totalCost: string,
  status: string,
  passengers:passenger[]

}
