using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iasset.FlightDashBoard.Domain;

namespace iasset.FlightDashBoard.Repository.Interfaces
{
    public interface IGateRepository
    {
        bool AddFlight(Flight flight, int gateNumber);
        bool RemoveFlight(Flight flight, int oldGateNumber);
        Gate Get(int gateNumber);
        Flight GetFlight(string flightNumber);
        List<Gate> GetAll();
        List<Flight> GetFlights(int gateNumber);
        bool UpdateFlight(Flight flight, int gateNumber);
        bool UpdateFlightStatus(Flight flight, int gateNumber);
        List<Gate> GatesStore { get; }
    }
}
