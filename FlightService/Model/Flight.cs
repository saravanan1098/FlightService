using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Model
{
    public class Flight
    {
        [Key]
        public int FlightId { get; set; }
        public string FlightModel { get; set; }
        public string FromPlace { get; set; }
        public string ToPlace { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string ScheduledDays { get; set; }
        public int BusinessClassSeats { get; set; }
        public int NonBusinessClassSeats { get; set; }
        public string MealType { get; set; }
        public string TypeofTrip { get; set; }
        public int OnewayTicketCost { get; set; }
        public int RoundTripTicketCost { get; set; }
        public string Status { get; set; } = "Active";
        public int AirlineId { get; set; }
        public Airline Airline { get; set; }
        public ICollection<Seatnumber> Seatnumbers { get; set; }
    }
}
