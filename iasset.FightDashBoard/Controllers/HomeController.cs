using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using AutoMapper;
using iasset.FlightDashBoard.Domain;
using iasset.FlightDashBoard.Domain.Constants;
using iasset.FlightDashBoard.Domain.Enums;
using iasset.FlightDashBoard.Models;
using iasset.FlightDashBoard.Services;
using iasset.FlightDashBoard.Services.Interfaces;

namespace iasset.FlightDashBoard.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGateService _gateService;
        private DateTime _now = DateTime.Now;
        private DateTime _endDate;
        private DateTime _startDate;
        public ActionResult Index()
        {
            return View();
        }

        public HomeController(IGateService gateService)
		{
            _gateService = gateService;
            _endDate = new DateTime(_now.Year, _now.Month, _now.Day, 23, 59, 0);
            _startDate = new DateTime(_now.Year, _now.Month, _now.Day, 0, 0, 0);
		}
        [System.Web.Mvc.Route("gate/{gateNumber:int}")]
        [System.Web.Mvc.HttpPost]
        public JsonResult AddFlight(FlightModel flight, int gateNumber)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                     return Json(new { error = ModelState.Values.First().Errors.First().ErrorMessage });
                }
                var flightDomain = Mapper.Map<Flight>(flight);
                flightDomain.FlightStatus = FlightStatus.Normal;
                int result = _gateService.AddFlight(flightDomain, gateNumber);
                if (result == 0) return Json(new { success = Constant.AddFlightMessage });
                return Json(new { error = GateService.ErrorMessages[(ErrorCode)result] });
            }
            catch
            {
                return Json (new {error = Constant.ErrorMessage});
            }
        }
        [System.Web.Mvc.Route("gate/{gateNumber:int}/cancelflight")]
        [System.Web.Mvc.HttpPost]
        public JsonResult CancelFlight(FlightModel flight, int gateNumber)
        {
            try
            {
                var flightDomain = new Flight { FlightNumber = flight.FlightNumber };
                _gateService.CancelFlight(flightDomain, gateNumber);
                return Json(new { success = Constant.CancelFlightMessage });
            }
            catch
            {
                return Json(new { error = Constant.ErrorMessage });
            }
        }
        [System.Web.Mvc.Route("fight/{oldGate:int}/changegate/{gateNumber:int}")]
        [System.Web.Mvc.HttpPost]
        public JsonResult ChangeGate(FlightModel flight, int oldGate, int gateNumber)
        {
            try
            {
                var flightDomain = _gateService.GetFlight(flight.FlightNumber);
                _gateService.ChangeGate(flightDomain, oldGate, gateNumber);
                return Json(new { success = Constant.ChhangeFlightGateMessage + gateNumber });
            }
            catch
            {
                return Json(new { error = Constant.ErrorMessage });
            }
        }
        [System.Web.Mvc.Route("gates")]
        [System.Web.Mvc.HttpGet]
        public JsonResult GetAll()
        {
            try
            {
                var gates = _gateService.GetAllGatess();
                var gatesModels = Mapper.Map<List<GateModel>>(gates) ?? new List<GateModel>();
                foreach (var gate in gatesModels)
                {
                    gate.Flights = Mapper.Map<List<FlightModel>>(gate.Flights.Where(f => DateTime.Parse(f.ArrivalTime) <= _endDate
                        && DateTime.Parse(f.ArrivalTime) >= _startDate && DateTime.Parse(f.DepartureTime) <= _endDate
                        && DateTime.Parse(f.DepartureTime) >= _startDate));
                }
                return Json(new { gatesModels }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { error = Constant.ErrorMessage });
            }
        }
        [System.Web.Mvc.Route("flights/{flightNumber}")]
        [System.Web.Mvc.HttpGet]
        public JsonResult GetFlight(string flightNumber)
        {
            try
            {
                var flight =  Mapper.Map<FlightModel>(_gateService.GetFlight(flightNumber));
                return Json(new { flight }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { error = Constant.ErrorMessage });
            }
        }
        [System.Web.Mvc.Route("{gateNumber:int}/flights")]
		[System.Web.Mvc.HttpGet]
        public JsonResult GetFlights(int gateNumber)
		{
            try
            {
                var gate = Mapper.Map<GateModel>(_gateService.GetGate(gateNumber)) ?? new GateModel();
                gate.Flights =
                    Mapper.Map<List<FlightModel>>(gate.Flights.Where(f => DateTime.Parse(f.ArrivalTime) <= _endDate
                                                                          && DateTime.Parse(f.ArrivalTime) >= _startDate &&
                                                                          DateTime.Parse(f.DepartureTime) <= _endDate
                                                                          &&
                                                                          DateTime.Parse(f.DepartureTime) >= _startDate));
                return Json(new {gate}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { error = Constant.ErrorMessage });
            }
		}
        [System.Web.Mvc.Route("gate/{gateNumber:int}/updateflight")]
        [System.Web.Mvc.HttpPost]
        public JsonResult UpdateFlight(FlightModel flight, int gateNumber)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { error = ModelState.Values.First().Errors.First().ErrorMessage + flight.FlightNumber });
                }
                var flightDomain = Mapper.Map<Flight>(flight);
                flightDomain.FlightStatus = FlightStatus.Normal;
                int result = _gateService.Update(flightDomain, gateNumber);
                if (result == 0) return Json(new { success = Constant.AddFlightMessage });
                return Json(new { error = GateService.ErrorMessages[(ErrorCode)result] });
            }
            catch
            {
                return Json(new { error = Constant.ErrorMessage });
            }
        }
    }
}
