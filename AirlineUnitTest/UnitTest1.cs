
using FlightService.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
using Microsoft.Extensions.Logging.Abstractions;
using FlightService.Dto;

namespace AirlineUnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void TestAddAirline_Pass()
        {
            var dbContext = DbContextMocker.GetDbContext();
            var controller = new AirlineController();

            AirlineDto model = new AirlineDto
            {
                AirlineName = "Singapore Airlines",
                ContactNumber = "328782928",
                ContactAddress = "Singapore",
            };

            var response = controller.AddAirline(model);

            dbContext.Dispose();

            Assert.IsType<OkResult>(response);
        }

        [Fact]
        public void TestAddAirline_Fail()
        {
            var dbContext = DbContextMocker.GetDbContext();
            var controller = new AirlineController();

            AirlineDto model = null;
            var response = controller.AddAirline(model);

            dbContext.Dispose();

            Assert.IsType<BadRequestResult>(response);
        }
    }
}
