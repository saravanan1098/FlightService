using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Dto
{
    public class FlightDto
    {
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
        public int BusinessClassSeatTicketCost { get; set; }
        public int NonBusinessClassSeatCost { get; set; }
    }
}
