using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iasset.FlightDashBoard.Domain.Enums
{
    public enum ErrorCode
    {
       InvalidFlightNumber = 1,
       FlightNumberNotExist,
       InvalidGateNumber,
       InvalidArrivalTime,
       Overlap
    }
}
