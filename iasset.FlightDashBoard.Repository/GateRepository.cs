using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iasset.FlightDashBoard.Domain;
using iasset.FlightDashBoard.Domain.Constants;
using iasset.FlightDashBoard.Domain.Enums;
using iasset.FlightDashBoard.Repository.Interfaces;

namespace iasset.FlightDashBoard.Repository
{
    public class GateRepository : IGateRepository
    {
        private static readonly List<Gate> _fakeGates = new List<Gate>();

        public List<Gate> GatesStore
        {
            get { return _fakeGates; }
        }
        static GateRepository()
        {
            InitData();
        }
        public bool AddFlight(Flight flight, int gateNumber)
        {
            var gate = GatesStore.Find(x => x.GateNumber == gateNumber);
            if (gate == null)
            {
                return false;
            }
            gate.Flights.Add(flight);
            return true;
        }

       
        public bool RemoveFlight(Flight flight, int oldGateNumber)
        {
            var oldflightGate = GatesStore.Find(x => x.GateNumber == oldGateNumber);
            if (oldflightGate == null)
            {
                return false;
            }
            oldflightGate.Flights.Remove(flight);
            return true;
        }
        public Gate Get(int gateNumber)
        {
            return GatesStore.Find(x => x.GateNumber == gateNumber); 
        }
        public List<Gate> GetAll()
        {
            return GatesStore;
        }
        public Flight GetFlight(string flightNumber)
        {
            Flight flight = null;
            foreach (var gate in GatesStore)
            {
                flight = gate.Flights.FirstOrDefault(f => f.FlightNumber == flightNumber);
                if (flight != null) break;
            }
            return flight;
        }
        public List<Flight> GetFlights(int gateNumber)
        {
            var gate = GatesStore.Find(x => x.GateNumber == gateNumber);
            if (gate == null)
            {
                return null;
            }
            return gate.Flights;
        }

        public bool UpdateFlight(Flight flight, int gateNumber)
        {
            var gate = GatesStore.Find(x => x.GateNumber == gateNumber);
            if (gate == null)
            {
                return false;
            }
            var updateFlight = gate.Flights.Find(f => f.FlightNumber == flight.FlightNumber);
            if (updateFlight == null)
            {
                return false;
            }
            updateFlight.ArrivalTime = flight.ArrivalTime;
            updateFlight.DepartureTime = flight.DepartureTime;
            return true;
        }
        public bool UpdateFlightStatus(Flight flight, int gateNumber)
        {
            var gate = GatesStore.Find(x => x.GateNumber == gateNumber);
            if (gate == null)
            {
                return false;
            }
            var updateFlight = gate.Flights.Find(f => f.FlightNumber == flight.FlightNumber);
            if (updateFlight == null)
            {
                return false;
            }
            updateFlight.FlightStatus = flight.FlightStatus;
            return true;
        }
        private static void InitData()
        {
             for (int gateNo = 1; gateNo <= 2; gateNo ++)
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
                _fakeGates.Add(gate);
            }
        }
    }
}
