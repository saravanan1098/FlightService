using AutoMapper;
using BookingService.Model;
using common;
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
            CreateMap<Airline, Airline_Shared>().ReverseMap();
            CreateMap<Flight, Flight_Shared>().ReverseMap();
            CreateMap<Seatnumber, Seatnumber_Shared>().ReverseMap();
            CreateMap<Flight, flightDto>().ReverseMap();


        }

    }
}
