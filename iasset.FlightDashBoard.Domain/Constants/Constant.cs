using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iasset.FlightDashBoard.Domain.Constants
{
    public class Constant
    {
        public const int ArrivalIntervalMins = 30;
        public const int DepartIntervalMins = 29;
        public const string AddFlightMessage = "Flight is added!";
        public const string CancelFlightMessage = "Flight is cancelled!";
        public const string ChhangeFlightGateMessage = "Flight is changed to gate:";
        public const string UpdateFlightMessage = "Flight is updated!";
        public const string GateErrorMessage = "Gate not found!";
        public const string ArrivalErrorMessage = "Arrival time cannot be behind departure time!";
        public const string FlghtNoErrorMessage = "Flight number already exists!";
        public const string FlghtNoNotExist = "Flight number does not exist!";
        public const string FlightNo = "Flight";
        public const string ErrorMessage = "Ops, the service is temporarily unavaiable, please contact the system adminstrator.";
        public const string OverlapErrorMessage = "Overlap! Please push the rest of the flights or assign it to another available gate!";
        public const string GateNo = "GN";
    }
}
