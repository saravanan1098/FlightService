using AutoMapper;
using BookingService.Model;
using MassTransit;
using common;
using BookingService.Dto;
using System.Threading.Tasks;
using System;

namespace BookingService.RabbitMQConsumer
{
    public class SeatnumberConsumer : IConsumer<Seatnumber_Shared>
    {
        private readonly BookingDbContext _db;
        private readonly IMapper _mapper;
        public SeatnumberConsumer(BookingDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<Seatnumber_Shared> context)
        {
            try
            {
                var seatnumber_shared = context.Message;

                Seatnumber Seatnumber = new Seatnumber();
                Seatnumber = _mapper.Map<Seatnumber>(seatnumber_shared);
                await _db.Seatnumbers.AddAsync(Seatnumber);
                await _db.SaveChangesAsync();



            }
            catch (Exception ex)
            {

            }


            //await Console.Out.WriteLineAsync(msg);
        }
    }
}