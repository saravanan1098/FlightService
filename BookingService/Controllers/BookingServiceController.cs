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
    [Authorize(Roles ="user")]
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
