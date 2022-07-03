using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Model
{
    public class Airline
    {   [Key]
        public int AirlineId { get; set; }
        public string AirlineName { get; set; }
        public List<Flight> FLights { get; set; }
        public string Status { get; set; } = "Active";
        public string ContactAddress { get; set; }
        public string ContactNumber { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

    }
}
