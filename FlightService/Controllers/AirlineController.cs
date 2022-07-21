using AutoMapper;
using common;
using FlightService.Dto;
using FlightService.Model;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
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

        [HttpPost("add")]
        public async Task<IActionResult> AddAirline(AirlineDto airlinedto)
        {
            Airline airline = new Airline();
            airline = mapper.Map<Airline>(airlinedto);
            airline.Logo = "";
            db.Airlines.Add(airline);
            await db.SaveChangesAsync();
            await publishEndpoint.Publish<Airline_Shared>(airline);
            Uri uri = new Uri("rabbitmq://localhost/AirlineQueue");
            var endPoint = await bus.GetSendEndpoint(uri);
            await endPoint.Send(airline);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Airline>> UpdateAirline([FromRoute] int id, AirlineDto airlinedto)
        {
            try
            {
                Airline airline = await db.Airlines.Where(x => x.AirlineId == id).FirstOrDefaultAsync();

                airline.AirlineName = airlinedto.AirlineName;
                airline.ContactAddress = airlinedto.ContactAddress;
                airline.ContactNumber = airlinedto.ContactNumber;

                db.Airlines.Update(airline);
                await db.SaveChangesAsync();

                await publishEndpoint.Publish<Airline_Shared>(airline);
                Uri uri = new Uri("rabbitmq://localhost/AirlineQueue");
                var endPoint = await bus.GetSendEndpoint(uri);
                await endPoint.Send(airline);

                return airline;


            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Internal DB Error");
            }
        }

        [HttpGet]
        public async Task<IEnumerable<Airline>> GetAllAirline()
        {
            //var airlines = mapper.Map<List<AirlineDto>>(db.Airlines);
            return await db.Airlines.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<Airline> GetAirlinebyId(int id)
        {

            return await db.Airlines.FirstOrDefaultAsync(a => a.AirlineId == id);
     
        }
        [HttpGet("block/{id}")]
        public async Task<bool> BlockFlightbyId(int id)
        {
            Airline airline = await db.Airlines.Where(x => x.AirlineId == id).Include(i => i.Flights).FirstOrDefaultAsync();
            
            airline.Status = "Blocked";
            foreach (Flight flight in airline.Flights)
            {
                flight.Status = "Blocked";
                db.Flights.Update(flight);
                await db.SaveChangesAsync();

            }
            db.Airlines.Update(airline);
            await db.SaveChangesAsync();


            Airline_block_id airline_Block_Id = new Airline_block_id();
            airline_Block_Id.AirlineId = id;

            await publishEndpoint.Publish<Airline_block_id>(airline_Block_Id);
            Uri uri = new Uri("rabbitmq://localhost/AirlineblockQueue");
            var endPoint = await bus.GetSendEndpoint(uri);
            await endPoint.Send(airline_Block_Id);
            return true;
        }
        [HttpGet("unblock/{id}")]
        public async Task<bool>UnBlockFlightbyId(int id)
        {

            Airline airline = await db.Airlines.Where(x => x.AirlineId == id).Include(i => i.Flights).FirstOrDefaultAsync();

            airline.Status = "Active";
            foreach (Flight flight in airline.Flights)
            {
                flight.Status = "Active";
                db.Flights.Update(flight);
                await db.SaveChangesAsync();

            }
            db.Airlines.Update(airline);
            await db.SaveChangesAsync();



            Airline_block_id airline_Block_Id = new Airline_block_id();
            airline_Block_Id.AirlineId = id;

            await publishEndpoint.Publish<Airline_block_id>(airline_Block_Id);
            Uri uri = new Uri("rabbitmq://localhost/AirlineunblockQueue");
            var endPoint = await bus.GetSendEndpoint(uri);
            await endPoint.Send(airline_Block_Id);
            return true;
        }
        [HttpDelete("{id}")]
        public async Task<bool> DeleteAirlinebyId(int id)
        {
            if (db.Airlines.Any(X => X.AirlineId == id))
            {
                var data =await db.Airlines.Where(X => X.AirlineId == id).FirstOrDefaultAsync();
                db.Airlines.Remove(data);
                await db.SaveChangesAsync();

                Id transferid = new Id();
                transferid.TransferId = id;
                await publishEndpoint.Publish<Id>(transferid);
                Uri uri = new Uri("rabbitmq://localhost/FlightdeleteQueue");
                var endPoint = await bus.GetSendEndpoint(uri);
                await endPoint.Send(transferid);
                return true;
            }
            return false;

        }
        [HttpPost]
        [Route("logo/{id}")]
        public async Task<ActionResult<string>> UploadImage([FromRoute] int id, IFormFile profileImage)
        {
            try
            {
                Airline airline = await db.Airlines.Where(x => x.AirlineId == id).FirstOrDefaultAsync();

                 
                var fileName = Guid.NewGuid() + Path.GetExtension(profileImage.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Images", fileName);
                using Stream fileStream = new FileStream(filePath, FileMode.Create);
                await profileImage.CopyToAsync(fileStream);
                string Logo = Path.Combine(@"Resources\Images", fileName);

                airline.Logo = Logo;
                db.Airlines.Update(airline);
                await db.SaveChangesAsync();

                TransferString transferString = new TransferString();
                transferString.Transferstring = Logo;
                transferString.Transferid = id;

                await publishEndpoint.Publish<TransferString>(transferString);
                Uri uri = new Uri("rabbitmq://localhost/AirlinelogoQueue");
                var endPoint = await bus.GetSendEndpoint(uri);
                await endPoint.Send(transferString);

                return Logo;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Internal DB Error");
            }

        }
    }
}
