using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Dto
{
    public class BookingDto
    {
        public string BookingName { get; set; }
        public string MailId { get; set; }
        public string FlightNumber { get; set; }
        public int NumberofSeats { get; set; }
        public ICollection<PassengerDto> PassengerDtos { get; set; }
    }
}
