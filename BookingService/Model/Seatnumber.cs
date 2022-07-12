using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Model
{

    public class Seatnumber
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int SeatId { get; set; }
        public string SeatNumber { get; set; }
        public string Typeofseat { get; set; }
        public string Status { get; set; } = "Active";
        public int FlightId { get; set; }
        public Flight Flight { get; set; }
    }
}

