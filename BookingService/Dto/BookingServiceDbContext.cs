using BookingService.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Dto
{
    public class BookingServiceDbContext:DbContext
    {
        public BookingServiceDbContext(DbContextOptions<BookingServiceDbContext> options):base(options)
        {

        }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
