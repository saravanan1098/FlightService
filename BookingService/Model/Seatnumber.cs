using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Model
{

    public class Seatnumber
    {
        [Key]
        public int SearchId { get; set; }
        public int SeatId { get; set; }
        public string SeatNumber { get; set; }
        public string Typeofseat { get; set; }
        public string Status { get; set; } = "Active";
        public int FlightId { get; set; }
        public Flight Flight { get; set; }
    }
}

