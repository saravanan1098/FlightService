using AutoMapper;
using BookingService.Model;
using MassTransit;
using common;
using BookingService.Dto;
using System.Threading.Tasks;
using System;

namespace BookingService.SeatnumberConsumer
{
    public class SeatnumberConsumer : IConsumer<Seatnumber_Shared>
    {
        private readonly BookingServiceDbContext _db;
        private readonly IMapper _mapper;
        public SeatnumberConsumer(BookingServiceDbContext db, IMapper mapper)
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