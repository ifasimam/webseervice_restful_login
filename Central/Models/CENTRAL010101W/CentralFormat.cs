using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Central.Models.CENTRAL010101W
{
    public class CentralFormat
    {
        public string formatValue { get; set; }
        public string partID { get; set; }

        public CentralFormat(string partID, string formatValue)
        {
            this.partID = partID;
            this.formatValue = formatValue;
        }
    }
}