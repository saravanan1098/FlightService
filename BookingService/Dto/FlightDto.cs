using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Dto
{
    public class flightDto
    {
        public int FlightId { get; set; }
        public string FlightNumber { get; set; }
        public string FromPlace { get; set; }
        public string ToPlace { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string ScheduledDays { get; set; }
        public int BusinessClassSeats { get; set; }
        public int NonBusinessClassSeats { get; set; }
        public int NumberofRows { get; set; }
        public string MealType { get; set; }
        public string TypeofTrip { get; set; }
        public int BusinessClassSeatTicketCost { get; set; }
        public int NonBusinessClassSeatTicketCost { get; set; }
        public string Status { get; set; } = "Active";
        public int AirlineId { get; set; }
        public string AirlineName { get; set; }
        public string Logo { get; set; }
       // public ICollection<Seatnumber> Seatnumbers { get; set; }
    }
}
