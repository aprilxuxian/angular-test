using System;
using System.Collections.Generic;
using System.Web.Helpers;
using System.Web.Mvc;
using iasset.FlightDashBoard.Controllers;
using iasset.FlightDashBoard.Domain;
using iasset.FlightDashBoard.Domain.Constants;
using iasset.FlightDashBoard.Domain.Enums;
using iasset.FlightDashBoard.Models;
using iasset.FlightDashBoard.Repository.Interfaces;
using iasset.FlightDashBoard.Services;
using iasset.FlightDashBoard.Services.Interfaces;
using NUnit.Framework;
using Moq;
/*I just add unit test code for 'addflight method' (not all methods) in this controller test class here to demonstrate this skill*/
namespace iasset.FlightDashBoard.Test
{
    [TestFixture]
    public class HomeControllerTest
    {
        [Test]
        public void TestAddFlightExist()
        {
            var gateService = new Mock<IGateService>();
            gateService.Setup(x => x.AddFlight(It.IsAny<Flight>(),1)).Returns((int)ErrorCode.InvalidFlightNumber);
            var service = new HomeController(gateService.Object);
            var fight = new FlightModel
            {
                ArrivalTime = DateTime.Now.ToString("HH:mm"),
                DepartureTime = DateTime.Now.ToString("HH:mm"),
                FlightNumber = "GNFlight1"
            };
            JsonResult result = service.AddFlight(fight, 1);
            Assert.IsInstanceOf<JsonResult>(result);
        }
       
    }
}
