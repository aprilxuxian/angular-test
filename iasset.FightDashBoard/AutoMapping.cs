using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using iasset.FlightDashBoard.Domain;
using iasset.FlightDashBoard.Models;

namespace iasset.FlightDashBoard
{
    public static class AutoMappings
    {
        public static void ConfigureAutoMappings()
        {
            Mapper.CreateMap<Flight, FlightModel>()
                .ForMember(
                    flightModel => flightModel.ArrivalTime, opt => opt.MapFrom(flight => flight.ArrivalTime.ToString("HH:mm")))
                .ForMember(
                    flightModel => flightModel.DepartureTime, opt => opt.MapFrom(flight => flight.DepartureTime.ToString("HH: mm")))
                .ForMember(
                    flightModel => flightModel.FlightStatus, opt => opt.MapFrom(flight => flight.FlightStatus.ToString()));
            Mapper.CreateMap<FlightModel, Flight>()
                 .ForMember(
                    flightModel => flightModel.ArrivalTime, opt => opt.MapFrom(flight => DateTime.Parse(flight.ArrivalTime)))
                .ForMember(
                    flightModel => flightModel.DepartureTime, opt => opt.MapFrom(flight => DateTime.Parse(flight.DepartureTime)));
            Mapper.CreateMap<Gate, GateModel>();
            Mapper.CreateMap<GateModel, Gate>();
            Mapper.AssertConfigurationIsValid();
        }
    }
}