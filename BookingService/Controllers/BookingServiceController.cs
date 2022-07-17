using AutoMapper;
using BookingService.Dto;
using BookingService.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles ="user")]
    public class BookingServiceController : ControllerBase
    {
        public BookingDbContext db;
        public IMapper mapper;
        public BookingServiceController(BookingDbContext _db, IMapper _mapper)
        {
            db = _db;
            mapper = _mapper;
        }
        [HttpGet("searchall")]
        public async Task<ActionResult<IEnumerable<flightDto>>> Getall()
        {
            try
            {
                //ICollection<Flight> flight = await db.Flights.Where(i => i.Status.Contains("Active")).Include(i => i.Seatnumbers.Where(j => j.Status.Contains("Active"))).ToListAsync();
                ICollection<Flight> flight = await db.Flights.Where(i => i.Status.Contains("Active")).ToListAsync();
                var flightdtos = mapper.Map<ICollection<flightDto>>(flight);
                foreach (var flightdto in flightdtos)
                {
                    Airline airline = db.Airlines.Where(i => i.AirlineId == flightdto.AirlineId).FirstOrDefault();
                    flightdto.AirlineName = airline.AirlineName;
                    flightdto.Logo = airline.Logo;
                }
                return flightdtos.ToList();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Internal DB Error");
            }

        }

        [HttpGet("getflight/{id}")]
        public async Task<ActionResult<flightDto>> Getflight([FromRoute] int id)
        {
            try
            {
                //var flight = await db.Flights.Where(i => i.FlightId == id).Include(i => i.Seatnumbers).FirstOrDefaultAsync();
                var flight = await db.Flights.Where(i => i.FlightId == id).FirstOrDefaultAsync();
                var flightdto = mapper.Map<flightDto>(flight);

                Airline airline = db.Airlines.Where(i => i.AirlineId == flightdto.AirlineId).FirstOrDefault();
                flightdto.AirlineName = airline.AirlineName;
                flightdto.Logo = airline.Logo;

                return flightdto;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Internal DB Error");
            }

        }
        [HttpPost("bookticket")]
        public async Task<IActionResult> AddBooking(BookingDto _booking)
        {

            Booking booking = mapper.Map<Booking>(_booking);

            await db.Bookings.AddAsync(booking);
            db.SaveChanges();
            return Ok();
        }
        //[HttpGet]
        //public List<Booking> GetallBooking()
        //{
        //    return db.Bookings.ToList();
        //}
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {

            Booking booking = await db.Bookings.Where(x => x.BookingId == id).FirstOrDefaultAsync();
           if (booking != null)
            {
               return booking;
            }
            return BadRequest("No booking found");
        }
        [HttpGet("pnr/{pnr}")]
        public async Task<ActionResult<Booking>> GetBookingByPnr(int pnr)
        {

            Booking booking = await db .Bookings.Where(x => x.PNR == pnr).FirstOrDefaultAsync();
            if (booking != null)
            {
                return booking;
            }
            return BadRequest("No booking found");
        }
        [HttpGet("mail/{mailId}")]
        public async Task<ActionResult<Booking>> GetBookingByMailId(string mailId)
        {

            Booking booking =await db.Bookings.Where(x => x.MailId == mailId).FirstOrDefaultAsync();
            if (booking != null)
            {
                return booking;
            }
            return BadRequest("No booking found");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBooking(int id)
        {
            if(db.Bookings.Any(x=>x.BookingId==id))
            {
                var data = await db.Bookings.Where(x => x.BookingId == id).FirstOrDefaultAsync ();
                db.Bookings.Remove(data);
                await db.SaveChangesAsync();
            }
            return BadRequest("Booking Id not Found");
        }


    }

}
