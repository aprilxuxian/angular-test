using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iasset.FlightDashBoard.Domain.Enums;

namespace iasset.FlightDashBoard.Domain
{
    [Serializable]
    public class Flight
    {
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public string FlightNumber { get; set; }
        public FlightStatus FlightStatus { get; set; }
    }
}
