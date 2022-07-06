using AutoMapper;
using BookingService.Dto;
using BookingService.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingServiceController : ControllerBase
    {
        public BookingServiceDbContext db;
        public IMapper mapper;
        public BookingServiceController(BookingServiceDbContext _db, IMapper _mapper)
        {
            db = _db;
            mapper = _mapper;
        }
        [HttpPost]
        public IActionResult AddBooking(BookingDto _booking)
        {

            Booking booking = mapper.Map<Booking>(_booking);

            db.Bookings.Add(booking);
            db.SaveChanges();
            return Ok();
        }
        [HttpGet]
        public List<Booking> GetallBooking()
        {
            return db.Bookings.ToList();
        }
        [HttpGet("{id}")]
        public ActionResult<Booking> GetBooking(int id)
        {

            Booking booking = db.Bookings.Where(x => x.BookingId == id).FirstOrDefault();
           if (booking != null)
            {
               return booking;
            }
            return BadRequest("No booking found");
        }
        [HttpGet("pnr/{pnr}")]
        public ActionResult<Booking> GetBookingByPnr(int pnr)
        {

            Booking booking = db.Bookings.Where(x => x.PNR == pnr).FirstOrDefault();
            if (booking != null)
            {
                return booking;
            }
            return BadRequest("No booking found");
        }
        [HttpGet("mail/{mailId}")]
        public ActionResult<Booking> GetBookingByMailId(string mailId)
        {

            Booking booking = db.Bookings.Where(x => x.MailId == mailId).FirstOrDefault();
            if (booking != null)
            {
                return booking;
            }
            return BadRequest("No booking found");
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteBooking(int id)
        {
            if(db.Bookings.Any(x=>x.BookingId==id))
            {
                var data = db.Bookings.Where(x => x.BookingId == id).FirstOrDefault();
                db.Bookings.Remove(data);
                db.SaveChanges();
            }
            return BadRequest("Booking Id not Found");
        }


    }

}
