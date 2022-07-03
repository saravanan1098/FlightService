using AutoMapper;
using FlightService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightService.Dto
{
    public class MappingConfig:Profile

    {
        public MappingConfig()
        {
            CreateMap<Airline, AirlineDto>().ReverseMap();
            CreateMap<Flight, FlightDto>().ReverseMap();
        }
    }
}
