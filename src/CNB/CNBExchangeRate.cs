﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Hefferon.CzechServices.CNB
{
    public class CNBExchangeRate
    {
        public DateTime Created { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
        public double Amount { get; set; }
        public string Code { get; set; }
        public double Rate { get; set; }
    }
}
