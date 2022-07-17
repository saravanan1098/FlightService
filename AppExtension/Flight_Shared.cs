using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace common
{
    public class Flight_Shared
    {
        [Key]
        public int FlightId { get; set; }
        public string FlightNumber { get; set; }
        public string FromPlace { get; set; }
        public string ToPlace { get; set; }
        public DateTime StartDateTime { get; set; } = DateTime.Now;
        public DateTime EndDateTime { get; set; } = DateTime.Now;
        public string ScheduledDays { get; set; }
        public int BusinessClassSeats { get; set; }
        public int NonBusinessClassSeats { get; set; }
        public string MealType { get; set; }
        public string TypeofTrip { get; set; }
        public int BusinessClassSeatTicketCost { get; set; }
        public int NonBusinessClassSeatTicketCost { get; set; }
        public string Status { get; set; } = "Active";
        public int AirlineId { get; set; }
        //public Airline Airline { get; set; }
        //public ICollection<Seatnumber> Seatnumbers { get; set; }
    }
}
