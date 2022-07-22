using FlightService.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AirlineUnitTest
{
    class DbContextMocker
    {
        public static FlightServiceDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<FlightServiceDbContext>()
               .UseInMemoryDatabase(databaseName: "FlightServiceDB")
                .Options;
               

            var dbContext = new FlightServiceDbContext(options);

            dbContext.Seed();

            return dbContext;
        }

    }
}
