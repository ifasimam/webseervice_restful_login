using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Central.Models.CENTRALSystem
{
    public class CentralSystem
    {
        public string SYSTEM_TYPE { get; set; }
        public string SYSTEM_CD { get; set; }
        public string VALID_FRM { get; set; }
        public string VALID_TO { get; set; }
        public string SYSTEM_VALUE_TXT { get; set; }
        public int SYSTEM_VALUE_NUM { get; set; }
        public DateTime SYSTEM_VALUE_DT { get; set; }
    }
}