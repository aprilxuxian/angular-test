using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using iasset.FlightDashBoard.Domain;
using iasset.FlightDashBoard.Domain.Constants;
using iasset.FlightDashBoard.Domain.Enums;
using iasset.FlightDashBoard.Repository.Interfaces;
using iasset.FlightDashBoard.Services.Interfaces;

namespace iasset.FlightDashBoard.Services
{
    public class GateService : IGateService
    {
        private readonly IGateRepository _gateRepository;

        static GateService()
        {
            ErrorMessages = new Dictionary<ErrorCode, string>();
            ErrorMessages[ErrorCode.InvalidFlightNumber] = Constant.FlghtNoErrorMessage;
            ErrorMessages[ErrorCode.FlightNumberNotExist] = Constant.FlghtNoNotExist;
            ErrorMessages[ErrorCode.InvalidGateNumber] = Constant.GateErrorMessage;
            ErrorMessages[ErrorCode.Overlap] = Constant.OverlapErrorMessage;
        }

        public GateService(IGateRepository gateRepository)
		{

            _gateRepository = gateRepository;
		}
        public int AddFlight(Flight flight, int gateNumber)
        {
            var dbflight = GetFlight(flight.FlightNumber);
            if (dbflight != null)
            {
                return (int)ErrorCode.InvalidFlightNumber;
            }
            var gate = GetGate(gateNumber);
            if (gate == null)
            {
                return (int)ErrorCode.InvalidGateNumber;
            }
            if (IsOverLap(gate, flight))
            {
                return (int)ErrorCode.Overlap;
            }
            _gateRepository.AddFlight(flight, gateNumber);
            return 0;
        }
       
        public Flight CancelFlight(Flight flight, int gateNumber)
        {
            flight.FlightStatus = FlightStatus.Cancelled;
            _gateRepository.UpdateFlightStatus(flight, gateNumber);
            return flight;
        }
        public Flight ChangeGate(Flight flight, int oldGateNumber, int gateNumber)
        {
            _gateRepository.RemoveFlight(flight, oldGateNumber);
            _gateRepository.AddFlight(flight, gateNumber);
            return flight;
        }
        public List<Gate> GetAllGatess()
        {
            return _gateRepository.GetAll();
        }
        public Flight GetFlight(string flightNumber)
        {
            return _gateRepository.GetFlight(flightNumber);
        }
        public Gate GetGate(int gateNumber)
        {
            var gate = _gateRepository.Get(gateNumber);
            return gate;
        }
        public int Update(Flight flight, int gateNumber)
        {
            var dbflight = GetFlight(flight.FlightNumber);
            if (dbflight == null)
            {
                return (int)ErrorCode.FlightNumberNotExist;
            }
            var gate = GetGate(gateNumber);
            if (gate == null)
            {
                return (int)ErrorCode.InvalidGateNumber;
            }
            if (IsOverLap(gate, flight))
            {
                return (int)ErrorCode.Overlap;
            }
            _gateRepository.UpdateFlight(flight, gateNumber);
            return 0;
        }

        public static Dictionary<ErrorCode,string> ErrorMessages{ get; private set; }

        private bool IsOverLap(Gate gate, Flight flight)
        {
            var overlap1 =
               gate.Flights.Any(
                   f => f.FlightNumber != flight.FlightNumber &&
                       f.ArrivalTime.CompareTo(flight.ArrivalTime) < 0 &&
                       f.ArrivalTime.AddMinutes(30).CompareTo(flight.ArrivalTime) > 0);
            var overlap2 =
               gate.Flights.Any(
                   f => f.FlightNumber != flight.FlightNumber &&
                       f.ArrivalTime.CompareTo(flight.ArrivalTime) > 0 &&
                       flight.ArrivalTime.AddMinutes(30).CompareTo(f.ArrivalTime) > 0);
            var overlap3 =
              gate.Flights.Any(
                  f => f.FlightNumber != flight.FlightNumber &&
                      f.ArrivalTime.CompareTo(flight.ArrivalTime) < 0 &&
                      flight.ArrivalTime.CompareTo(f.DepartureTime) < 0);
            var overlap4 =
             gate.Flights.Any(
                 f => f.FlightNumber != flight.FlightNumber &&
                     f.ArrivalTime.CompareTo(flight.DepartureTime) < 0 &&
                     flight.DepartureTime.CompareTo(f.DepartureTime) < 0);
            if (overlap1 || overlap2 || overlap3 || overlap4)
            {
                return true;
            }
            return false;
        }
    }
}
