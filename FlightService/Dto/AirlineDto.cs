
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Dto
{
    public class AirlineDto
    {
        public string AirlineName { get; set; }
        public string ContactAddress { get; set; }
        public string ContactNumber { get; set; }

    }
}
