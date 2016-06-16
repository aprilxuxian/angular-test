using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iasset.FlightDashBoard.Domain
{
    public class Gate
    {
        public int GateNumber { get; set; }
        public List<Flight> Flights { get; set; }
    }
}
