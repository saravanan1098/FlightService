using FlightService.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Dto
{
    public class FlightServiceDbContext:DbContext
    {
        public FlightServiceDbContext(DbContextOptions<FlightServiceDbContext> options):base(options)
        {

        }
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Seatnumber> Seatnumbers { get; set; }
    }
}
