
using AutoMapper;
using BookingService.Model;
using MassTransit;
using common;
using System.Threading.Tasks;
using BookingService.Dto;
using System;


namespace BookingService.AirlineConsumer
{
    public class AirlineConsumer : IConsumer<Airline_Shared>
    {
        private readonly BookingServiceDbContext _db;
        private readonly IMapper _mapper;
        public AirlineConsumer(BookingServiceDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<Airline_Shared> context)
        {
            try
            {
                var airline_shared = context.Message;
                Airline airline = new Airline();
                airline = _mapper.Map<Airline>(airline_shared);
                await _db.Airlines.AddAsync(airline);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }


            //await Console.Out.WriteLineAsync(msg);
        }
    }
}