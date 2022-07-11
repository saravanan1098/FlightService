using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace common
{
    public class Seatnumber_Shared
    {
        [Key]
        public int SeatId { get; set; }
        public string SeatNumber { get; set; }
        public string Typeofseat { get; set; }
        public string Status { get; set; } = "Active";
        public int FlightId { get; set; }
        //public Flight Flight { get; set; }
    }
}
