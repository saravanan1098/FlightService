using AutoMapper;
using FlightService.Dto;
using FlightService.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolrNet.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {

        private readonly FlightServiceDbContext db;
        private readonly IMapper mapper;

        public FlightController(FlightServiceDbContext _db, IMapper _mapper)
        {
            db = _db;
            mapper = _mapper;
        }
        [HttpPost]
        public IActionResult AddFlight(FlightDto _flight)
        {

            Flight flight = mapper.Map<Flight>(_flight);

            db.Flights.Add(flight);
            db.SaveChanges();
            return Ok();
        }
        [HttpGet]
        public List<FlightDto> GetAllFlight()
        {
            var Flights = mapper.Map<List<FlightDto>>(db.Flights);
            return Flights;
        }
        [HttpGet("{id}")]
        public ActionResult<FlightDto> GetFlightbyId(int id)
        {

            Flight flight = db.Flights.FirstOrDefault(a => a.FlightId == id);
            if (flight != null)
            {
                var Flight1 = mapper.Map<FlightDto>(flight);

                return Flight1;
            }
            else
            {

                return NotFound();
            }
        }
        [HttpGet("block/{id}")]
        public ActionResult BlockFlightbyId(int id)
        {

            Flight flight = db.Flights.FirstOrDefault(a => a.FlightId == id);
            flight.Status = "Bloced";
            db.SaveChanges();
            return Ok("Blocked");
        }
        [HttpPut("{id}")]
        public ActionResult<Flight> UpdateFlightbyId(int id,FlightDto _flight)
        {
            var flight = mapper.Map<Flight>(_flight);
           var flight1 = db.Flights.FirstOrDefault(a => a.FlightId == id);
            if (flight1 != null)
            {
                flight1 = flight;
                    db.Flights.Update(flight1);
                    db.SaveChanges();
                    return flight;  
            }
             return NotFound();
        }
        [HttpGet("{fromplace},{toplace},{typeoftrip}")]
        public ActionResult<List<FlightDto>> Getflightsbydata( string fromplace, string toplace, string typeoftrip)
        {

            var flight = db.Flights.Where(x => x.FromPlace == fromplace).ToList();
            flight = db.Flights.Where(x => x.ToPlace == toplace).ToList();
            flight = db.Flights.Where(x => x.TypeofTrip == typeoftrip).ToList();
            //flight = db.Flights.Where(x => x.StartDateTime.Date == startdate).ToList();
            flight = db.Flights.Where(x => x.Status.Contains("Active")).ToList();
            if (flight != null)
            {
                var flight1 = mapper.Map<List<FlightDto>>(flight);
                return flight1;
            }
            return BadRequest("No Flight Available");

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
    
