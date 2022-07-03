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
    public class AirlineController : ControllerBase
    {
        private FlightServiceDbContext db;
        private IMapper mapper;

        public AirlineController(FlightServiceDbContext _db, IMapper _mapper)
        {
            db = _db;
            mapper = _mapper;
        }
        [HttpPost]
        public IActionResult AddAirline(AirlineDto airlinedto)
        {
            Airline airline = new Airline();
            airline = mapper.Map<Airline>(airlinedto);

            db.Airlines.Add(airline);
            db.SaveChanges();
            return Ok();
        }
        [HttpGet]
        public List<AirlineDto> GetAllAirline()
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

        [HttpDelete("{id}")]
        public ActionResult DeleteAirlinebyId(int id)
        {
            if (db.Airlines.Any(X => X.AirlineId == id))
            {
                var data = db.Airlines.Where(X => X.AirlineId == id).FirstOrDefault();
                db.Airlines.Remove(data);
                db.SaveChanges();
                return Ok("Airline Deleted");
            }
            return BadRequest("Airline not Exist");
        }
    }
}
