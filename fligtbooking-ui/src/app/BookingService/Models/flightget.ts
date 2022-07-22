import { seatnumber } from "./seatnumber"

export interface flightget{
    flightId:string,
    airlineId:string,
    flightNumber: string,
    fromPlace: string,
    toPlace: string,
    startDateTime: string,
    endDateTime: string,
    scheduledDays: string,
    businessClassSeats: string,
    nonBusinessClassSeats: string,
    mealType: string,
    typeofTrip: string,
    businessClassSeatTicketCost:string,
    nonBusinessClassSeatTicketCost: string,
    airlineName: string,
    logo:string,
    seatnumbers:seatnumber[]
}
