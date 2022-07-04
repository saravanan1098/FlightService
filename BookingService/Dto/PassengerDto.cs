using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Dto
{
    public class PassengerDto
    {
       
        public string PassengerName { get; set; }
        public int age { get; set; }
        public string Gender { get; set; }
        public string MealType { get; set; }
        public int SeatNumber { get; set; }
        
    }
}
