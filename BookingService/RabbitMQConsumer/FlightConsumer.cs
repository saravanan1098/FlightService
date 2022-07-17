using AutoMapper;
using BookingService.Dto;
using MassTransit;

using System.Threading.Tasks;
using BookingService.Model;
using System;
using System.Linq;
using common;
using Microsoft.EntityFrameworkCore;

namespace BookingService.RabbitMQConsumer
{
    public class FlightConsumer : IConsumer<Flight_Shared>
    {
        private readonly BookingDbContext db;
        private readonly IMapper mapper;
        public FlightConsumer(BookingDbContext _db, IMapper _mapper)
        {
            db = _db;
            mapper = _mapper;
        }
        public async Task Consume(ConsumeContext<Flight_Shared> context)
        {
            try
            {
                var flight_shared = context.Message;
                Flight flight = new Flight();
                flight = mapper.Map<Flight>(flight_shared);
                await db.Flights.AddAsync(flight);
                await db.SaveChangesAsync();

            }
            catch (Exception ex)
            {

            }

        }
    }
    public class FlightdeleteConsumer : IConsumer<Id>
    {
        private readonly BookingDbContext _db;
        private readonly IMapper _mapper;
        public FlightdeleteConsumer(BookingDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<Id> context)
        {
            try
            {
                var id = context.Message;
                int flightid = id.TransferId;
                Flight flight = _db.Flights.Where(x => x.FlightId == flightid).FirstOrDefault();

                _db.Flights.Remove(flight);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }

        }
    }


}
