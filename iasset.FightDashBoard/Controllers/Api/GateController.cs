using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using iasset.FlightDashBoard.Domain;
using iasset.FlightDashBoard.Services.Interfaces;
using AttributeRouting;
using AttributeRouting.Web.Http;
namespace iasset.FlightDashBoard.Controllers.Api
{
    public class GateController: ApiController
    {
        private readonly IGateService _gateService;

        public GateController(IGateService gateService)
		{
            _gateService = gateService;
		}

        //[GET("{id:int}/flights")]
		[HttpGet]
        public List<Flight> GetFlights(int gateNumber)
		{
            var flights = _gateService.GetFlights(gateNumber);

            return flights ?? new List<Flight>();
		}
    }
}