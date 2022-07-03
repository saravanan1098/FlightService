using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Model
{
    public class Passenger
    {
        [Key]
        public int PassengerId { get; set; }
        public string PassengerName { get; set; }
        public int age { get; set; }
        public string Gender { get; set; }
        public string MealType { get; set; }
        public int SeatNumber { get; set; }
        public int BookingId { get; set; }
        public Booking Booking { get; set; }

    }
}
