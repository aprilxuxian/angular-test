using System;
using System.Collections.Generic;
namespace iasset.FlightDashBoard.Models
{
    public class GateModel
    {
        public int GateNumber { get; set; }
        public List<FlightModel> Flights { get; set; }
    }
}