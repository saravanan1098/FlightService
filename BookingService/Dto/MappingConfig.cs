using AutoMapper;
using BookingService.Model;
using common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Dto
{
    public class MappingConfig
    {
            public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {

            config.CreateMap<Passenger, PassengerDto>().ReverseMap();
            config.CreateMap<Booking, BookingDto>().ReverseMap();
            config.CreateMap<Airline, Airline_Shared>().ReverseMap();
            config.CreateMap<Flight, Flight_Shared>().ReverseMap();
            config.CreateMap<Seatnumber, Seatnumber_Shared>().ReverseMap();
            });

                return mappingConfig;
        }
    }
}
