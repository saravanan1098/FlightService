using AutoMapper;
using BookingService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Dto
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<Passenger, PassengerDto>().ReverseMap();
            CreateMap<Booking, BookingDto>().ReverseMap();
        }
    }
}
