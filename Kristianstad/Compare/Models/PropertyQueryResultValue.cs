using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kstistianstad.Compare.Models
{
    public class PropertyQueryResultValue
    {
        public string Gender { get; set; }
        public string Status { get; set; }
        public float? Value { get; set; }  // is it always of number type? never a string or similar?
        
        //Constructors
        public PropertyQueryResultValue()
        {
            // Empty
        }
        public PropertyQueryResultValue(string gender, string status, float? value)
        {
            Gender = gender;
            Status = status;
            Value = value;
        }
    }
}
