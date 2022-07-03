using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Model
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        public string PassengerId { get; set; }
        public string MailId { get; set; }
        public string PNR { get; set; }
        public DateTime BookingDateTime { get; set; } = DateTime.Now;
        public int FlightNumber { get; set; }
        public int NumberofSeats { get; set; }
        public int TotalCost { get; set; }
        public string Status { get; set; } = "Active";
        public ICollection<Passenger> Passengers { get; set; }
    }
}
