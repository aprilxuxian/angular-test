using System;
using System.Collections.Generic;
using iasset.FlightDashBoard.Domain;
using iasset.FlightDashBoard.Domain.Constants;
using iasset.FlightDashBoard.Domain.Enums;
using iasset.FlightDashBoard.Repository.Interfaces;
using iasset.FlightDashBoard.Services;
using NUnit.Framework;
using Moq;
/*I just add unit test code for 'addflight method' (not all methods) in this service test class here to demonstrate this skill*/
namespace iasset.FlightDashBoard.Test
{
    [TestFixture]
    public class GateServiceTest
    {
        [Test]
        public void TestAddFlightExist()
        {
            var gateRepository = new Mock<IGateRepository>();
            gateRepository.SetupGet(x => x.GatesStore).Returns(InitData);
            gateRepository.Setup(x => x.GetFlight("GN1Flight1")).Returns(gateRepository.Object.GatesStore[0].Flights[0]);
            var service = new GateService(gateRepository.Object);
            var fight = new Flight
            {
                ArrivalTime = DateTime.Now,
                DepartureTime = DateTime.Now,
                FlightNumber = "GN1Flight1"
            };
            Assert.AreEqual((int)ErrorCode.InvalidFlightNumber, service.AddFlight(fight, 1));
        }
        [Test]
        public void TestAddFlightGateNotExist()
        {
            var gateRepository = new Mock<IGateRepository>();
            gateRepository.SetupGet(x => x.GatesStore).Returns(InitData);

            var service = new GateService(gateRepository.Object);
            var fight = new Flight
            {
                ArrivalTime = DateTime.Now,
                DepartureTime = DateTime.Now,
                FlightNumber = "GNFlight11"
            };
            Assert.AreEqual((int)ErrorCode.InvalidGateNumber, service.AddFlight(fight, 3));
        }
        [Test]
        public void TestAddFlightOverlap()
        {
            var gateRepository = new Mock<IGateRepository>();
            gateRepository.SetupGet(x => x.GatesStore).Returns(InitData);
            gateRepository.Setup(x => x.Get(1)).Returns(gateRepository.Object.GatesStore[0]);
            var service = new GateService(gateRepository.Object);
            var fight = new Flight
            {
                ArrivalTime = gateRepository.Object.GatesStore[0].Flights[0].ArrivalTime.AddMinutes(20),
                DepartureTime = DateTime.Now,
                FlightNumber = "GNFlight11"
            };
            Assert.AreEqual((int)ErrorCode.Overlap, service.AddFlight(fight, 1));
        }
        [Test]
        public void TestAddFlight()
        {
            var gateRepository = new Mock<IGateRepository>();
            gateRepository.SetupGet(x => x.GatesStore).Returns(InitData);
            gateRepository.Setup(x => x.Get(1)).Returns(gateRepository.Object.GatesStore[0]);
            var service = new GateService(gateRepository.Object);
            var fight = new Flight
            {
                ArrivalTime = DateTime.Now.AddMinutes(10*31),
                DepartureTime = DateTime.Now.AddMinutes(11 * 31),
                FlightNumber = "GNFlight11"
            };
            Assert.AreEqual(0, service.AddFlight(fight, 1));
        }
        private List<Gate> InitData()
        {
            List<Gate> fakeGates = new List<Gate>();
            for (int gateNo = 1; gateNo <= 2; gateNo++)
            {
                var gate = new Gate
                {
                    GateNumber = gateNo
                };
                var flights = new List<Flight>();
                var arrivateTime = DateTime.Now;
                for (int flightNo = 1; flightNo <= 10; flightNo++)
                {
                    var flight = new Flight
                    {
                        ArrivalTime = arrivateTime,
                        DepartureTime = arrivateTime.AddMinutes(Constant.DepartIntervalMins),
                        FlightNumber = string.Concat(Constant.GateNo, gateNo, Constant.FlightNo, flightNo),
                        FlightStatus = FlightStatus.Normal
                    };
                    flights.Add(flight);
                    arrivateTime = arrivateTime.AddMinutes(Constant.ArrivalIntervalMins);
                }
                gate.Flights = flights;
                fakeGates.Add(gate);
            }
            return fakeGates;
        }
    }
}
