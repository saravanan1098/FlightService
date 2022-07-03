using AutoMapper;
using FlightService.Dto;
using FlightService.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        
        private FlightServiceDbContext db;
        private IMapper mapper;

        public FlightController(FlightServiceDbContext _db, IMapper _mapper)
        {
            db = _db;
            mapper = _mapper;
        }
        [HttpPost]
        public IActionResult AddFlight(flightDto flightdto)
        {
            Flight flight = new Flight();
            flight = mapper.Map<Flight>(flightdto);

            db.Flights.Add(flight);
            db.SaveChanges();
            return Ok();
        }
        [HttpGet]
        public List<flightDto> GetAllFlight()
        {
            var Flights = mapper.Map<List<flightDto>>(db.Flights);
            return Flights;
        }
        [HttpGet("{id}")]
        public ActionResult<flightDto> GetFlightbyId(int id)
        {

            var Flight = db.Flights.FirstOrDefault(a => a.FlightId == id);
            if (Flight != null)
            {
                var Flight1 = mapper.Map<flightDto>(Flight);

                return Flight1;
            }
            else
            {

                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteFlightbyId(int id)
        {
            if (db.Flights.Any(X => X.FlightId == id))
            {
                var data = db.Flights.Where(X => X.FlightId == id).FirstOrDefault();
                db.Flights.Remove(data);
                db.SaveChanges();
                return Ok("Flight Deleted");
            }
            return BadRequest("Flight not Exist");
        }
    }
}
    
