﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iasset.FlightDashBoard.Domain.Exceptions
{
    public class GateException : ApplicationException
    {
        public string ErrorMessage { get; set; }
    }
}
