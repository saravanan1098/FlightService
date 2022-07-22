using AutoMapper;
using BookingService.Dto;
using BookingService.Model;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles ="user")]
    public class BookingServiceController : ControllerBase
    {
        private readonly BookingDbContext db;
        private readonly IMapper mapper;
        private readonly IConverter _converter;
        public BookingServiceController(BookingDbContext _db, IMapper _mapper, IConverter converter)
        {
            db = _db;
            mapper = _mapper;
            _converter = converter;
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
        public async Task<ActionResult<Booking>> AddBooking(BookingDto bookingdto)
        {

            //Booking booking = mapper.Map<Booking>(_booking);

            //await db.Bookings.AddAsync(booking);
            //db.SaveChanges();
            //return Ok();
            Booking Booking = new Booking();
            Booking.BookingName = bookingdto.BookingName;
            Booking.FlightNumber = bookingdto.FlightNumber;
            Booking.NumberofSeats = bookingdto.PassengerDtos.Count();
            Booking.MailId = bookingdto.MailId;
            //Booking.TotalCost = (bookingdto.PassengerDtos.Count() * flight.NonBusinessClassSeatTicketCost) - discount.Discountamount;

            db.Bookings.Add(Booking);
            await db.SaveChangesAsync();

            foreach (var PassengerDto in bookingdto.PassengerDtos)
            {
                Passenger passenger = new Passenger();
                passenger.Gender = PassengerDto.Gender;
                passenger.age = PassengerDto.age;
                //passenger.SeatNumber = PassengerDto.SeatNumber;
                passenger.BookingId = Booking.BookingId;
                passenger.MealType = PassengerDto.MealType;
                passenger.Name = PassengerDto.Name;

                //Seatnumber seatnumber=await _db.Seatnumbers.Where(i=>i.FlightId==flight.FlightId && i.SeatNumber==PassengerDto.SeatNumber).FirstOrDefaultAsync();
                //seatnumber.Status = "Booked";
                //_db.Seatnumbers.Update(seatnumber);
                //await _db.SaveChangesAsync();

                db.Passengers.Add(passenger);
                await db.SaveChangesAsync();
            }
            return Booking;
        }
        [HttpGet("allbookings")]
        public async Task<ActionResult<IEnumerable<Booking>>> allBooking()
        {
            return await db.Bookings.ToListAsync();
        }

        [HttpGet ("cancel/{pnr}")]
        public async Task<bool> CancelBooking(int pnr)
        {

            try
            {
                Booking Booking = await db.Bookings.Where(x => x.PNR == pnr).FirstOrDefaultAsync();

                if (Booking == null)
                    return false;
                //if((Booking.BookingDateTime-DateTime.Now).TotalHours<24)
                //    return false;

                Booking.Status = "Cancelled";

                db.Bookings.Update(Booking);

                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
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
        public async Task<ActionResult<Booking>> GetBookingByPnr([FromRoute] int pnr)
        {

            Booking booking = await db .Bookings.Where(x => x.PNR == pnr).FirstOrDefaultAsync();
            if (booking != null)
            {
                return booking;
            }
            return BadRequest("No booking found");
        }
        [HttpGet("mail/{mailId}")]
        public async Task<ActionResult<List<Booking>>> GetBookingByMailId([FromRoute] string mailId)
        {

            List<Booking> booking =await db.Bookings.Where(x => x.MailId == mailId).ToListAsync();
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

        [HttpGet("generatepdf/pnr/{pnr}")]
        public IActionResult GeneratePDF([FromRoute] int pnr)
        {
            var bookingmodel = db.Bookings.Where(x => x.PNR == pnr).Include(i => i.Passengers).FirstOrDefault();
            var flightmodel = db.Flights.Where(x => x.FlightNumber == bookingmodel.FlightNumber).FirstOrDefault();

            var sb = new StringBuilder();
            sb.Append(@"
                                <div class='header'><h1>Passenger Details</h1></div>
                                <table align='center'>
                                    <tr>
                                        <th>Name</th>                                      
                                        <th>Age</th>
                                        <th>Gender</th>
                                        <th>Mealtype</th>
                                    </tr>");
            foreach (Passenger passenger in bookingmodel.Passengers)
            {
                sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                  </tr>", passenger.Name, passenger.age, passenger.Gender, passenger.MealType);
            }
            sb.Append(@"
                                </table>
                          ");
            var html1 = sb.ToString();

            var html = $@"
           <!DOCTYPE html>
           <html lang=""en"">
           <head>
             <h1 class='header'>Booking Details</h1>  
           </head>
          <body class='body'>
          <p><b>PNR</b> : {bookingmodel.PNR}</p>
          <p><b>Flight Number</b> : {bookingmodel.FlightNumber}</p>
          <p><b>From Place</b> : {flightmodel.FromPlace}</p>
          <p><b>To Place</b> : {flightmodel.ToPlace}</p>
          <p><b>Departure Time</b> : {flightmodel.StartDateTime}</p>
          <p><b>Status</b> : {bookingmodel.Status}</p>
          <p><b>No. Of Seats</b> : {bookingmodel.NumberofSeats}</p>
          <p><b>Booking Datetime</b> : {bookingmodel.BookingDateTime}</p>
              <p>{html1}</p>
          </body>
          </html>
          ";
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = html,
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            var pdfFile = _converter.Convert(pdf);
            var pdfname = bookingmodel.PNR + ".pdf";
            return File(pdfFile,
            "application/octet-stream", pdfname);
        }




    }


}


