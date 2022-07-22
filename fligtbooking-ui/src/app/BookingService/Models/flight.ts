import { seatnumber } from "./seatnumber"

export interface flight{
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
    seatnumbers:seatnumber[]
}
