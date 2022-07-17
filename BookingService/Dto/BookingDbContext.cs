using BookingService.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Dto
{
    public class BookingDbContext:DbContext
    {
        public BookingDbContext(DbContextOptions<BookingDbContext> options):base(options)
        {

        }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Seatnumber> Seatnumbers { get; set; }
        //public DbSet<Discount> Discounts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Passenger>().HasOne<Booking>(n => n.Booking)
                .WithMany(s => s.Passengers)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Booking>().ToTable("Bookings");
            modelBuilder.Entity<Passenger>().ToTable("Passengers");
            base.OnModelCreating(modelBuilder);
        }

    }
}
