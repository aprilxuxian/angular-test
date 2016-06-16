using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iasset.FlightDashBoard.Domain;
using iasset.FlightDashBoard.Domain.Enums;

namespace iasset.FlightDashBoard.Services.Interfaces
{
    public interface IGateService
    {
        int AddFlight(Flight flight, int gateNumber);
        Flight CancelFlight(Flight flight, int gateNumber);
        Flight ChangeGate(Flight flight, int oldGateNumber, int gateNumber);
        List<Gate> GetAllGatess();
        Flight GetFlight(string flightNumber);
        Gate GetGate(int gateNumber);
        int Update(Flight flight, int gateNumber);
    }
}
