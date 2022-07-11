using AutoMapper;
using BookingService.Dto;
using MassTransit;
using common;
using System.Threading.Tasks;
using BookingService.Model;
using System;

namespace BookingService.RabbitMQConsumer
{
    public class FlightConsumer : IConsumer<Flight_Shared>
    {
        private readonly BookingServiceDbContext _db;
        private readonly IMapper _mapper;
        public FlightConsumer(BookingServiceDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<Flight_Shared> context)
        {
            try
            {
                var flight_shared = context.Message;


                //foreach (var seatnumber_Shared in flight_shared.Seatnumbers_Shared)
                //{
                //    Seatnumber seatnumber = new Seatnumber();
                //    seatnumber = _mapper.Map<Seatnumber>(seatnumber_Shared);
                //    _db.Seatnumbers.Add(seatnumber);
                //}

                Flight flight = new Flight();
                flight = _mapper.Map<Flight>(flight_shared);
                _db.Flights.Add(flight);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }

        }
    }

}