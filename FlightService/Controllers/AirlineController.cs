using AutoMapper;
using FlightService.Dto;
using FlightService.Model;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Controllers
{
    [Authorize(Roles ="admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AirlineController : ControllerBase
    {
        private readonly FlightServiceDbContext db;
        private readonly IMapper mapper;
        private readonly IBusControl bus;
        private readonly IPublishEndpoint publishEndpoint;

        public AirlineController(FlightServiceDbContext _db, IMapper _mapper, IBusControl _bus, IPublishEndpoint _publishEndpoint)
        {
            db = _db;
            mapper = _mapper;
            bus = _bus;
            publishEndpoint = _publishEndpoint;
        }
        [HttpPost]
        public async Task<IActionResult> AddAirline(AirlineDto airlinedto)
        {
            Airline airline = new Airline();
            airline = mapper.Map<Airline>(airlinedto);

            db.Airlines.Add(airline);
            await db.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        public async Task<List<AirlineDto>> GetAllAirline()
        {
            var airlines = mapper.Map<List<AirlineDto>>(db.Airlines);
            return airlines;
        }
        [HttpGet("{id}")]
        public ActionResult<AirlineDto> GetAirlinebyId(int id)
        {

            var airline = db.Airlines.FirstOrDefault(a => a.AirlineId == id);
            if (airline != null)
            {
                var airline1 = mapper.Map<AirlineDto>(airline);

                return airline1;
            }
            else
            {

                return NotFound();
            }
        }
        [HttpGet("block/{id}")]
        public async Task<ActionResult> BlockFlightbyId(int id)
        {

            Airline airline = db.Airlines.FirstOrDefault(a => a.AirlineId == id);
            airline.Status = "Blocked";
            await db.SaveChangesAsync();
            return Ok("Blocked");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAirlinebyId(int id)
        {
            if (db.Airlines.Any(X => X.AirlineId == id))
            {
                var data =await db.Airlines.Where(X => X.AirlineId == id).FirstOrDefaultAsync();
                db.Airlines.Remove(data);
                await db.SaveChangesAsync();
                return Ok("Airline Deleted");
            }
            return BadRequest("Airline not Exist");
        }
    }
}
