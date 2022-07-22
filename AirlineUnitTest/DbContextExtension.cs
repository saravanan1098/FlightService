using FlightService.Dto;
using FlightService.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirlineUnitTest
{
    public static class DbContextExtension
    {
        public static void Seed(this FlightServiceDbContext db)
        {
            db.Airlines.Add(new FlightService.Model.Airline
            {
                AirlineName = "Dubai Airlines",
                ContactNumber = "989772878",
                ContactAddress = "Dubai",
                
            });

            db.Airlines.Add(new FlightService.Model.Airline
            {
                AirlineName = "Singapore Airlines",
                ContactNumber = "328782928",
                ContactAddress = "Singapore",

            });

          
        }
    }
}
