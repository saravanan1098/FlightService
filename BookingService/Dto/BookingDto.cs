using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Dto
{
    public class BookingDto
    {
        public string MailId { get; set; }
        public int FlightNumber { get; set; }
        public int NumberofSeats { get; set; }
        public ICollection<PassengerDto> PassengerDtos { get; set; }
    }
}
