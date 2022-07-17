using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace common
{
    public class Airline_Shared
    {
        [Key]
        public int AirlineId { get; set; }
        public string AirlineName { get; set; }
        //public List<Flight> FLights { get; set; }
        public string Logo { get; set; }
        public string Status { get; set; } = "Active";
        public string ContactAddress { get; set; }
        public string ContactNumber { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public DateTime LastUpdatedDateTime { get; set; } = DateTime.Now;
    }
}
