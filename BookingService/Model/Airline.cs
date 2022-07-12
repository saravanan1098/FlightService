using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Model
{
    public class Airline
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AirlineId { get; set; }
        public string AirlineName { get; set; }
        public List<Flight> FLights { get; set; }
        public string Status { get; set; } = "Active";
        public string ContactAddress { get; set; }
        public string ContactNumber { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

    }
}
