using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kristianstad.ViewModels.Compare
{
    public class AddressDistanceModel
    {
        public bool Success { get; set; }

        public virtual string MeasureFromAddress { get; set; }
    }
}