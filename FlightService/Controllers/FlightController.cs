using AutoMapper;
using common;
using FlightService.Dto;
using FlightService.Model;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolrNet.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Controllers
{
    //[Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {

        private readonly FlightServiceDbContext db;
        private readonly IMapper mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IBusControl _bus;

        public FlightController(FlightServiceDbContext _db, IMapper _mapper, IPublishEndpoint publishEndpoint, IBusControl busControl)
        {
            db = _db;
            mapper = _mapper;
            _publishEndpoint = publishEndpoint;
            _bus = busControl;
        }
        [Authorize(Roles = "admin")]
        [HttpPost("add")]
        public async Task<IActionResult> AddFlight(FlightDto _flight)
        {
            Flight flight = new Flight();
            flight = mapper.Map<Flight>(_flight);

            db.Flights.Add(flight);
            await db.SaveChangesAsync();
            await _publishEndpoint.Publish<Flight_Shared>(flight);
            Uri uri = new Uri("rabbitmq://localhost/FlightQueue");
            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(flight);
            return Ok();
        }
        [HttpGet]
        public List<Flight> GetAllFlight()
        {
            var Flights = mapper.Map<List<Flight>>(db.Flights);
            return Flights;
        }
        [HttpGet("{id}")]
        public ActionResult<Flight> GetFlightbyId(int id)
        {

            return  db.Flights.FirstOrDefault(a => a.FlightId == id);
            //if (flight != null)
            //{
            //    var Flight1 = mapper.Map<Flight>(flight);

            //    return Flight1;
            //}
            //else
            //{

            //    return NotFound();
            //}
        }
        [Authorize(Roles = "admin")]
        [HttpGet("block/{id}")]
        public ActionResult BlockFlightbyId(int id)
        {

            Flight flight = db.Flights.FirstOrDefault(a => a.FlightId == id);
            flight.Status = "Blocked";
            db.SaveChanges();
            return Ok("Blocked");
        }
        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Flight>> UpdateFlightbyId(int id,FlightDto flightDto)
        {
            
           var flight1 = db.Flights.FirstOrDefault(a => a.FlightId == id);
            if (flight1 != null)
            {
                Flight flight = await db.Flights.Where(x => x.FlightId == id).FirstOrDefaultAsync();

                flight.AirlineId = flightDto.AirlineId;
                flight.FlightNumber = flightDto.FlightNumber;
                flight.FromPlace = flightDto.FromPlace;
                flight.ToPlace = flightDto.ToPlace;
                flight.StartDateTime = flightDto.StartDateTime;
                flight.EndDateTime = flightDto.EndDateTime;
                flight.ScheduledDays = flightDto.ScheduledDays;   
                flight.BusinessClassSeats = flightDto.BusinessClassSeats;
                flight.NonBusinessClassSeats = flightDto.NonBusinessClassSeats;
                flight.MealType = flightDto.MealType;
                flight.TypeofTrip = flightDto.TypeofTrip;
                flight.BusinessClassSeatTicketCost = flightDto.BusinessClassSeatTicketCost;
                flight.NonBusinessClassSeatTicketCost = flightDto.NonBusinessClassSeatTicketCost;

                db.Flights.Update(flight);
                await db.SaveChangesAsync();

                await _publishEndpoint.Publish<Flight_Shared>(flight);
                Uri uri = new Uri("rabbitmq://localhost/FlightQueue");
                var endPoint = await _bus.GetSendEndpoint(uri);
                await endPoint.Send(flight);


                return flight;
            }
             return NotFound();
        }
        //[HttpGet("{fromplace},{toplace},{typeoftrip}")]
        //public ActionResult<List<FlightDto>> Getflightsbydata( string fromplace, string toplace, string typeoftrip)
        //{

        //    var flight = db.Flights.Where(x => x.FromPlace == fromplace).ToList();
        //    flight = db.Flights.Where(x => x.ToPlace == toplace).ToList();
        //    flight = db.Flights.Where(x => x.TypeofTrip == typeoftrip).ToList();
        //    //flight = db.Flights.Where(x => x.StartDateTime.Date == startdate).ToList();
        //    flight = db.Flights.Where(x => x.Status.Contains("Active")).ToList();
        //    if (flight != null)
        //    {
        //        var flight1 = mapper.Map<List<FlightDto>>(flight);
        //        return flight1;
        //    }
        //    return BadRequest("No Flight Available");

        //}
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteFlightbyId(int id)
        {
            if (db.Flights.Any(X => X.FlightId == id))
            {
                var data = db.Flights.Where(X => X.FlightId == id).FirstOrDefault();
                db.Flights.Remove(data);
                db.SaveChanges();

                Id transferid = new Id();
                transferid.TransferId = id;

                await _publishEndpoint.Publish<Id>(transferid);
                Uri uri = new Uri("rabbitmq://localhost/FlightdeleteQueue");
                var endPoint = await _bus.GetSendEndpoint(uri);
                await endPoint.Send(transferid);

                return true;
            }
            return false;
        }
    }
}
    
