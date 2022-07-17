
using AutoMapper;
using BookingService.Model;
using MassTransit;
using common;
using System.Threading.Tasks;
using BookingService.Dto;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BookingService.RabbitMQConsumer
{
    public class AirlineConsumer : IConsumer<Airline_Shared>
    {
        private readonly BookingDbContext _db;
        private readonly IMapper _mapper;
        public AirlineConsumer(BookingDbContext db, IMapper mapper)
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
    public class AirlineblockConsumer : IConsumer<Airline_block_id>
    {
        private readonly BookingDbContext _db;
        private readonly IMapper _mapper;
        public AirlineblockConsumer(BookingDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<Airline_block_id> context)
        {
            try
            {
                var id = context.Message;
                int airlineid = id.AirlineId;
                Airline airline = _db.Airlines.Where(x => x.AirlineId == airlineid).Include(i => i.Flights).FirstOrDefault();

                airline.Status = "Blocked";
                foreach (Flight flight in airline.Flights)
                {
                    flight.Status = "Blocked";
                    _db.Flights.Update(flight);
                    await _db.SaveChangesAsync();

                }
                _db.Airlines.Update(airline);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }

        }
    }

    public class AirlineunblockConsumer : IConsumer<Airline_block_id>
    {
        private readonly BookingDbContext _db;
        private readonly IMapper _mapper;
        public AirlineunblockConsumer(BookingDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<Airline_block_id> context)
        {
            try
            {
                var id = context.Message;
                int airlineid = id.AirlineId;
                Airline airline = _db.Airlines.Where(x => x.AirlineId == airlineid).Include(i => i.Flights).FirstOrDefault();

                airline.Status = "Active";
                foreach (Flight flight in airline.Flights)
                {
                    flight.Status = "Active";
                    _db.Flights.Update(flight);
                    await _db.SaveChangesAsync();

                }
                _db.Airlines.Update(airline);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }

        }
    }
}
public class AirlinelogoConsumer : IConsumer<TransferString>
{
    private readonly BookingDbContext _db;
    private readonly IMapper _mapper;
    public AirlinelogoConsumer(BookingDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<TransferString> context)
    {
        try
        {
            var id = context.Message;
            string logo = id.Transferstring;
            int airlineid = id.Transferid;

            Airline airline = await _db.Airlines.Where(x => x.AirlineId == airlineid).FirstOrDefaultAsync();


            airline.Logo = logo;
            _db.Airlines.Update(airline);
            await _db.SaveChangesAsync();

        }
        catch (Exception ex)
        {

        }

    }
}

